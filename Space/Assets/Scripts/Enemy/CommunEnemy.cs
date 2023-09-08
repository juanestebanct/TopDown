using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class CommunEnemy : Enemy
{
    [SerializeField] protected EnemyMovement movent;

    private void Awake()
    {
        movent = GetComponent<EnemyMovement>();
    }
    public override void ResetMovent(Vector3 position)
    {
        MoventPatron patron = (MoventPatron)Random.Range(0, 2);
        print(patron);
        movent.ResetValues(position,patron);
    }
    public override void Desactive()
    {
        gameObject.SetActive(false);
        //var deathFX = Instantiate(DeadVfx);
        //deathFX.transform.positionRay = transform.positionRay;
        //Destroy(deathFX, 1f);
    }
    public override void ResiveDamage(float Damage)
    {
        Score.Instance.GetPoins(Point);
        Desactive();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ResetZone")) Desactive();
    }
}
