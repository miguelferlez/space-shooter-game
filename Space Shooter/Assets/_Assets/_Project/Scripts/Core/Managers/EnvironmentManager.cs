using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Managers
{
    public class EnvironmentManager : MonoBehaviour
    {
        private EnvironmentObject[] enviroScript;

        public void Awake()
        {
            enviroScript = GetComponentsInChildren<EnvironmentObject>();

        }

        public void OnEnable()
        {
            foreach (var obj in enviroScript)
            {
                obj.enabled = true;
            }
        }

        public void OnDisable()
        {
            foreach (var obj in enviroScript)
            {
                obj.enabled = false;
            }
        }

    }

}

