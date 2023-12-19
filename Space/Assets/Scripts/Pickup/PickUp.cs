using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] protected float TimeLive = 5;
    private float timeCurrent;

    private void Update()
    {
        if (timeCurrent >= TimeLive)
        {
            timeCurrent = 0;
            gameObject.SetActive(false);
        }
        timeCurrent += Time.deltaTime;
    }
}
