using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpdate", menuName = "WeaponsUpdate")]
public class UpdateWeaponStatiscs : Updates
{
    [SerializeField] private int update;
    public override void ActivateImprovement()
    {
        PlayerController.instance.AplicationUpdate(Type, update);
    }
}
