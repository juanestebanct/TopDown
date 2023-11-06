using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLive : PickUp
{
    [SerializeField] private int heal = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) collision.gameObject.GetComponent<PlayerStats>().MoreLive(heal);
        gameObject.SetActive(false);
    }
}
