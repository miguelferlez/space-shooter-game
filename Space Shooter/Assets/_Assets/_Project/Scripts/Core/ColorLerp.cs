using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour
{
    public Text text;
    public Color currentColor;
    public Color newColor;

    public void Awake()
    {
        text.color = GetComponent<Text>().color;
    }

    public void Update()
    {
        text.color = Color.Lerp(currentColor, newColor, Mathf.PingPong(Time.unscaledTime, 0.6f));
    }

}
