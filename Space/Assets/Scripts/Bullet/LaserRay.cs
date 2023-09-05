using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class LaserRay : Projectile
{
    [SerializeField] private ParticleSystem LineParticule;
    [SerializeField] private LineRenderer lineRenderer;

    public void RayPosition(Vector3 position)
    {
        lineRenderer.SetPosition(1, position);
    }
    public void active()
    {
        LineParticule.Play();
    }

    public void Damage(GameObject Objetive)
    {
        switch (type)
        {
            case BulletType.Enemy:

                if (Objetive.CompareTag("Player"))
                {
                    Objetive.GetComponent<IDamage>().ResiveDamage(damage);
                }

                break;
            case BulletType.Player:
                break;
        }
    }
    
    private void Update()
    {
        if(curreDesactiveTime >= desactivateTime)
        {
            curreDesactiveTime = 0;
        }
        curreDesactiveTime += Time.deltaTime;
    }
}
