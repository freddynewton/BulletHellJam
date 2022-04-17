using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private int bulletAmount = 50;
    private GameObject[] bullets;

    private void Start()
    {
        bullets = new GameObject[bulletAmount];
        for (int i = 0; i < bulletAmount; i++)
        {
            bullets[i] = Instantiate(bullet, transform.position, Quaternion.identity);
            bullets[i].SetActive(false);
            bullets[i].transform.parent = gameObject.transform;
        }
    }
}
