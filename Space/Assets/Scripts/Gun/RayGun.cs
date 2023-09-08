using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RayGun : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] private GameObject laser;
    [SerializeField] private Transform positionRay;
    [SerializeField] private float rayCastDistance;
    [SerializeField] private Vector3 lastRayPosition;
    [SerializeField] private bool canDamage;
    [SerializeField] private float timeFire, curreTimeFire;


    private void Awake()
    {
        laser.transform.position= positionRay.transform.position;
        laser.transform.rotation = transform.rotation;
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
        else
        {
            curreTimeFire += Time.deltaTime;
        }

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
    /// Calcula donde va a caer el rayo 
    /// </summary>
    /// <returns></returns>
    private void EndLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(positionRay.position, Vector2.down, rayCastDistance);

        if (hit.collider != null)
        {
            Debug.DrawLine(positionRay.position, hit.point, Color.green);
            float PoinToRay = hit.collider.transform.position.y - positionRay.position.y;
            lastRayPosition = new Vector3(0, PoinToRay, 0);
            laser.GetComponent<LaserRay>().Damage(hit.collider.gameObject);
        }
        else
        {
            // Si el rayo no golpea nada, dibuja una línea roja en la dirección hacia adelante.
            Debug.DrawRay(positionRay.position, Vector2.down * rayCastDistance, Color.red);
            lastRayPosition = transform.position;
        }
    }

}
