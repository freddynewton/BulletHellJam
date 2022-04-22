using System.Collections;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject fireSource;
    [SerializeField] private GameObject bullet;
    private GameObject[][] rowPool;
    [SerializeField] private int rowWaves;
    private int rowPerWave = 11;
    private GameObject[][] sectorPool;
    [SerializeField] private int sectorWaves;
    private int sectorPerWave = 9;

    private void Start()
    {
        InitializePool(out rowPool, rowWaves, rowPerWave);
        InitializePool(out sectorPool, sectorWaves, sectorPerWave);
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

    private void InitializePool(out GameObject[][] bulletPool, int waves, int bulletsPerWave)
    {
        bulletPool = new GameObject[waves][];

        for (int i = 0; i < waves; i++)
        {
            bulletPool[i] = new GameObject[bulletsPerWave];
        }

        for (int i = 0; i < waves; i++)
        {
            for (int j = 0; j < bulletsPerWave; j++)
            {
                GameObject bulletToShoot = Instantiate(bullet, transform.position, Quaternion.identity);
                bulletPool[i][j] = bulletToShoot;
                bulletPool[i][j].SetActive(false);
                bulletPool[i][j].transform.parent = transform;
            }
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
