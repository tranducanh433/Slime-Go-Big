using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileController
{
    public class Bullet : MonoBehaviour
    {
        /// <summary>
        /// Shoot bullet around
        /// </summary>
        /// <param name="bulletPrefab"></param>
        /// <param name="shootPos">Start at</param>
        /// <param name="numberOfBullet"></param>
        /// <param name="startAtAngle"></param>
        /// <returns></returns>
        public static GameObject[] Spread(GameObject bulletPrefab, Transform shootPos, int numberOfBullet, float startAtAngle)
        {
            List<GameObject> bullets = new List<GameObject>();
            float angleStep = 360 / numberOfBullet;
            float currentAngle = startAtAngle;

            for (int i = 0; i < numberOfBullet; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, shootPos.position, Quaternion.Euler(0, 0, currentAngle));
                bullets.Add(bullet);

                currentAngle += angleStep;
            }

            return bullets.ToArray();
        }
        /// <summary>
        /// Shoot bullet around but random angle
        /// </summary>
        /// <param name="bulletPrefab"></param>
        /// <param name="shootPos"></param>
        /// <param name="numberOfBullet"></param>
        /// <param name="minAngle"></param>
        /// <param name="maxAngle"></param>
        /// <returns></returns>
        public static GameObject[] SpreadRandom(GameObject bulletPrefab, Transform shootPos, int numberOfBullet, float minAngle = 0, float maxAngle = 360)
        {
            List<GameObject> bullets = new List<GameObject>();

            for (int i = 0; i < numberOfBullet; i++)
            {
                float angle = Random.Range(minAngle, maxAngle);
                GameObject bullet = Instantiate(bulletPrefab, shootPos.position, Quaternion.Euler(0, 0, angle));
                bullets.Add(bullet);
            }

            return bullets.ToArray();
        }

    }
}