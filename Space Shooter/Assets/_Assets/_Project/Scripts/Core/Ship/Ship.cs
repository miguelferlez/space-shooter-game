using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Weapons;

namespace Core.Ships
{
    public abstract class Ship : MonoBehaviour
    {
        private const float colorFade = 0.5f;

        public float velocity;
        public Weapon[] weapons;
        public float maxHealth;
        protected Weapon[] currentWeapons;
        public float currentHealth;

        public bool Enabled
        {
            get
            {
                return gameObject.activeSelf;
            }
        }
        private Coroutine current;

        private Animator anim;
        private Collider col;

        public void Awake()
        {
            gameObject.SetActive(true);
            currentHealth = maxHealth;
            currentWeapons = new Weapon[weapons.Length];
            for (int i = 0; i < weapons.Length; i++)
            {
                currentWeapons[i] = Instantiate(weapons[i], transform);
            }

            anim = GetComponent<Animator>();
            col = GetComponent<Collider>();
        }

        public virtual void ChangeHealth(float amount)
        {
            currentHealth -= amount;

            if (current != null)
            {
                StopCoroutine(current);
            }
            current = StartCoroutine(damageColor(amount));

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        IEnumerator damageColor(float amount)
        {
            var shipSprite = GetComponent<SpriteRenderer>();

            var hitColor = new Vector3(1f, 150f / 255f, 150f / 255f);
            shipSprite.color = new Color(hitColor.x, hitColor.y, hitColor.z);

            while (amount > 0)
            {
                hitColor = Vector3.MoveTowards(hitColor, Vector3.one, colorFade * Time.deltaTime);
                shipSprite.color = new Color(hitColor.x, hitColor.y, hitColor.z);
                amount -= colorFade * Time.deltaTime;
                yield return null;
            }

            shipSprite.color = Color.white;

            current = null;
        }

        protected virtual void Die()
        {
            col.enabled = false;
            anim.SetBool("death", true);
            StartCoroutine(DelayDeath());
            if (currentWeapons != null)
            {
                foreach (var wep in currentWeapons)
                {
                    Destroy(wep);
                }
            }
        }

        IEnumerator DelayDeath()
        {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }

        public virtual void Shoot()
        {
            foreach (var wep in currentWeapons)
            {
                wep.Shoot(transform.position);
            }
        }

    }
}

