using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRay : PickUp
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) collision.gameObject.GetComponent<PlayerController>().ActiveRay();
        gameObject.SetActive(false);
    }
}
