using UnityEngine;

public class Lidar : MonoBehaviour
{
    [SerializeField] private GameObject lidarStorage;
    [SerializeField] private GameObject hitPref;
    [SerializeField] private Material monsterHit;
    private void Start()
    {
        lidarStorage = GameObject.Find("LidarStorage");
    }
    private void Update()
    {
        //(transform.forward - transform.up * 0.1f - transform.right * 0.1f) * 100f === –¿¡Œ◊¿ﬂ ‘Œ–Ã”À¿!!!!!!

        if (Input.GetMouseButtonDown(0))
        {
            if (lidarStorage.transform.childCount != 0) foreach (Transform child in lidarStorage.transform) Destroy(child.gameObject);
            for (float up = -0.5f; up < 0.6f; up += 0.1f)
            {
                for (float right = -1f; right < 1.1f; right += 0.1f)
                {
                    Ray ray = new Ray(transform.position, (transform.forward + transform.up * up + transform.right * right) * 100f);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        GameObject hitBall = Instantiate(hitPref, hit.point, new Quaternion(0, 0, 0, 0), lidarStorage.transform);
                        if (hit.transform.tag == "Monster") hitBall.GetComponent<MeshRenderer>().material = monsterHit;
                    }
                }
            }
        }
    }
}
