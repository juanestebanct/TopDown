using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RayGun : MonoBehaviour
{
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
    private void DesactiveRay()
    {
        canDamage = false;
    }

    /// <summary>
    /// activa el rayo
    /// </summary>
    private void ActiveRay()
    {
        laser.SetActive(true);
        canDamage = true;
    }
   
    /// <summary>
    /// La
    /// </summary>
    /// <returns></returns>
    private void EndLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(positionRay.position, Vector2.down, rayCastDistance);

        if (hit.collider != null)
        {
            Debug.Log("Objeto golpeado: " + hit.collider.gameObject.name);
            Debug.DrawLine(positionRay.position, hit.point, Color.green);
            lastRayPosition = new Vector3(0, hit.collider.transform.position.y,0);
            if (canDamage) laser.GetComponent<LaserRay>().Damage(hit.collider.gameObject);
        }
        else
        {
            // Si el rayo no golpea nada, dibuja una línea roja en la dirección hacia adelante.
            Debug.DrawRay(positionRay.position, Vector2.down * rayCastDistance, Color.red);
            lastRayPosition = transform.position;
        }
    }
}
