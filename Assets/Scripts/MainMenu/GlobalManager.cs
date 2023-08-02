using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class GlobalManager : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Disconnect scriptDisc = GameObject.Find("Manager").GetComponent<Disconnect>();
            if (Manager.role == null) StartCoroutine(scriptDisc.DisconnectServer());
            else StartCoroutine(scriptDisc.DisconnectGame());
        }
        StartCoroutine(LogOutCoroutine());
        PlayerPrefs.DeleteAll();
    }

    private IEnumerator LogOutCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("PlayerId"));
        UnityWebRequest request = UnityWebRequest.Post(Links.links["logout"], form);
        yield return request.SendWebRequest();
        PlayerPrefs.DeleteAll();
    }
}
