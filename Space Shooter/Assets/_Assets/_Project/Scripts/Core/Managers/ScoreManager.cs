using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Managers
{
    public class ScoreManager : MonoBehaviour
    {

        public static int currentScore;
        public Text pointText;
        public Text pointDead;
        public Text pointWin;

        public void Awake()
        {
            DontDestroyOnLoad(this);
            currentScore = 0;
        }

        public void Update()
        {
            pointText.text = currentScore.ToString();
            pointDead.text = currentScore.ToString();
            pointWin.text = currentScore.ToString();

        }
    }
}


