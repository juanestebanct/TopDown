using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


public enum MoventPatron {Circule,ChaseToPlayer,Down,HorizonChange,LookPlayer,GoBack }

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private MoventPatron patron;
    [SerializeField] private Enemy enemy;

    [Header("Circle Movement")]
    [SerializeField] private float circleRadius;
    [SerializeField] private float rotatioSpeed;
    [SerializeField] private float downSpeed;

    private Vector3 startPosition;
    private float startTime;

    [Header("ChaseToPlayer")]
    [SerializeField] private float speedChase;
    [SerializeField] private float rotationLook;

    private bool triggerTargetPosition;
    private Vector3 targetPosition;
    private PlayerController reference;

    [Header("MoveDown")]
    [SerializeField] private float SpeedDown;
    [SerializeField] private float timeToDown, currentDropTime;

    [Header("MoveBack")]
    [SerializeField] private float speedBack;

    // Update is called once per frame
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        startTime = Time.time;
        triggerTargetPosition = true;
        currentDropTime = 0;
    }
    private void FixedUpdate()
    {
        ChangePatron();
    }
    private void Start()
    {
       
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
            targetPosition = reference.transform.position;
            triggerTargetPosition = false;
        }

        Vector2 direction = (targetPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotacion 
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle + 90.0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationLook * Time.deltaTime);

        Vector2 movement = direction * speedChase * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);
        if (Vector3.Distance(transform.position, targetPosition) <= 1f) triggerTargetPosition = true;
    }

    private void ChaseHorizon()
    {
        targetPosition = PlayerController.instance.transform.position;
        Vector2 direction = new Vector2((targetPosition.x - transform.position.x), 0).normalized;

        float distanciaEnEjeX = Mathf.Abs(transform.position.x - targetPosition.x);
        if (distanciaEnEjeX >= 1f)
        {
            Vector2 movement = direction * speedChase * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
        if (currentDropTime >= timeToDown)
        {
            currentDropTime = 0;
            patron = MoventPatron.Down;
            ChangePatron();
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
            ChangePatron();
        }
        currentDropTime += Time.deltaTime;
    }

    private void LookAtThePlayer()
    {
        targetPosition = PlayerController.instance.transform.position;

        Vector2 direction = (targetPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotacion 
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle + 90.0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationLook * Time.deltaTime);
        ChangePatron();
    }

    private void ChangePatron()
    {
        switch (patron)
        {
            case MoventPatron.Circule:
                enemy.Move = MovementCircule;
                break;
            case MoventPatron.ChaseToPlayer:
                enemy.Move = ChaseToPlayer;
                break;
            case MoventPatron.Down:
                enemy.Move = MovenDown;
                break;
            case MoventPatron.HorizonChange:
                enemy.Move = ChaseHorizon;
                break;
            case MoventPatron.LookPlayer:
                enemy.Move = LookAtThePlayer;
                break;
            case MoventPatron.GoBack:
                enemy.Move = GoBack;
                break;

        }
    }
    public void ResetValues(Vector3 newPosition, MoventPatron pattern)
    {
        startPosition = newPosition;
        startTime = Time.time;
        triggerTargetPosition = true;
        patron = pattern;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        currentDropTime = 0;
        ChangePatron();
    }

    public void ChangeRute(MoventPatron pattern)
    {
        patron = pattern;
        ChangePatron();
    }
    public void GoBack()
    {
        Vector2 movement = transform.up * speedBack * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
    public void GetReference(PlayerController tempReference)
    {
        reference = tempReference;
        if (reference == null) { reference = PlayerController.instance; }
        
    }

}
