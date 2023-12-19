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
    [SerializeField] private GameObject trail;

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
    public void AddForce(Vector2 tempMeteorite, Quaternion rotation)
    {
        tempMeteorite.Normalize();
        rb.AddForce(tempMeteorite * force, ForceMode2D.Impulse);
        transform.rotation = rotation;      
        Invoke("Desactive",DesactivateTime);
       
    }
    public void MoreSpeed(float velocityPlayer)
    {
        velocity += velocityPlayer;
    }
}
