using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canyon : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectirePoint;
    [SerializeField] private float TimeToFire = 2f;
    private bool canFire = true;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController= GetComponent<PlayerController>();
    }
    private void Shoot()
    {
        GameObject Power = Instantiate(projectile, projectirePoint.position, transform.rotation);
        Power.GetComponent<Rigidbody2D>().velocity += new Vector2(0,rb.velocity.y);
        StartCoroutine(Delay());
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(TimeToFire);
        canFire = true;
    }
}
