using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [SerializeField] private float forceReturn = 50;

    private bool desactiveMove;
    private Rigidbody2D rb;
    private Collider2D collition;
    private Coroutine coroutine;
    private void Start()
    {
        collition = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
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
        if (Move != null && !desactiveMove)
        {
            Move();
        }
    }

    public virtual void ResiveDamage(float Damage)
    {
      
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamage>().ResiveDamage(Damage);
            gameObject.SetActive(false);
            AudioManager.instance.PlayClip(AudioManager.instance.Explocion);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {

            Vector3 pushDirection = transform.position - collision.gameObject.transform.position;
            float deviationAngle = Random.Range(-80f, 80f);
            Quaternion rotation = Quaternion.Euler(deviationAngle, 0, 0);
            Vector3 rotatedDirection = rotation * pushDirection;

            //rb.AddForce(rotatedDirection.normalized  * forceReturn, ForceMode2D.Impulse);
        }
        if (collision.gameObject.CompareTag("ResetZone")) Desactive();
    }
    private IEnumerator ActivateForce()
    {
        collition.enabled = false;

        yield return new WaitForSeconds(0.3f);
        collition.enabled = true;
    }

}
