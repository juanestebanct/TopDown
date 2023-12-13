using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canyon : ProyectileWeapon
{
    [Header("pooling")]
    private List<GameObject> bullets = new List<GameObject>();

    private float NowVelocity; 
    private bool canFire = true;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = PController.GetComponent<Rigidbody2D>();
        PController.Fire += Shoot;
        pooling();
    }
    protected override void Shoot()
    {
        if (!canFire) return;
        Shootpool();
    }

    private void Shootpool()
    {
        GameObject bullet = bullets.Find(b => !b.activeSelf);
        if (bullet == null)
        {
            bullet = Instantiate(Projectile, ProjectirePoint.position,transform.rotation);
            bullets.Add(bullet);
        }
        bullet.transform.rotation = transform.rotation;

        bullet.transform.position = ProjectirePoint.position;
        bullet.SetActive(true);

        if (rb.velocity.y >= 0) NowVelocity = rb.velocity.y;
        bullet.GetComponent<Bullet>().AddForce(ProjectirePoint.forward);
        bullet.GetComponent<Bullet>().MoreSpeed(rb.velocity.magnitude);

        canFire = false;
        StartCoroutine(Delay());
        AudioManager.instance.PlayClip(AudioManager.instance.Shoot);
    }
    private void pooling()
    {
        for (int i = 0; i < MaxBulletBullet; ++i)
        {
            GameObject bullet = Instantiate(Projectile, ProjectirePoint.position, transform.rotation);
            bullet.SetActive(false);
            bullets.Add(bullet); 
        }
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(TimeToFire);
        canFire = true;
    }

}
