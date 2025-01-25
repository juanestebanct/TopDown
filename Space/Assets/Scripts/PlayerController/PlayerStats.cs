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

    public void TakeDamage(float damage)
    {
        if (!damageable) return; 
        live -= damage;
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
        float tempLive = heal + live;

        if(tempLive >= maxLive) live = maxLive;
        else live = tempLive;

        UpdateBarlive();
    }
    public void UpdateMoreLive(int moreLive)
    {
        maxLive += moreLive;
        live = maxLive;
        UpdateBarlive();
    }
    public void GetIuElement(Image refImage, TextMeshProUGUI textLive)
    {
        barLive = refImage;
        liveText = textLive;
        live = maxLive;
        damageable = true;
        liveText.text = $"{live}/{maxLive}";
    }

    private void UpdateBarlive()
    {
        barLive.fillAmount = (live / maxLive);
        liveText.text = $"{live}/{maxLive}";
    }
    public IEnumerator RelockTime()
    {
        damageable = false;
        yield return new WaitForSeconds(timeActiveDamage);
        damageable = true;
    }
    private IEnumerator ReactiveShield()
    {
        yield return new WaitForSeconds(10);
        damageable = true;
    }
   
}
