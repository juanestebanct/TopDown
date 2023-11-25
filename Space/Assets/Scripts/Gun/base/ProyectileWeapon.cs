using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileWeapon : MonoBehaviour
{
    [Header("Weapon Reference")]
    [SerializeField] protected PlayerController PController;
    [SerializeField] protected GameObject Projectile;
    [SerializeField] protected Transform ProjectirePoint;
    [SerializeField] protected float TimeToFire = 2f;
    [SerializeField] protected int MaxBulletBullet;

    protected virtual void Shoot()
    {

    }
}
