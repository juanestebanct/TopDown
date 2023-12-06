using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : ProyectileWeapon
{
    [Header("MiniGun Stast")]
    [SerializeField][Range(0.0f, 3f)] private float distortion;
    [SerializeField] private float maxGunShoot;

    [Header("pooling")]
    private List<GameObject> bullets = new List<GameObject>();

    private float NowVelocityY;
    private float NowVelocityX;

    private bool canFire = true;
    private Rigidbody2D rb;

    private Vector3 tempDistortion;

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
    // Start is called before the first frame update
    private void Shootpool()
    {
        for (int counter = 0; counter < maxGunShoot; ++counter)
        {
            GameObject bullet = bullets.Find(b => !b.activeSelf);
            if (bullet == null)
            {
                bullet = Instantiate(Projectile, ProjectirePoint.position, transform.rotation);
                bullets.Add(bullet);
            }
            bullet.transform.rotation = transform.rotation;

            bullet.transform.position = ProjectirePoint.position + DistorcionShoot();
            bullet.SetActive(true);

            NowVelocityX = Mathf.Abs(rb.velocity.x);
            NowVelocityY = Mathf.Abs(rb.velocity.y);

            bullet.GetComponent<BullMiniGun>().MoreSpeed(rb.velocity.magnitude);

            canFire = false;
            StartCoroutine(Delay());
            AudioManager.instance.PlayClip(AudioManager.instance.Shoot);
        }
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
    private Vector3 DistorcionShoot()
    {
        float distortionX = Random.Range(-distortion, distortion);
        float distortionY = Random.Range(-distortion, distortion);
        float distortionZ = Random.Range(-distortion, distortion);
        tempDistortion = new Vector3(distortionX, distortionY, distortionZ);
        return tempDistortion;
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(TimeToFire);
        canFire = true;
    }
    private void OnDisable()
    {
        PController.Fire -= Shoot;
    }
    private void OnEnable()
    {
        PController.Fire += Shoot;
    }
}
