using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CircleBulletSpawner : BulletPatternSpawner
{
    public float RotationSpeed;
    public int SeccondWave;
    public float SeccondWaveTimer;
    public List<Transform> SpawnPositions;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
    }

    public override IEnumerator bulletPatternShot()
    {
        for (int y = 0; y < SeccondWave; y++)
        {
            for (int i = 0; i < Waves; i++)
            {
                for (int x = 0; x < SpawnPositions.Count; x++)
                {
                    Bullet bullet = bulletSpawner.BasicBulletPool.Get();
                    bullet.transform.position = SpawnPositions[x].position;
                    bullet.DirectionVector = SpawnPositions[x].localPosition;
                }

                currentWave++;
                yield return new WaitForSecondsRealtime(WaveTimer);
            }
            yield return new WaitForSecondsRealtime(SeccondWaveTimer);
        }

        bulletSpawner.SpawnRandomBulletPattern();
    }

}
