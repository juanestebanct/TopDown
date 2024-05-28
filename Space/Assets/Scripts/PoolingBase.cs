using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingBase : MonoBehaviour
{
    [Header("pooling")]
    [SerializeField] private int MaxBullet;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform refPoint;
    private List<GameObject> bullets = new List<GameObject>();
    void Start()
    {
        InitPool();
    }
    private void InitPool()
    {
        for (int i = 0; i < MaxBullet; ++i)
        {
            GameObject bullet = Instantiate(projectile, refPoint.position, transform.rotation);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }

    private void Shootpool()
    {
        //Verifica si necesita esta activado  
        GameObject bullet = bullets.Find(b => !b.activeSelf);
        if (bullet == null)
        {
            bullet = Instantiate(projectile, refPoint.position, transform.rotation);
            bullets.Add(bullet);
        }
        //Toda la logica que necesita

        bullet.transform.rotation = transform.rotation;
    }
}
