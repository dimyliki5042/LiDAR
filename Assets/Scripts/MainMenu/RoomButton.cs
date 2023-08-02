using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    public int id;
    public string roomName;
    private Image img;
    private void Awake()
    {
        img = transform.GetComponent<Image>();
    }
    private void OnMouseEnter()
    {
        img.color = Color.yellow;
    }
    private void OnMouseExit()
    {
        img.color = Color.white;
    }
    private void OnMouseDown()
    {
        GameObject.Find("WebManager").GetComponent<Servers>().choosenRoom = gameObject;
        GameObject.Find("WebManager").GetComponent<Servers>().choosenRoomImg = transform.GetComponent<Image>();
    }
}
