using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Servers : MonoBehaviour
{
    #region UpdateRooms
    [SerializeField] private GameObject serverPref;
    [SerializeField] private GameObject content;
    #endregion
    #region CreateRoom
    [SerializeField] private Text roomNameText;
    [SerializeField] private Dropdown gamemodeValue;
    [SerializeField] private Text passwordTextCreate;
    #endregion
    #region Connect
    public GameObject choosenRoom;
    [SerializeField] private GameObject roomsList;
    [SerializeField] private GameObject passwordWindow;
    public Image choosenRoomImg;
    [SerializeField] private Text passwordTextConn;
    #endregion
    private void Update()
    {
        if(choosenRoomImg != null) choosenRoomImg.color = Color.green;
    }
    public void ServerUpdate()
    {
        choosenRoom = null;
        choosenRoomImg = null;
        if (content.transform.childCount > 0)
        {
            for (int i = 0; i < content.transform.childCount; i++)
            {
                Destroy(content.transform.GetChild(i).gameObject);
            }
        }
        StartCoroutine(ServerUpdateCoroutine());
    }
    private IEnumerator ServerUpdateCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(Links.links["update"]);
        yield return request.SendWebRequest();
        if (request.downloadHandler.text == "false") yield break;
        RoomArray rooms = JsonUtility.FromJson<RoomArray>("{\"rooms\":" + request.downloadHandler.text + "}");
        for(int i = 0; i < rooms.rooms.Length; i++)
        {
            GameObject server = Instantiate(serverPref, content.transform, false);
            server.name = (i + 1).ToString();
            server.GetComponent<RoomButton>().id = rooms.rooms[i].id;
            server.transform.GetChild(0).GetComponent<Text>().text = rooms.rooms[i].Name;
            server.transform.GetChild(1).GetComponent<Text>().text = rooms.rooms[i].Mode;
            server.transform.GetChild(2).GetComponent<Text>().text = rooms.rooms[i].Players.ToString();
        }
    }

    public void CreateRoom()
    {
        choosenRoom = null;
        choosenRoomImg = null;
        StartCoroutine(CreateRoomCoroutine());
        ServerUpdate();
    }
    private IEnumerator CreateRoomCoroutine()
    {
        string roomName = roomNameText.text;
        string gamemode = gamemodeValue.options[gamemodeValue.value].text;
        string password = passwordTextCreate.text.Replace(" ", "");
        WWWForm form = new WWWForm();
        form.AddField("roomName", roomName);
        form.AddField("gamemode", gamemode);
        form.AddField("password", password);
        UnityWebRequest request = UnityWebRequest.Post(Links.links["create"], form);
        yield return request.SendWebRequest();
    }

    public void EnterPassword()
    {
        if(choosenRoom != null)
        {
            roomsList.SetActive(false);
            passwordWindow.SetActive(true);
        }
    }
    public void CancelPassword()
    {
        roomsList.SetActive(true);
        passwordWindow.SetActive(false);
        ServerUpdate();
    }
    public void Connect()
    {
        StartCoroutine(ConnectCoroutine());
    }
    private IEnumerator ConnectCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("roomId", choosenRoom.GetComponent<RoomButton>().id);
        form.AddField("PlayerId", PlayerPrefs.GetInt("PlayerId"));
        form.AddField("Password", passwordTextConn.text);
        UnityWebRequest request = UnityWebRequest.Post(Links.links["connect"], form);
        yield return request.SendWebRequest();
        if (request.isDone && request.downloadHandler.text != "false")
        {
                PlayerPrefs.SetInt("RoomId", choosenRoom.GetComponent<RoomButton>().id);
                PlayerPrefs.SetString("roomName", request.downloadHandler.text);
                DontDestroyOnLoad(GameObject.Find("GLOBAL"));
                SceneManager.LoadScene(1);
        }
        else yield break;
    }
}
