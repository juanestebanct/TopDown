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
        movent.ResetValues(position,patron);
    }
    public void ResetMovent(Vector3 position, MoventPatron patrons)
    {
        MoventPatron patron = patrons;
        movent.ResetValues(position, patron);
    }
    public override void ResiveDamage(float Damage)
    {
        Live -= Damage;
        print("live" + Live);
        if (Live > 0) return;
        Score.Instance.GetPoins(Point);
        AudioManager.instance.PlayClip(AudioManager.instance.Explocion);
        Desactive();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ResetZone")) Desactive();
    }
}
