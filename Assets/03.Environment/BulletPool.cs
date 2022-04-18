using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject fireSource;
    [SerializeField] private GameObject bullet;
    private GameObject[][] rowPool;
    [SerializeField] private int rowWaves;
    private int rowPerWave = 11;
    // private List<GameObject> sectorPool = new List<GameObject>();
    private GameObject[][] sectorPool;
    [SerializeField] private int sectorWaves;
    private int sectorPerWave = 9;

    private void Start()
    {
        rowPool = new GameObject[rowWaves][];

        for (int i = 0; i < rowPool.Length; i++)
        {
            rowPool[i] = new GameObject[rowPerWave];
        }

        for (int i = 0; i < rowWaves; i++)
        {
            for (int j = 0; j < rowPerWave; j++)
            {
                GameObject rowBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                rowPool[i][j] = rowBullet;
                rowPool[i][j].SetActive(false);
                rowPool[i][j].transform.parent = transform;
            }
        }

        sectorPool = new GameObject[sectorWaves][];

        for (int i = 0; i < sectorWaves; i++)
        {
            sectorPool[i] = new GameObject[sectorPerWave];
        }

        for (int i = 0; i < sectorWaves; i++)
        {
            for (int j = 0; j < sectorPerWave; j++)
            {
                GameObject sectorBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                sectorPool[i][j] = sectorBullet;
                sectorPool[i][j].SetActive(false);
                sectorPool[i][j].transform.parent = transform;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StopCoroutine(ShootRowCoroutine());
            StartCoroutine(ShootRowCoroutine());
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StopCoroutine(ShootSectorCoroutine());
            StartCoroutine(ShootSectorCoroutine());
        }
    }

    private IEnumerator ShootRowCoroutine()
    {
        int gap = 2;
        for (int i = 0; i < rowWaves; i++)
        {
            for (int j = 0; j < rowPerWave; j++)
            {
                Vector2 bulletPosition;
                bulletPosition.x = fireSource.transform.position.x - gap * (rowPerWave / 2) + gap * j;
                bulletPosition.y = fireSource.transform.position.y;

                rowPool[i][j].transform.position = bulletPosition;
                rowPool[i][j].SetActive(true);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator ShootSectorCoroutine()
    {
        float angle = 20f;
        for (int i = 0; i < sectorWaves; i++)
        {
            for (int j = 0; j < sectorPerWave; j++)
            {
                sectorPool[i][j].transform.Rotate(0f, 0f, -4 * angle + j * angle);
                sectorPool[i][j].transform.position = fireSource.transform.position;
                sectorPool[i][j].SetActive(true);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void ShootStrafing()
    {
    }
}
