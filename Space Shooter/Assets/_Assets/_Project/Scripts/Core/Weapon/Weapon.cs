using Core.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public int currentAmmo;
        private int currentMagazines;

        public int maxAmmo;
        public int maxMagazines;

        public Bullet bullet;
        public float speedVelocity;
        public Vector3 distance;

        public float rateOfFire;
        public float reloadTime;
        private float lastTimeShoot;

        public float dmg;

        private bool isShooting;
        private Vector3 position;

        protected virtual float dmgWeapon
        {
            get
            {
                return dmg;
            }
        }

        public void Awake()
        {
            currentAmmo = maxAmmo;
            currentMagazines = maxMagazines;
        }

        public bool HaveAmmoSurplus
        {
            get
            {
                if (currentAmmo <= 0)
                {
                    return false;
                }

                return true;

            }
        }

        public abstract void AddLevel();

        public virtual void Shoot(Vector3 spawnPosition)
        {
            position = spawnPosition;

            if (currentAmmo > 0)
            {
                if (Time.time <= lastTimeShoot + 1 / rateOfFire)
                {
                    return;
                }

                isShooting = true;
            }
            else
            {
                isShooting = false;
            }
        }

        public void Update()
        {
            if (isShooting == true)
            {
                var newBullet = BulletManager.Instance.GetBullet(bullet);
                newBullet.damage = dmgWeapon;
                newBullet.transform.position = position + distance;
                newBullet.GetComponent<Rigidbody>().velocity = new Vector3(0, speedVelocity, 0);
                lastTimeShoot = Time.time;
                currentAmmo--;

                isShooting = false;
            }
            else
            {
                if (Time.time >= lastTimeShoot + reloadTime && currentAmmo == 0)
                {
                    currentAmmo = maxAmmo;
                    currentMagazines--;
                }
            }
        }
    }
}