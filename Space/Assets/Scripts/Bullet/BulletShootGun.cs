using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class BulletShootGun : Projectile
{
    [Header("Starts Bull")]
    [SerializeField] private float velocity;
    [SerializeField] private float force;

    private Rigidbody2D rb;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
    }
    private void Desactive()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (type)
        {
            case BulletType.Enemy:
                break;

            case BulletType.Player:

                if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Meteorite"))
                {
                    print(collision);
                    collision.gameObject.GetComponent<IDamage>().ResiveDamage(damage);
                    LostDrilling();
                    return;
                }
                gameObject.SetActive(false);
                break;
        }
    }
    private void OnEnable()
    {
        rb.velocity = new Vector2(0, 0);
    }
    private int RandomForce()
    {
        int randomForce = 0;
        randomForce = (int)Random.Range((force-30), (force+30));
        return randomForce;
    }
    public void AddForce(Vector2 tempForcce, Quaternion rotation)
    {
        tempForcce.Normalize();
        rb.AddForce(tempForcce * RandomForce(), ForceMode2D.Impulse);
        transform.rotation = rotation;      
        Invoke("Desactive",DesactivateTime);
       
    }
    public void MoreSpeed(float velocityPlayer)
    {
        velocity += velocityPlayer;
    }
}
