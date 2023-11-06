using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] protected float TimeLive = 5;
    private float TimeCurrent;

    private void Update()
    {
        if (TimeCurrent >= TimeLive) Destroy(gameObject);

        TimeCurrent += Time.deltaTime;
    }
}
