using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChooseClass : MonoBehaviour
{
    [SerializeField] private GameObject redButton;
    [SerializeField] private GameObject greenButton;
    [SerializeField] private GameObject humanPref;
    [SerializeField] private GameObject monsterPref;
    [SerializeField] private GameObject opponentPref;
    private GameObject playerPref;
    private void Start()
    {
        StartCoroutine(CheckClass());
        Manager.mainCamera = Camera.main;
    }
    public void ClickClass(bool monster)
    {
        if (monster)
        {
            playerPref = monsterPref;
            Manager.role = "Monster";
        }
        else
        {
            playerPref = humanPref;
            Manager.role = "Human";
        }
        GameObject.Find("UI").SetActive(false);
        StartCoroutine(transform.GetComponent<Disconnect>().UpdateTable());
    }
    public void Spawn()
    {
        GameObject player = Instantiate(playerPref);
        Camera.main.gameObject.SetActive(false);
        player.name = playerPref.name;
        GameObject enemy = Instantiate(opponentPref);
        Manager.enemy = enemy;
        if (Manager.role == "Monster")
        {
            player.transform.position = new Vector3(3.5f, 0.3f, -0.75f);
            enemy.transform.position = new Vector3(-4.5f, 0.3f, -0.75f);
            player.tag = "Monster";
            enemy.tag = "Player";
        }
        else
        {
            player.transform.position = new Vector3(-4.5f, 0.3f, -0.75f);
            enemy.transform.position = new Vector3(3.5f, 0.3f, -0.75f);
            player.tag = "Player";
            enemy.tag = "Monster";
            enemy.GetComponent<MeshRenderer>().enabled = false;
        }
        Manager.player = player;
        gameObject.AddComponent(typeof(GetCoords));
    }
    private IEnumerator CheckClass()
    {
        while (Manager.role == null)
        {
            WWWForm form = new WWWForm();
            form.AddField("idRoom", PlayerPrefs.GetInt("RoomId"));
            UnityWebRequest request = UnityWebRequest.Post(Links.links["checkClass"], form);
            yield return request.SendWebRequest();
            int a = Convert.ToInt32(request.downloadHandler.text);
            if (a / 10 != 0) redButton.GetComponent<Button>().interactable = false;
            if (a % 10 != 0) redButton.GetComponent<Button>().interactable = false;
        }
        yield break;
    }
}
