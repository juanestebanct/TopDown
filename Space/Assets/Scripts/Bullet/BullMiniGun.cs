using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class BullMiniGun : Projectile
{
    [Header("Starts BullMiniGun")]
    [SerializeField] private float startVelocity;
    [SerializeField] private GameObject trail;
    [SerializeField] private int minDamage;
    [SerializeField] private float force;

    private Vector3 direction;
    private float velocity;
    private Rigidbody2D rb;
    public void Awake()
    {
        velocity = startVelocity;
        direction = Vector3.forward;
        rb = GetComponent<Rigidbody2D>();
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
    private int RangeDamage()
    {
        int tempDamage = (int)Random.Range(minDamage, damage);
        return tempDamage;
    }
    private void OnEnable()
    {
        velocity = startVelocity;
        rb.velocity = new Vector2(0, 0);
        StartCoroutine(Active());
    }
    private void OnDisable()
    {
        trail.gameObject.SetActive(false);
    }
    private IEnumerator Active()
    {
        trail.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.08f);
        trail.gameObject.SetActive(true);
    }
    public void MoreSpeed(float velocityPlayer)
    {
        velocity += velocityPlayer;
        
    }
    public void newDireccion(Vector3 vector3)
    {
        direction = vector3;
    }
    public void AddForce(Vector2 tempDireccion)
    {
        tempDireccion.Normalize();
        rb.AddForce(tempDireccion * force, ForceMode2D.Impulse);
        transform.forward = tempDireccion;

    }
}
