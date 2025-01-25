using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class BossChaster : Enemy
{
    [Header("BossChaster")]
    [SerializeField] protected MoventPatron[] patrons;
    [SerializeField] protected EnemyMovement movent;
    [SerializeField] private ProyectileWeapon weapon,shotgun;
    [SerializeField] private float delayChange,delayShoot;

    [SerializeField] private AudioSource spawnSource;
    [SerializeField] private AudioClip spawnClip;

    private GenerationEnemiesNormal generationEnemies;
    private PlayerController reference;
    private bool fire;
    ///Desde aqui Se va a ir cambiando las cosas,charter y shooter
    ///
    private void Awake()
    {
        movent = GetComponent<EnemyMovement>();
        reference = movent.GetRefenecePlayer();
    }
    private void Start()
    {
        movent.ChangePatron(patrons[0]);
        StartCoroutine(ActivateForce());
    }
    private void Update()
    {
        if (weapon && fire)
            weapon.Shoot();
    }
    public override void ResetMovent(Vector3 position)
    {
        MoventPatron patron = MoventPatron.LookPlayer;
        movent.ResetValues(position, patron);
    }

    public override void TakeDamage(float damage)
    {
        Live -= damage;
        print("live" + Live);
        if (Live > 0) return;

        print("Va a morir");
        Score.Instance.GetPoins(Point);
        generationEnemies.ActiveEvent();
        Desactive();
    }
    public void OnEnable()
    {
        spawnSource.PlayOneShot(spawnClip);
    }
    public void Ref(GenerationEnemiesNormal generation)
    {
        generationEnemies = generation;
    }

    IEnumerator ActivateForce()
    {
        print("hola");
        yield return new WaitForSeconds(delayChange);

        int range = Random.Range(0, patrons.Length);
        if (patrons[range] == MoventPatron.MovenSHoot) fire = true;
        else fire = false;

        movent.ChangePatron(patrons[range]);

        StartCoroutine(ActivateForce());
        if(patrons[range] == MoventPatron.Tackle) StartCoroutine(ShotShogun());
    }
    IEnumerator ShotShogun()
    {
        yield return new WaitForSeconds(delayShoot);
        shotgun.Shoot();

    }
    private void OnDisable()
    {
        StopCoroutine("ActivateForce");
    }

}

