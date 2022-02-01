using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Ships;
using Core.Weapons;
using UnityEngine.UI;

namespace Core.HUD
{
    public class HUDManager : MonoBehaviour
    {
        public PlayerShip player;

        public Text ammoText;
        public Text gunText;
        public Slider healthBar;
        public Slider cannonBar;

        public GameObject deadMenu;
        public GameObject winMenu;

        public GameObject pausePrefab;
        private GameObject pauseMenu;
        private bool isPaused;

       
        public void Awake()
        {
            pauseMenu = Instantiate(pausePrefab);
        }

        public void Start()
        {
            if (pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(false);
            }
        }

        public void Update()
        {
            ammoText.text = player.CurrentAmmo.ToString();
            gunText.text = player.CurrentGun;
            healthBar.value = player.CurrentNormalizedHealth;
            cannonBar.value = player.CurrentChargePercent;

            if (gunText.text == "MACHINE GUN")
            {
                gunText.color = new Color(71f / 255f, 183f / 255f, 1f);
            }

            if(gunText.text == "CANNON")
            {
                gunText.color = new Color(24f / 255f, 184f / 255f, 175f / 255f);
            }


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    ContinueGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void ShowMenu()
        {
            StartCoroutine(DelayMenu());
        }


        IEnumerator DelayMenu()
        {
            yield return new WaitForSeconds(0.5f);
            var newMenu = Instantiate(deadMenu);
            Time.timeScale = 0f;
        }

        public void ShowVictory()
        {
            StartCoroutine(WinnerMenu());
        }

        IEnumerator WinnerMenu()
        {
            yield return new WaitForSeconds(0.5f);
            var newMenu = Instantiate(winMenu);
            Time.timeScale = 0f;
        }

        public void PauseGame()
        {

            isPaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        public void ContinueGame()
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }
}

