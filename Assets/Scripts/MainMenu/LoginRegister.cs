using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginRegister : MonoBehaviour
{
    Account acc;
    [SerializeField] private Text ip;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject loginWindow;
    [SerializeField] private GameObject ipWindow;
    #region Login
    [SerializeField] private Text login;
    [SerializeField] private Text password;
    #endregion
    #region Register
    [SerializeField] private Text username;
    [SerializeField] private Text loginReg;
    [SerializeField] private Text passwordReg;
    #endregion
    private void Start()
    {
        if (PlayerPrefs.HasKey("PlayerId"))
        {
            MainMenu.SetActive(true);
            ipWindow.SetActive(false);
        }
    }
    public void SetIp()
    {
        Links.ip = ip.text;
        Links.SetLinks();
        loginWindow.SetActive(true);
        ipWindow.SetActive(false);
    }
    public void Login()
    {
        StartCoroutine(LoginCoroutine());
    }
    public void Register()
    {
        StartCoroutine(RegCoroutine());
    }
    public void LogOut()
    {
        StartCoroutine(LogOutCoroutine());
    }
    private IEnumerator LoginCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("Login", login.text);
        form.AddField("Password", password.text);
        UnityWebRequest request = UnityWebRequest.Post(Links.links["login"], form);
        yield return request.SendWebRequest();
        if (request.downloadHandler.text != "false")
        {
            acc = JsonUtility.FromJson<Account>(request.downloadHandler.text);
            PlayerPrefs.SetInt("PlayerId", acc.id);
            PlayerPrefs.SetString("Username", acc.username);
            PlayerPrefs.SetInt("RoomId", acc.idRoom);
            MainMenu.SetActive(true);
            loginWindow.SetActive(false);
        }
    }

    private IEnumerator RegCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", username.text);
        form.AddField("Login", loginReg.text.Replace(" ", ""));
        form.AddField("Password", passwordReg.text.Replace(" ", ""));
        UnityWebRequest request = UnityWebRequest.Post(Links.links["reg"], form);
        yield return request.SendWebRequest();
    }

    private IEnumerator LogOutCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("PlayerId"));
        UnityWebRequest request = UnityWebRequest.Post(Links.links["logout"], form);
        yield return request.SendWebRequest();
        PlayerPrefs.DeleteAll();
        MainMenu.SetActive(false);
        loginWindow.SetActive(true);
    }
}
