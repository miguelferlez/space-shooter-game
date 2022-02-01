using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Weapons
{
    public class WeaponConfiguration : ScriptableObject
    {
        public float rateOfFire;
        public int clipSize;
        public float velocity;
        public float damage;

        public bool doPercentageDamage;
        public float percentageDamage;

        //solo aplicable a machineGun
        //public float dispersionAngle;
    }
}