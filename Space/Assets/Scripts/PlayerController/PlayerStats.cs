using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(PlayerController))]
public class PlayerStats : MonoBehaviour, IDamage
{
    [SerializeField] private float maxLive;
    [SerializeField] private float live;

    [Header("Time damage")]
    [SerializeField] private float timeActiveDamage;
    private bool damageable; 

    [Header("Ui live")]
    [SerializeField] private Image barLive;
    [SerializeField] private TextMeshProUGUI liveText;
    private void Awake()
    {
        live = maxLive;
        damageable = true;
        liveText.text = $"{live}/{maxLive}";
    }
    public void ResiveDamage(float Damage)
    {
        if (!damageable) return; 
        live -= Damage;
        StartCoroutine(RelockTime());

        UpdateBarlive();
        if (live < 1) PlayerController.instance.Reset();
    }
    public void DesactiveDamage()
    {
        damageable = false;
        StartCoroutine(ReactiveShield());
    }
    public void MoreLive(int heal)
    {
        float TempLive = heal + live;

        if (TempLive >= maxLive) live = maxLive;
        else live = TempLive;

        UpdateBarlive();
    }
    public void UpdateMoreLive(int moreLive)
    {
        maxLive += moreLive;
        live = maxLive;
        UpdateBarlive();
    }

    private void UpdateBarlive()
    {
        barLive.fillAmount = (live / maxLive);
        liveText.text = $"{live}/{maxLive}";
    }
    IEnumerator RelockTime()
    {
        damageable = false;
        yield return new WaitForSeconds(timeActiveDamage);
        damageable = true;
    }
    IEnumerator ReactiveShield()
    {
        yield return new WaitForSeconds(10);
        damageable = true;
    }
   
}
