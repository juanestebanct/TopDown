using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Canyon : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectirePoint;
    [SerializeField] private float TimeToFire = 2f;

    [Header("pooling")]
    [SerializeField] private int maxBulletBullet;
    private List<GameObject> bullets = new List<GameObject>();

    private PlayerController playerController;
    private float NowVelocity; 
    private bool canFire = true;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController= GetComponent<PlayerController>();
        playerController.Fire+=Shoot;
        pooling();
    }
    private void Shoot()
    {
        if (!canFire) return;
        Shootpool();
    }

    private void Shootpool()
    {
        GameObject bullet = bullets.Find(b => !b.activeSelf);
        if (bullet == null)
        {
            bullet = Instantiate(projectile, projectirePoint.position,transform.rotation);
            bullets.Add(bullet);
        }
        bullet.transform.rotation = transform.rotation;

        bullet.transform.position = projectirePoint.position;
        bullet.SetActive(true);

        if (rb.velocity.y >= 0) NowVelocity = rb.velocity.y;
        bullet.GetComponent<Rigidbody2D>().velocity += new Vector2(0, NowVelocity);

        canFire = false;
        StartCoroutine(Delay());
    }
    private void pooling()
    {
        for (int i = 0; i < maxBulletBullet; ++i)
        {
            GameObject bullet = Instantiate(projectile, projectirePoint.position, transform.rotation);
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
