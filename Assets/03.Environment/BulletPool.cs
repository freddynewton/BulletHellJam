using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject player; // ! Just for testing, should base on firewall
    [SerializeField] private GameObject bullet;
    private Queue<GameObject> rowPool = new Queue<GameObject>();
    [SerializeField] private int rowPoolCount;

    private void Start()
    {
        for (int i = 0; i < rowPoolCount; i++)
        {
            GameObject rowBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            rowPool.Enqueue(rowBullet);
            rowBullet.SetActive(false);
            rowBullet.transform.parent = transform;
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
        // TODO: Shoot more than one wave
        for (int i = 0; i < rowPool.Count; i++)
        {
            Vector2 bulletPosition;
            GameObject bulletToShoot = rowPool.Dequeue();
            // TODO: Should base on firewall position
            bulletPosition.x = player.transform.position.x - offset * (i - rowPool.Count / 2);
            bulletPosition.y = 10f;
            bulletToShoot.transform.position = bulletPosition;
            bulletToShoot.SetActive(true);
            rowPool.Enqueue(bulletToShoot);
        }
    }

    private void ShootSector()
    {
    }

    private void ShootStrafing()
    {
    }
}
