using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class LaserRay : Projectile
{
    [Header("Raygun ")]
    public Coroutine lineDraw;

    [SerializeField] private RayGun Raygun;
    [SerializeField] private ParticleSystem LineParticule;
    [SerializeField] private LineRenderer lineRenderer, lineAnticipation;
    [SerializeField] private float waitToStar;

    private bool canDamage;

    [Header("Source")]
    [SerializeField] private AudioSource rayGunSource;
    [SerializeField] private AudioClip charging,inprogress,fire;

    [Header("Raygun Change")]
    public AnimationCurve blendedCurve;



    private float initialWidth;
    private float targetWidth;
    private float elapsedTime = 0f;

    private void Awake()
    {
        initialWidth = lineRenderer.startWidth;
    }
    public void RayPosition(Vector3 position)
    {
        if (!canDamage) return;
        lineRenderer.SetPosition(1, position);
    }
    public void active()
    {
        elapsedTime = 0;
        rayGunSource.PlayOneShot(charging);
        lineDraw = StartCoroutine(LineDraw());

        lineRenderer.startWidth = 5;
        lineRenderer.endWidth = 5;
        targetWidth = blendedCurve.Evaluate(1f);

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
                if (Objetive.CompareTag("Enemy") || Objetive.gameObject.CompareTag("Meteorite"))
                {
                    Objetive.GetComponent<IDamage>().ResiveDamage(damage);
                }
                break;
                
        }
    }

    private void DesactiveLaser()
    {
        canDamage = false;
        lineRenderer.SetPosition(1, Vector3.zero);
        Raygun.DesactiveRay();
        LineParticule.Stop();
        LineParticule.Clear();

    }
    private IEnumerator LineDraw()
    {
        LineParticule.Play();
        if (lineAnticipation) StartCoroutine(ChangeWidthGraduallyEffect());
        yield return new WaitForSeconds(waitToStar);

        rayGunSource.PlayOneShot(fire);
        canDamage = true;
        StartCoroutine(ChangeWidthGradually());
        yield return new WaitForSeconds(DesactivateTime);

        DesactiveLaser();
    }

    private IEnumerator ChangeWidthGradually()
    {
        if (inprogress!= null) rayGunSource.PlayOneShot(inprogress);
        elapsedTime = 0;

        while (elapsedTime < DesactivateTime)
        {
            // Calcula el valor de interpolación entre 0 y 1
            float t = elapsedTime / DesactivateTime;

            // Evalúa la curva de animación para obtener el ancho deseado en este punto
            float lerpedWidth = Mathf.Lerp(targetWidth,initialWidth, blendedCurve.Evaluate(t));

            // Actualiza los anchos del LineRenderer
            lineRenderer.startWidth = lerpedWidth;
            lineRenderer.endWidth = lerpedWidth;

            // Incrementa el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            yield return null; // Espera un frame antes de la próxima iteración
        }

        // Asegúrate de que el ancho final sea exacto
        lineRenderer.startWidth = targetWidth;
        lineRenderer.endWidth = targetWidth;
    }

    private IEnumerator ChangeWidthGraduallyEffect()
    {
        lineAnticipation.startWidth = 12;
        lineAnticipation.endWidth = 12;
        lineAnticipation.gameObject.SetActive(true);

        while (elapsedTime < 0.5f)
        {
            // Calcula el valor de interpolación entre 0 y 1
            float t = elapsedTime / 0.5f;

            // Evalúa la curva de animación para obtener el ancho deseado en este punto
            float lerpedWidth = Mathf.Lerp(targetWidth, initialWidth, blendedCurve.Evaluate(t));

            // Actualiza los anchos del LineRenderer
            lineAnticipation.startWidth = lerpedWidth;
            lineAnticipation.endWidth = lerpedWidth;

            // Incrementa el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            yield return null; // Espera un frame antes de la próxima iteración
        }
        lineAnticipation.gameObject.SetActive(false);
        // Asegúrate de que el ancho final sea exacto

    }
    private void OnDisable()
    {
        if (lineDraw != null) StopCoroutine(lineDraw);
        DesactiveLaser();
    }

}
