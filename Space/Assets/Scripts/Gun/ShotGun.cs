using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShotGun : ProyectileWeapon
{
    [Header("Shootgun Stast")]
    [SerializeField][Range(0.0f, 360)] private float maxRange;
    [SerializeField] private float maxGunShoot;
    [SerializeField] private float force;
    [SerializeField] private int DesplazM;

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
        StartCoroutine(startShoot());
    }

    private void Shootpool()
    {
        for (int counter = 0; counter < maxGunShoot; ++counter)
        {
            GameObject bullet = bullets.Find(b => !b.activeSelf);
            if (bullet == null)
            {
                bullet = Instantiate(Projectile, ProjectirePoint.position, ProjectirePoint.rotation);
                bullets.Add(bullet);
            }

            bullet.transform.rotation = transform.rotation;
            bullet.transform.position = ProjectirePoint.position;
            bullet.SetActive(true);

            float angle = (counter / (maxGunShoot - 1) - 0.5f) * 2 * maxRange;

            Vector2 direction = Quaternion.Euler(0, 0, (angle)) * ProjectirePoint.forward;
           // print(45*counter-45);
            float torque = Random.Range(500.0f, 1500.0f);

            bullet.GetComponent<BulletShootGun>().MoreSpeed(rb.velocity.magnitude);

            Quaternion Rotation = Quaternion.LookRotation(direction);

            bullet.GetComponent<Projectile>().DrillingWeapon(Drilling);
            bullet.GetComponent<BulletShootGun>().AddForce(direction, Rotation);
            bullet.GetComponent<Rigidbody2D>().velocity += new Vector2(0, NowVelocity);

        }
        AudioManager.instance.PlayClip(AudioManager.instance.Shoot);
    }

    private void OnDisable()
    {
        PController.Fire -= Shoot;
    }
    private void OnEnable()
    {
        PController.Fire += Shoot;
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
    private IEnumerator startShoot()
    {
        canFire = false;
        int intShoot = 0;
        StartCoroutine(Delay());
        while (CicleShoot > intShoot)
        {
            intShoot++;
            Shootpool();
            yield return new WaitForSeconds(0.1f);
            print("Repite shoot ");
        }
    }

}
