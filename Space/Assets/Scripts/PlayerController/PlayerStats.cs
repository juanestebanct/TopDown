using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerStats : MonoBehaviour, IDamage
{
    [SerializeField] private float Live;
    public void ResiveDamage(float Damage)
    {
        Live-= Damage;
        if (Live < 0) PlayerController.instance.Reset();
    }
}
