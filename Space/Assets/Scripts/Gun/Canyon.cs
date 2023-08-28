using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canyon : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectirePoint;
    [SerializeField] private float TimeToFire = 2f;
    private float NowVelocity; 
    private bool canFire = true;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController= GetComponent<PlayerController>();
        playerController.Fire+=Shoot;
    }
    private void Shoot()
    {
        GameObject Power = Instantiate(projectile, projectirePoint.position, transform.rotation);
        if (rb.velocity.y >= 0) NowVelocity = rb.velocity.y;
        Power.GetComponent<Rigidbody2D>().velocity += new Vector2(0, NowVelocity);

        StartCoroutine(Delay());
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(TimeToFire);
        canFire = true;
    }
}
