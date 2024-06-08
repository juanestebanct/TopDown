using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackleEnemy : Enemy
{
    [SerializeField] protected EnemyMovement movent;
    void Start()
    {
        
    }

    public override void ResetMovent(Vector3 position)
    {
        MoventPatron patron = MoventPatron.Tackle;
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

}
