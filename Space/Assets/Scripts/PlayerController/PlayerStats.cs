
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private void Awake()
    {
        live = maxLive;
        damageable = true;
    }
    public void ResiveDamage(float Damage)
    {
        if (!damageable) return; 
        live -= Damage;
        StartCoroutine(RelockTime());

        barLive.fillAmount = (live / maxLive);
        if (live < 1) PlayerController.instance.Reset();
    }
    IEnumerator RelockTime()
    {
        damageable = false;
        yield return new WaitForSeconds(timeActiveDamage);
        damageable = true;
    }
   
}
