using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpShield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) collision.gameObject.GetComponent<PlayerController>().activeShield();
        gameObject.SetActive(false);
    }
}
