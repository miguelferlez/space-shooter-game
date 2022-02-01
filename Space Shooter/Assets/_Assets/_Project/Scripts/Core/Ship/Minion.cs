using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Weapons;
using Core.Managers;

namespace Core.Ships
{
    public class Minion : Ship
    {
        public int pointsValue;

        public void Update()
        {
            Shoot();
        }

        protected override void Die()
        {

            ScoreManager.currentScore += pointsValue;           
            base.Die();
        }
    }
}