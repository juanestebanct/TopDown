using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateControllers : MonoBehaviour
{
    [SerializeField] private UpdateWeapon[] WeaponsScript;
    [SerializeField] private Updates[] UpdatesScript;
    [SerializeField] private CardUpdate[] cards;
    [SerializeField] private GameObject UpdateUi;

    private bool isPaused;
    void Start()
    {
        isPaused = false;
        TriggerPause();
        SelectWeapons();
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
        List<Updates> temUpdates = new List<Updates>(UpdatesScript);

        for (int i = 0; i < cards.Length; i++)
        {
            int tempIndex = Random.Range(0, temUpdates.Count);

            cards[i].UpdateCard(temUpdates[tempIndex]);
            temUpdates.RemoveAt(tempIndex);
        }
    }

    public void OpenMenuUpdate()
    {
        TriggerPause();
        NewUpdates();
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
