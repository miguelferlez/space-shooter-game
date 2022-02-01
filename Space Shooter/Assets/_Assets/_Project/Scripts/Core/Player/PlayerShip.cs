using Core.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.HUD;
using System;

namespace Core.Ships
{
    public class PlayerShip : Ship
    {
        public Weapon defaultWeapon;

        public float leftLimit, rightLimit, upperLimit, downLimit;
        private Vector2 xLimit, yLimit;

        public float CurrentNormalizedHealth
        {
            get
            {
                return currentHealth / maxHealth;
            }
        }
        public float CurrentAmmo
        {
            get
            {
                return CurrentWeapon.currentAmmo;
            }
        }
        public string CurrentGun
        {
            get
            {
                if (CurrentWeapon is Cannon)
                {
                    return "CANNON";
                }
                else
                {
                    if (CurrentWeapon is MachineGun)
                    {
                        return "MACHINE GUN";
                    }
                }

                return "nada joder";
            }
        }

        public HUDManager hudManager;

        public float CurrentChargePercent
        {
            get
            {
                if (CurrentWeapon is Cannon)
                {
                    return ((Cannon)CurrentWeapon).CurrentNormalizedChargeShoot;
                }

                return 0f;
            }
        }
        private Weapon CurrentWeapon
        {
            get
            {
                return currentWeapons[0];
            }
            set
            {
                currentWeapons[0] = value;
            }
        }

        public void Start()
        {
            Vector2 xVec = new Vector2();
            Vector2 yVec = new Vector2();

            Vector3 aux = Camera.main.ViewportToWorldPoint(new Vector3(leftLimit, downLimit, 0));
            xVec.x = aux.x;
            yVec.x = aux.y;

            aux = Camera.main.ViewportToWorldPoint(new Vector3(rightLimit, upperLimit, 0));
            xVec.y = aux.x;
            yVec.y = aux.y;

            xLimit = xVec;
            yLimit = yVec;

        }

        public void Update()
        {
            Vector3 nextPosition = new Vector3();
            nextPosition.x = Input.GetAxis("Horizontal");
            nextPosition.y = Input.GetAxis("Vertical");
            var rot = Quaternion.Euler(0, 0, 15 * -nextPosition.x);
            nextPosition = transform.position + (nextPosition * velocity * Time.deltaTime);


            if (nextPosition.x > xLimit.y)
            {
                nextPosition.x = xLimit.y;
            }
            if (nextPosition.x < xLimit.x)
            {
                nextPosition.x = xLimit.x;
            }

            if (nextPosition.y > yLimit.y)
            {
                nextPosition.y = yLimit.y;
            }
            if (nextPosition.y < yLimit.x)
            {
                nextPosition.y = yLimit.x;
            }

            transform.position = nextPosition;
            transform.rotation = rot;

            if (Input.GetKey("space") || Input.GetKeyDown("space") || Input.GetKeyUp("space"))
            {
                Shoot();
            }

        }

        //public void ChangeWeapon(Weapon newWeapon)
        //{
        //    if (weapons[0].GetType() == newWeapon.GetType())
        //    {
        //        CurrentWeapon.AddLevel();
        //    }
        //    else
        //    {
        //        weapons = new Weapon[] { newWeapon };
        //    }

        //    weapons = new Weapon[] { newWeapon };
        //}

        public override void Shoot()
        {
            base.Shoot();
            /*
            if (!currentWeapon.HaveAmmoSurplus)
            {
                ChangeWeapon(defaultWeapon);
            }
            */

        }

        protected override void Die()
        {

            base.Die();
            hudManager.ShowMenu();
        }

    }
}