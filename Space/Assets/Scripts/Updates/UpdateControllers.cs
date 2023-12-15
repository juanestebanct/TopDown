using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateControllers : MonoBehaviour
{
    [SerializeField] private UpdateWeapon[] WeaponsScript;
    [SerializeField] private CardUpdate[] cards;
    [SerializeField] private GameObject UpdateUi;

    private bool isPaused;
    void Start()
    {
        isPaused = false;
        TriggerPause();
        SelectWeapons();
        Time.timeScale = 0;
    }
    public void SelectWeapons()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            print("a");
            cards[i].UpdateCard(WeaponsScript[i]);
        }
    }
    public void NewUpdates()
    {

    }
    public void TriggerPause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            UpdateUi.gameObject.SetActive(false);
            isPaused = false;
        }
        else if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            UpdateUi.gameObject.SetActive(true);
            isPaused = true;
        }
    }
}
