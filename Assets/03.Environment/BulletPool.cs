using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject fireSource;
    [SerializeField] private GameObject bullet;
    private List<GameObject> rowPool = new List<GameObject>();
    [SerializeField] private int rowWaves;
    private int rowPoolCount;
    private List<GameObject> sectorPool = new List<GameObject>();
    [SerializeField] private int sectorWaves;
    private int sectorPoolCount;

    private void Start()
    {
        rowPoolCount = rowWaves * 11;
        for (int i = 0; i < rowPoolCount; i++)
        {
            GameObject rowBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            rowPool.Add(rowBullet);
            rowBullet.SetActive(false);
            rowBullet.transform.parent = transform;
        }

        sectorPoolCount = sectorWaves * 9;
        for (int i = 0; i < sectorPoolCount; i++)
        {
            GameObject sectorBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            sectorPool.Add(sectorBullet);
            sectorBullet.SetActive(false);
            sectorBullet.transform.parent = transform;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            StartCoroutine(ShootRowCoroutine());
        if (Input.GetKeyDown(KeyCode.W))
            ShootSector();
    }

    private IEnumerator ShootRowCoroutine()
    {
        int gap = 2;
        for (int i = 0; i < rowWaves; i++)
        {
            for (int j = i * 11; j < i * 11 + 11; j++)
            {
                Vector2 bulletPosition;
                bulletPosition.x = fireSource.transform.position.x - gap * (rowPoolCount / (rowWaves * 2)) + gap * (j - i * 11);
                bulletPosition.y = fireSource.transform.position.y;

                rowPool[j].transform.position = bulletPosition;
                rowPool[j].SetActive(true);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void ShootSector()
    {
        float angle = 20f;
        for (int i = 0; i < sectorPoolCount; i++)
        {
            sectorPool[i].transform.Rotate(0f, 0f, -4 * angle + i * angle);
            sectorPool[i].transform.position = fireSource.transform.position;
            sectorPool[i].SetActive(true);
        }
    }

    private void ShootStrafing()
    {
    }
}
