using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpdate", menuName = "WeaponsScript")]
public class UpdateWeapon : Updates
{
    public int weaponIndex;
    /// <summary>
    /// activa el arma
    /// </summary>
    /// <param name="Weapon"></param>
    public  override void ActivateImprovement(ProyectileWeapon Weapon)
    {
        Weapon.gameObject.SetActive(true);
        PlayerController.instance.ChangeWeapon(Weapon);
    }
}
