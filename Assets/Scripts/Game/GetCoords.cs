using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GetCoords : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Write());
        StartCoroutine(Read());
    }
    private Vector3 Parsing(string vec)
    {
        string[] values = new string[3];
        int index = 0;
        foreach (char letter in vec)
        {
            if (letter != Convert.ToChar(";"))
            {
                values[index] += letter.ToString();
            }
            else index++;
        }
        print(values[0]);
        float x = float.Parse(values[0]);
        float y = float.Parse(values[1]);
        float z = float.Parse(values[2]);
        return new Vector3(x, y, z);
    }
    private IEnumerator Read()
    {
        WWWForm form = new WWWForm();
        form.AddField("role", Manager.role);
        form.AddField("roomName", PlayerPrefs.GetString("roomName"));
        while (true)
        {
            UnityWebRequest request = UnityWebRequest.Post(Links.links["read"], form);
            yield return request.SendWebRequest();
            if (request.downloadHandler.text != "") Manager.enemy.transform.position = Parsing(request.downloadHandler.text);
            yield return new WaitForSeconds(0.05f); //Тестовое время
        }
    }
    private IEnumerator Write()
    {
        Transform player = Manager.player.transform;
        while (true)
        {
            if (player != Manager.player.transform)
            {
                WWWForm form = new WWWForm();
                form.AddField("role", Manager.role);
                form.AddField("roomName", PlayerPrefs.GetString("roomName"));
                string vec = $"{Math.Round(player.position.x, 4)};{Math.Round(player.position.y, 4)};{(float)Math.Round(player.position.z, 4)}";
                vec.Replace(".", ",");
                form.AddField("vec", vec);
                UnityWebRequest request = UnityWebRequest.Post(Links.links["write"], form);
                yield return request.SendWebRequest();
            }
        }
    }
}
