using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject player;
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
            ShootRow();
    }

    private void ShootRow()
    {
        int offset = 2;
        int bulletCount = 9;
        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 bulletPosition;
            bulletPosition.x = player.transform.position.x - offset * (i - bulletCount / 2);
            bulletPosition.y = 10f;
            bullets[i].transform.position = bulletPosition;
            bullets[i].SetActive(true);
        }
    }

    private void ShootSector()
    {
    }

    private void ShootStrafing()
    {
    }
}
