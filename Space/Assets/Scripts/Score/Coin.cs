using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Score.Instance.GetPoins(coinValue);
            AudioManager.instance.PlayClip(AudioManager.instance.PickUp);
            gameObject.SetActive(false);
        }
    }
}
