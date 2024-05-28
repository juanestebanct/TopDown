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
        MoventPatron patron = MoventPatron.ChaseToPlayer;
        movent.ResetValues(position, patron);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
