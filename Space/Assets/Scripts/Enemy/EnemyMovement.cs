using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum MoventPatron {Circule,ChaseToPlayer }

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

    // Update is called once per frame
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        startTime = Time.time;
        triggerTargetPosition = true;
    }
    private void Update()
    {
        switch (patron)
        {
            case MoventPatron.Circule:
                MovementCircule();
                break;

            case MoventPatron.ChaseToPlayer:
                ChaseToPlayer();
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

        Vector2 movement = direction * Speed * Time.deltaTime;

        rb.MovePosition(rb.position + movement);
        if (Vector3.Distance(transform.position, targetPosition) <= 0.2f) triggerTargetPosition = true;
    }
    public void ResetValues(Vector3 newPosition, MoventPatron pattern)
    {
        startPosition = newPosition;
        transform.position = newPosition;
        startTime = Time.time;
        triggerTargetPosition = true;
        patron = pattern;
    }
}
