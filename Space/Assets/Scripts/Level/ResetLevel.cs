using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    [SerializeField] private bool isGameOver;

    private void Start()
    {
        //PlayerController.instance.Reset += RestartLevel;
    }
    public void RestartLevel()
    {
        print("reinicio");
        isGameOver = true;
        if (isGameOver)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
    }
}
