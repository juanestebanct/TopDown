using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RayGun : MonoBehaviour
{
    [Header("Laser")]
    public float timeFire;
    [SerializeField] private GameObject laser;
    [SerializeField] private Transform positionRay;
    [SerializeField] private float rayCastDistance;
    [SerializeField] private Vector3 lastRayPosition;
    [SerializeField] private bool canDamage, disableforcedlacer;
    [SerializeField] private LayerMask Mask;
    [SerializeField] private float timeRay;

    private float curreTimeFire;
    private void Awake()
    {
        laser.transform.position = positionRay.transform.position;
        
        laser.SetActive(false);
    }
    private void Update()
    {
        //activa el rayo de energia 
        if (curreTimeFire >= timeFire && !canDamage)
        {
            ActiveRay();
            curreTimeFire = 0;
        }
        if (canDamage)
        {
            EndLaser();
            laser.GetComponent<LaserRay>().RayPosition(lastRayPosition);
        }
        else curreTimeFire += Time.deltaTime;
    }
    /// <summary>
    /// Desactiva el el rayo
    /// </summary>
    public void DesactiveRay()
    {
        canDamage = false;
    }

    /// <summary>
    /// activa el rayo y llama la funcion a laser ray
    /// </summary>
    private void ActiveRay()
    {
        laser.SetActive(true);
        canDamage = true;
        laser.GetComponent<LaserRay>().active();
    }
   
    /// <summary>
    /// Calcula donde va a caer el rayo y llama el daño
    /// </summary>
    /// <returns></returns>
    private void EndLaser()
    {

        RaycastHit2D hit = Physics2D.Raycast(positionRay.position, positionRay.forward, rayCastDistance, Mask);

        if (hit.collider != null)
        {
            Debug.DrawLine(positionRay.position, hit.point, Color.green);
           
            float magnitudeBetweenVectors = Vector3.Distance(hit.collider.transform.position, positionRay.position);

            lastRayPosition = new Vector3(0,-magnitudeBetweenVectors, 0);
            laser.transform.forward = positionRay.forward;

            if (disableforcedlacer)
            {
                laser.GetComponent<LaserRay>().RayPosition(lastRayPosition);
                return;
            }

            laser.GetComponent<LaserRay>().Damage(hit.collider.gameObject);
        }
        else
        {
            // Si el rayo no golpea nada, dibuja una línea roja en la dirección hacia adelante.
            Debug.DrawRay(positionRay.position, transform.forward * rayCastDistance, Color.red);
            
        }
    }
    public void DisableForcedLacer(bool Damage)
    {
        disableforcedlacer = Damage;
    }
    private void OnDisable()
    {
        DesactiveRay();
        curreTimeFire = 0;
    }
    public void DesactivarObjeto()
    {
        // Desactiva el objeto actual.
        Invoke("ActiveRaymanual", timeRay);
    }
    public void ActiveRaymanual()
    {
        gameObject.SetActive(false);
    }
}
