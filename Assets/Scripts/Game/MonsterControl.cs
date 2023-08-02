using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    private float xMos;
    [SerializeField] private float sensivity;

    private void Awake()
    {
        xMos = 90;
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal")) Move("Horizontal", transform.right);
        if (Input.GetButton("Vertical")) Move("Vertical", transform.forward);
        Rotate();
    }

    private void Move(string direction, Vector3 dir)
    {
        dir = Input.GetAxisRaw(direction) * dir;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);
    }

    private void Rotate()
    {
        xMos += Input.GetAxis("Mouse X") * sensivity;
        transform.rotation = Quaternion.Euler(0, xMos, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Manager.mainCamera.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
