using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourShootEnemy : Enemy
{
    [Header("TourShoot")]
    [SerializeField] protected EnemyMovement movent;
    [SerializeField] private ProyectileWeapon weapon;

    [SerializeField] private AudioSource spawnSource;
    [SerializeField] private AudioClip spawnClip;

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
        MoventPatron patron = MoventPatron.LookPlayer;
        movent.ResetValues(position, patron);
    }

    public override void ResiveDamage(float Damage)
    {
        Score.Instance.GetPoins(Point);
        AudioManager.instance.PlayClip(AudioManager.instance.Explocion);
        //Desactive();
    }
    public void OnEnable()
    {
        spawnSource.PlayOneShot(spawnClip);
    }

}
