using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Ships;

namespace Core.Weapons
{
    public class Bullet : MonoBehaviour
    {
        public float damage;
        public bool enabled;

        public void Init()
        {
            enabled = true;
            gameObject.SetActive(true);
        }

        public void Finish()
        {
            enabled = false;
            gameObject.SetActive(false);
        }

        public void Update()
        {
            Vector3 bulletLimit = Camera.main.WorldToViewportPoint(transform.position);
            if (bulletLimit.y >= 1 || bulletLimit.y <= 0)
            {
                Finish();
            }
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && tag == "AllyBullets")
            {
                return;
            }
            if (other.tag == "Enemy" && tag == "EnemyBullets")
            {
                return;
            }
            if (other.tag == "AllyBullets" || other.tag == "EnemyBullets")
            {
                return;
            }

            if (other.tag == "AllyBullets" || other.tag == "PowerUp")
            {
                return;
            }
            if (other.tag == "PowerUp" || other.tag == "EnemyBullets")
            {
                return;
            }

            Ship ship = other.GetComponent<Ship>();
        
            if (ship == null)
            {
                return;
            }
            ship.ChangeHealth(damage);

            Finish();           
        }
    }
}