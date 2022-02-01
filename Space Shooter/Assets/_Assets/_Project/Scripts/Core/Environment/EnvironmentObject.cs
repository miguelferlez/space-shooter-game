using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Managers
{
    public class EnvironmentObject : MonoBehaviour
    {
        Material material;
        Vector2 textureMove;
        public float xVelocity, yVelocity;

        public void Awake()
        {
            material = GetComponent<Renderer>().material;
        }

        public void Update()
        {
            material.mainTextureOffset += new Vector2(xVelocity, yVelocity) * Time.deltaTime;
        }
    }
}