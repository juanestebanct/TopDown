using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Obstacle : MonoBehaviour, IDamage
{
    [Header("Stats Obstacle")]
    [SerializeField] protected int Live = 1;
    [SerializeField] protected int Damage;
    [SerializeField] protected int Point;
    [SerializeField] protected GameObject DeadVfx;
    void Start()
    {
        
    }

    public virtual void Desactive()
    {
        //var deathFX = Instantiate(DeadVfx);
        //deathFX.transform.positionRay = transform.positionRay;
        //Destroy(deathFX, 1f);
        gameObject.SetActive(false);
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
}
