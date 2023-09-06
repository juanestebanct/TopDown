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
        Debug.Log("Se esta cargando el movimiento en camper enemigo");
        MoventPatron patron = MoventPatron.Down;
        movent.ResetValues(position, patron);
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
}
