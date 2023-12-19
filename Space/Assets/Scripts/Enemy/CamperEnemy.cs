using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CamperEnemy : Enemy
{
    [SerializeField] protected EnemyMovement movent;
    private void Awake()
    {
        movent = GetComponent<EnemyMovement>();
    }
    // Start is called before the first frame update
    public override void ResetMovent(Vector3 position)
    {
        MoventPatron patron = MoventPatron.Down;
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
