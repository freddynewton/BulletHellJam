using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayerBulletPattern : BulletPatternSpawner
{
    public override IEnumerator bulletPatternShot()
    {
        for (int i = 0; i < Waves; i++)
        {
            for (int j = 0; j < BulletAmount; j++)
            {
                Bullet bullet = bulletSpawner.BasicBulletPool.Get();
                bullet.transform.position = transform.position;
                bullet.DirectionVector = transform.position - bulletSpawner.playerManager.transform.position;
                yield return new WaitForSecondsRealtime(BulletSpawnTimeOffset);

            }

            yield return new WaitForSecondsRealtime(WaveTimer);
        }
    }
}
