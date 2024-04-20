using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsCount : MonoBehaviour
{
    private float deltaTime = 0.0f;

    [SerializeField] private TextMeshProUGUI Fps;
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        int fps = Mathf.RoundToInt(1.0f / deltaTime);
        string text = $"FPS: {fps}";
        Fps.text = text;
    }

}
