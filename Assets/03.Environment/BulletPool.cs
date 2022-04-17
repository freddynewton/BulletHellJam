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
            bullets[i] = Instantiate(bullet, transform.position, bullet.transform.rotation);
            bullets[i].SetActive(false);
            bullets[i].transform.parent = gameObject.transform;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ShootBullets();
    }

    private void ShootBullets()
    {
        for (int i = 0; i < 5; i++)
        {
            bullets[i].transform.position = new Vector2(Random.Range(-6f, 6f), 20);
            bullets[i].SetActive(true);
            bullets[i].GetComponent<Bullet>().Initialize();
        }
    }
}
