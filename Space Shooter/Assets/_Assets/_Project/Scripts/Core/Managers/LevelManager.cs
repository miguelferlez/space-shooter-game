using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Ships;
using Core.HUD;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Core.Managers                                                 
{
    [System.Serializable]
    public class SpawnInfo
    {
        public Squadron squads;
        public float spawnTime;
        public bool isSpawned;
        public int position; 
    }

    [System.Serializable]
    public class HealthBonus
    {
        public GameObject healthBarrel;
        public int minTime;
        public int maxTime;
        public float randomTime;
        public bool objectSpawned;
    }


    public class LevelManager : MonoBehaviour
    {
        public List<SpawnInfo> squadSpawnInfo;
        public List<HealthBonus> healthObjectInfo;

        private Squadron[] squadrons;      
        private int cont = 0;  

        public HUDManager hudManager;

        public void Awake()
        {
            squadrons = new Squadron[squadSpawnInfo.Count];
        }

        public void Update()
        {
            
            foreach (var info in squadSpawnInfo)
            {
                if (Time.timeSinceLevelLoad > info.spawnTime && info.isSpawned == false)
                {
                    var sq = Instantiate(info.squads);
                    squadrons[cont] = sq;       
                    info.position = cont;     
                    cont++;
                    info.isSpawned = true;
                }
            }

            if (Time.timeSinceLevelLoad >= squadSpawnInfo[0].spawnTime)
            {
                SquadDestroyed();
            }

            foreach (var obj in healthObjectInfo) 
            {

                if (obj.randomTime == 0)
                {
                    obj.randomTime = Random.Range(obj.minTime, obj.maxTime);
                }

                if(Time.timeSinceLevelLoad > obj.randomTime && obj.objectSpawned == false)
                {
                    Vector3 position = new Vector3(Random.Range(-3f, 3f), 8, 0);
                    Instantiate(obj.healthBarrel, position, Quaternion.identity);
                    obj.objectSpawned = true;
                }
            }
        }

        public void SquadDestroyed()
        {
            for (int i = 0; i < squadrons.Length; i++)
            {
                if(squadrons[i] != null)
                {
                    if (!squadrons[i].isDead)
                    {
                        return;
                    }
                }
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //hudManager.ShowVictory();
        }
    }
}
