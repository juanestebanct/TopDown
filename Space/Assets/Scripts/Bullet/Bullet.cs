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

    public void Awake()
    {
        Invoke("Desactive", desactivateTime);
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * velocity * Time.fixedDeltaTime);
    }
    private void Desactive()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (type)
        {
            case BulletType.Enemy:
                
                break;

            case BulletType.Player:
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    collision.gameObject.GetComponent<IDamage>().ResiveDamage(damage);
                }
                gameObject.SetActive(false);
                break;
        }
    }
}
