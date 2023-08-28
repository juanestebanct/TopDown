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
        Destroy(gameObject);
    }
}
