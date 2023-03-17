using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrameRateLogger : MonoBehaviour
{
    private float deltaTime = 0.0f;

    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} fps", fps);
        Debug.Log(text);
    }
}
