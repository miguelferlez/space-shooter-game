using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Weapons
{
    public class Cannon : Weapon
    {
        public float chargePercent;
        public float minDamagePercent;
        public float maxDamagePercent;
        public float dmgPercentPersec;

        public float CurrentNormalizedChargeShoot
        {
            get
            {
                return chargePercent / maxDamagePercent;
            }
        }

        protected override float dmgWeapon
        {
            get
            {
                return dmgPercentPersec * dmg / 100;
            }
        }

        public override void AddLevel()
        {
            throw new System.NotImplementedException();
        }

        public override void Shoot(Vector3 spawnPosition)
        {
            if (Input.GetKeyDown("space"))
            {
                chargePercent = minDamagePercent;
            }

            if (Input.GetKey("space"))
            {
                chargePercent += dmgPercentPersec * Time.deltaTime;
            }

            if(Input.GetKeyUp("space"))
            {
                if (dmgPercentPersec > maxDamagePercent)
                {
                    dmgPercentPersec = maxDamagePercent;
                }

                base.Shoot(spawnPosition);

            }
        }

    }
}