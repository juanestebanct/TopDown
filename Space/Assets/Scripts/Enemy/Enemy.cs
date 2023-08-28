using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected int Live;
    [SerializeField] protected int Damage;
    [SerializeField] protected GameObject DeadVfx;

    public void Desactive()
    {
        var deathFX = Instantiate(DeadVfx);
        deathFX.transform.position = transform.position;
        Destroy(deathFX, 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DesactiveZone")) Desactive();
    }

}
