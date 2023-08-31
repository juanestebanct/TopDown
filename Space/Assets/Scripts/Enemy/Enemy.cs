using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


[RequireComponent(typeof(EnemyMovement))]
public abstract class Enemy : MonoBehaviour ,IDamage
{
    [SerializeField] protected int Live;
    [SerializeField] protected int Damage;
    [SerializeField] protected int Point;
    [SerializeField] protected GameObject DeadVfx;

    public virtual void ResetMovent(Vector3 position)
    {
        //var deathFX = Instantiate(DeadVfx);
        //deathFX.transform.position = transform.position;
        //Destroy(deathFX, 1f);
        gameObject.SetActive(false);
    }
    public virtual void Desactive()
    {

    }

    public virtual void ResiveDamage(float Damage)
    {
       
    }



}
