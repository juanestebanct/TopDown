using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackleEnemy : Enemy
{
    [SerializeField] protected EnemyMovement movent;
    private void Awake()
    {
        movent = GetComponent<EnemyMovement>();
    }

    public override void ResetMovent(Vector3 position)
    {
        MoventPatron patron = MoventPatron.Tackle;
        movent.ResetValues(position, patron);
    }
    public override void TakeDamage(float damage)
    {
        Live -= damage;
        print("live" + Live);
        if (Live > 0) return;
        Score.Instance.GetPoins(Point);
        AudioManager.instance.PlayClip(AudioManager.instance.Explocion);
        Desactive();
    }

}
