using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Ships
{
    public class Squadron : MonoBehaviour                                                 //COSAS NUEVAS DE HOY
    {
        public Transform[] posMinions;
        public Transform[] posMovement;
        public float velocity;
        public Minion minionModel;
        public Minion eliteModel;

        public Minion[] enemies;
        public int enemySpawn;
        public int eliteSpawn;
        public int speedSpawn;

        public Transform squad;
        private Transform target;
        private int targetIndex;

        public bool rotated;
        public int rotationAngle;

        public bool isDead;     //para decir cuando se muere

        public void Awake()
        {
            squad.position = posMovement[0].position;
            targetIndex = 1;
            target = posMovement[targetIndex];
            InstanceMinion(enemySpawn, eliteSpawn);
        }

        public void InstanceMinion(int defaultEnemies)
        {
            if(defaultEnemies <= posMinions.Length)
            {
                for (int i = 0; i < defaultEnemies; i++)
                {
                    SpawnInPosition(i, minionModel);
                }
            }
            else
            {
                return;
            }
        }

        private void SpawnInPosition(int pos, Minion minionModel)
        {
            enemies[pos] = Instantiate(minionModel);
            enemies[pos].transform.SetParent(posMinions[pos]);
            enemies[pos].transform.localPosition = new Vector3(0, 0, 0);
        }

        public void InstanceMinion(int defaultEnemies, int eliteSpawn)
        {
            
            if(eliteSpawn <= posMinions.Length)
            {
                for (int i = posMinions.Length - 1; i > posMinions.Length - eliteSpawn - 1; i--)
                {
                    SpawnInPosition(i, eliteModel);
                }

                if (defaultEnemies + eliteSpawn > posMinions.Length)
                {
                    InstanceMinion(posMinions.Length - eliteSpawn);
                }
                else
                {
                    InstanceMinion(defaultEnemies);
                }
            }

            
        }

        public void SquadMovement(Transform target)
        {
            squad.position = Vector3.MoveTowards(squad.position, target.position, velocity * Time.deltaTime);
            Vector3 squadRotation = new Vector3(0, 0, 0);
            var rot = Quaternion.Euler(0, 0, rotationAngle * velocity *Time.deltaTime);
            
            if (rotated)
            {
                squad.rotation = squad.rotation * rot;

                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].transform.rotation = Quaternion.Euler(0, 0, 180);
                }

            }
            else
            {
                return;
            }

        }

        public void Update()
        {
            if(target.position == squad.position)
            {
                targetIndex++;
                if(targetIndex == posMovement.Length)
                {
                    targetIndex = 0;
                }
                target = posMovement[targetIndex];
            }

            for (int i = 0; i < enemySpawn + eliteSpawn; i++)
            {
                if (enemies[i].Enabled)
                {
                    break;
                }
                if (i == enemySpawn + eliteSpawn - 1 && !enemies[i].Enabled)
                {
                    isDead = true;      //aqui decimos que es true y desactivamos el objeto, no pongas destroy porque despues en LevelManager no va a coger algo que ya se ha destruido y no existe
                    gameObject.SetActive(false);
                }
            }

            SquadMovement(target);

        }
    }
}