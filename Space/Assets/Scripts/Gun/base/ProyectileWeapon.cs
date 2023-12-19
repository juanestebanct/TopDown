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
    [SerializeField] protected int Drilling = 1;
    [SerializeField] protected int CicleShoot = 1;

    protected virtual void Shoot()
    {

    }
    public virtual void AddShoot(int cicleShoot )
    {
        CicleShoot += cicleShoot;
        print("mejoro la carencia");
    }
    public virtual void AddDilling(int newDirting)
    {
        Drilling += newDirting;
    }

    public void ReduceTimeFire(int coldown)
    {
        TimeToFire -= (coldown/100)*TimeToFire;
    }

}
