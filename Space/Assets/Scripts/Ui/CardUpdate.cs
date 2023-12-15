using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardUpdate : MonoBehaviour
{
    
    [Header("UI Card")]
    [SerializeField] private Button activateButton;
    [SerializeField] private Image imageUpdate;
    [SerializeField] private TextMeshProUGUI title, description;
    [SerializeField] private UpdateControllers updateControllers;
    private Updates update;
    void Start()
    {
        updateControllers = FindObjectOfType<UpdateControllers>();
    }
    public void UpdateCard(Updates temUpdates)
    {
        print("paso por aca");
        update = temUpdates;
        title.text = update.Title;
        description.text = update.Description;
        imageUpdate.sprite =update.ImageUpdate;
        activateButton.onClick.RemoveAllListeners();

        if (update is UpdateWeapon)
        {
            activateButton.onClick.AddListener(EventTemp);
        }
        else activateButton.onClick.AddListener(temUpdates.ActivateImprovement);
        activateButton.onClick.AddListener(SelectUpdate);
    }
    public void EventTemp()
    {
        update.ActivateImprovement(PlayerController.instance.Weapons[(update as UpdateWeapon).weaponIndex]);
    }
    private void SelectUpdate()
    {
        print("Prende?");
        updateControllers.TriggerPause();
    }
}
