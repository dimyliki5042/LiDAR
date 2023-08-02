using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Disconnect : MonoBehaviour
{
    ChooseClass scriptClass;
    private void Start()
    {
        scriptClass = transform.GetComponent<ChooseClass>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(Manager.role == null) StartCoroutine(DisconnectServer());
            else StartCoroutine(DisconnectGame());
        }
    }
    public IEnumerator UpdateTable()
    {
        WWWForm form = new WWWForm();
        form.AddField("role", Manager.role);
        form.AddField("idRoom", PlayerPrefs.GetInt("RoomId"));
        form.AddField("playerId", PlayerPrefs.GetInt("PlayerId"));
        UnityWebRequest request;
        request = UnityWebRequest.Post(Links.links["setRole"], form);
        yield return request.SendWebRequest();
        do
        {
            request = UnityWebRequest.Post(Links.links["checkOpponent"], form);
            yield return request.SendWebRequest();
        } while (request.downloadHandler.text == "0");
        Cursor.visible = false;
        scriptClass.Spawn();
        do
        {
            request = UnityWebRequest.Post(Links.links["checkOpponent"], form);
            yield return request.SendWebRequest();
        } while (request.downloadHandler.text != "0");
        StartCoroutine(DisconnectGame());
        yield break;
    }

    public IEnumerator DisconnectGame()
    {
        WWWForm form = new WWWForm();
        form.AddField("role", Manager.role);
        form.AddField("idRoom", PlayerPrefs.GetInt("RoomId"));
        form.AddField("playerId", PlayerPrefs.GetInt("PlayerId"));
        UnityWebRequest request = UnityWebRequest.Post(Links.links["disconnectGame"], form);
        yield return request.SendWebRequest();
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
    public IEnumerator DisconnectServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("idRoom", PlayerPrefs.GetInt("RoomId"));
        form.AddField("playerId", PlayerPrefs.GetInt("PlayerId"));
        UnityWebRequest request = UnityWebRequest.Post(Links.links["disconnect"], form);
        yield return request.SendWebRequest();
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
}
