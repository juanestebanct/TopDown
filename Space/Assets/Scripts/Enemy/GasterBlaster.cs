using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasterBlaster : Enemy
{
    [Header ("Gaster Stats")]
    [SerializeField] protected EnemyMovement movent;
    [SerializeField] private float timeToFire, currentime;
    [SerializeField] private RayGun rayGun;

    [SerializeField] private AudioSource spawnSource;
    [SerializeField] private AudioClip spawnClip;
    private void Awake()
    {
        movent = GetComponent<EnemyMovement>();
        rayGun = GetComponent<RayGun>();
        
    }
    private void Update()
    {
        if (currentime >= timeToFire)
        {
            MoventPatron patron = MoventPatron.GoBack;
            movent.ChangeRute(patron);
            rayGun.DisableForcedLacer(true);
            currentime = 0;
        }
        currentime += Time.deltaTime;
    }
    // Start is called before the first frame update
    public override void ResetMovent(Vector3 position)
    {
        currentime = 0;
        rayGun.DisableForcedLacer(false);
        MoventPatron patron = MoventPatron.LookPlayer;
        movent.ResetValues(position, patron);
    }

    public override void ResiveDamage(float Damage)
    {
        Score.Instance.GetPoins(Point);
        AudioManager.instance.PlayClip(AudioManager.instance.Explocion);
        Desactive();
    }
    public void OnEnable()
    {
        spawnSource.PlayOneShot(spawnClip);
    }



}
