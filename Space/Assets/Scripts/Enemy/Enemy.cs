using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


[RequireComponent(typeof(EnemyMovement))]
public abstract class Enemy : MonoBehaviour, IDamage
{
    public Action Move;
    [SerializeField] protected int Level;
    [SerializeField] protected float MaxLive;
    [SerializeField] protected float Live;
    [SerializeField] protected int Damage;
    [SerializeField] protected int Point;
    [SerializeField] protected GameObject DeadVfx;
    private void Start()
    {
        Live = MaxLive;
        Level = 1;
    }
    public virtual void ResetMovent(Vector3 position)
    {
        //var deathFX = Instantiate(DeadVfx);
        //deathFX.transform.positionRay = transform.positionRay;
        //Destroy(deathFX, 1f);
        gameObject.SetActive(false);
    }
    public virtual void Desactive()
    {
        //var deathFX = Instantiate(DeadVfx);
        //deathFX.transform.positionRay = transform.positionRay;
        //Destroy(deathFX, 1f);
        Live = MaxLive;
        gameObject.SetActive(false);
    }
    public virtual void FixedUpdate()
    {
        if (Move != null)
        {
            Move();
        }
    }

    public virtual void ResiveDamage(float Damage)
    {
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamage>().ResiveDamage(Damage);
            gameObject.SetActive(false);
            AudioManager.instance.PlayClip(AudioManager.instance.Explocion);
        }
        if (collision.gameObject.CompareTag("ResetZone")) Desactive();
    }
    public void UpdateLevel(int currentLevel)
    {
        if (Level != currentLevel) 
        {
            int tempLevel = currentLevel - Level;
            print("next nevel" + currentLevel + "" + Level);
            Level = currentLevel;
            MaxLive += tempLevel * 2;
            Damage += tempLevel * 2;
        }
    }

}
