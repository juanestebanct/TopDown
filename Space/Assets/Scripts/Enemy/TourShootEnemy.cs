using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourShootEnemy : Enemy
{
    [Header("TourShoot")]
    [SerializeField] protected EnemyMovement movent;
    [SerializeField] private ProyectileWeapon weapon;

    private PlayerController reference;
    ///Desde aqui se desaciva el movent y se coloca a uno que va a girar 45 grados en la direccion y desde aqui se va a 
    ///disparar
    ///
    private void Awake()
    {
        movent = GetComponent<EnemyMovement>();
        reference = movent.GetRefenecePlayer();
    }

    private void Update()
    {
        if (weapon)
        weapon.Shoot();
    }
    public override void ResetMovent(Vector3 position)
    {
        movent.ResetValues(position, MoventPatron.MovenSHoot);
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
