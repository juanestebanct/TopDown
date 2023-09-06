using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class LaserRay : Projectile
{
    [SerializeField] private RayGun Raygun;
    [SerializeField] private ParticleSystem LineParticule;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float waitToStar,EndTime;
    private bool canDamage;
    public Coroutine lineDraw;

    public void RayPosition(Vector3 position)
    {
        if (!canDamage) return;
        lineRenderer.SetPosition(1, position);
    }
    public void active()
    {
        LineParticule.Play();
        lineDraw = StartCoroutine(LineDraw());
    }

    public void Damage(GameObject Objetive)
    {
        if (!canDamage) return; 
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
    private IEnumerator LineDraw()
    {
        yield return new WaitForSeconds(waitToStar);
        canDamage = true;

        yield return new WaitForSeconds(EndTime);
        canDamage = false;
        lineRenderer.SetPosition(1,Vector3.zero);
        Raygun.DesactiveRay();
        LineParticule.Stop();
        LineParticule.Clear();
    }
}
