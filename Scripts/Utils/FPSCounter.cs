using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    float deltaTime = 0.0f;
    private int fps;
    private int objectCount;

    private void Start()
    {
        StartCoroutine(FPSCoroutine());
    }

    private IEnumerator FPSCoroutine()
    {
        while(true)
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            fps = Mathf.RoundToInt(1.0f / deltaTime);
            objectCount = FindObjectsOfType<GameObject>().Length;
            text.text = $"FPS: {fps}\nObj count: {objectCount}";

            yield return new WaitForSeconds(0.5f);
        }
    }
}
