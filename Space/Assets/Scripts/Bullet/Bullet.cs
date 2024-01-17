using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Bullet : Projectile
{
    [Header("Starts Bull")]
    [SerializeField] private float velocity;
    [SerializeField] private float force;

    private Rigidbody2D rb;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
    public void MoreSpeed(float velocityPlayer)
    {
        velocity += velocityPlayer;
    }
    public void AddForce(Vector2 tempForce)
    {
        tempForce.Normalize();
        rb.AddForce(tempForce * force, ForceMode2D.Impulse);
        transform.forward = tempForce;

    }
   
}
