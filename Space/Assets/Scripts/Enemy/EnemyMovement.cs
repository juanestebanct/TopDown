using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


public enum MoventPatron {Circule,ChaseToPlayer,Down,HorizonChange }

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private MoventPatron patron;

    [Header("Circle Movement")]
    [SerializeField] private float circleRadius;
    [SerializeField] private float rotatioSpeed;
    [SerializeField] private float downSpeed;

    private Vector3 startPosition;
    private float startTime;

    [Header("ChaseToPlayer")]
    [SerializeField] private float Speed;
    private bool triggerTargetPosition;
    private Vector3 targetPosition;

    [Header("MoveDown")]
    [SerializeField] private float SpeedDown;
    [SerializeField] private float timeToDown, currentDropTime;

    // Update is called once per frame
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        startTime = Time.time;
        triggerTargetPosition = true;
        currentDropTime = 0;
    }
    private void FixedUpdate()
    {
        switch (patron)
        {
            case MoventPatron.Circule:
                MovementCircule();
                break;

            case MoventPatron.ChaseToPlayer:
                ChaseToPlayer();
                break;
            case MoventPatron.Down:
                MovenDown();
                break;
            case MoventPatron.HorizonChange:
                ChaseHorizon();
                break;
            
        }
    }

    private void MovementCircule()
    {
        float elapsedTime = Time.time - startTime;
        float angle = elapsedTime * rotatioSpeed;
        float x = Mathf.Cos(angle) * circleRadius;
        float y = Mathf.Sin(angle) * circleRadius;

        Vector2 circlePosition = startPosition + new Vector3(x, y);
        rb.MovePosition(circlePosition + Vector2.down * elapsedTime * downSpeed);
    }
    private void ChaseToPlayer()
    {
        if (triggerTargetPosition)
        {
            targetPosition = PlayerController.instance.transform.position;
            triggerTargetPosition = false;
        }

        Vector2 direction = (targetPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotacion 
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle + 90.0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200 * Time.deltaTime);

        Vector2 movement = direction * Speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);
        if (Vector3.Distance(transform.position, targetPosition) <=1f) triggerTargetPosition = true;
    }
    private void ChaseHorizon()
    {
        targetPosition = PlayerController.instance.transform.position;
        Vector2 direction = new Vector2((targetPosition.x - transform.position.x),0).normalized;

        float distanciaEnEjeX = Mathf.Abs(transform.position.x - targetPosition.x);
        if (distanciaEnEjeX >= 1f)
        {
            Vector2 movement = direction * Speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
        if (currentDropTime >= timeToDown)
        {
            currentDropTime = 0;
            patron = MoventPatron.Down;
        }
        currentDropTime += Time.deltaTime;

    }
    //lo baja verticalmente hasta un punto
    private void MovenDown()
    {
        Vector2 movement = Vector2.down * SpeedDown * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        if (currentDropTime >= timeToDown)
        {
            currentDropTime = 0;
            patron = MoventPatron.HorizonChange;
        }
        currentDropTime += Time.deltaTime;
    }
    private void ChangePatron()
    {
        switch (patron)
        {
            case MoventPatron.Circule:
                MovementCircule();
                break;
            case MoventPatron.ChaseToPlayer:
                ChaseToPlayer();
                break;
            case MoventPatron.HorizonChange:
                ChaseHorizon();
                break;
        }
    }
    public void ResetValues(Vector3 newPosition, MoventPatron pattern)
    {
        startPosition = newPosition;
        startTime = Time.time;
        triggerTargetPosition = true;
        patron = pattern;
        transform.rotation= Quaternion.Euler(0, 0, 0);
        currentDropTime = 0;
        ChangePatron();
    }
}
