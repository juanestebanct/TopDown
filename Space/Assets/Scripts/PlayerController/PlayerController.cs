using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region public 
    public static PlayerController instance;
    #endregion

    #region private

    private Rigidbody2D rb;
    private InputManager inputManager;

    #endregion

    [SerializeField] private float speed;

    private void Awake()
    {
        instance = this;
        inputManager = new InputManager();
        inputManager.Player.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
        Move(inputManager.Player.Move.ReadValue<Vector2>());
    }
    private void Move(Vector2 movement)
    {
        rb.velocity = movement * speed * 10 * Time.fixedDeltaTime;
    }
}
