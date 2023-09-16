using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region public 
    public static PlayerController instance;
    public InputManager inputManager;
    public Action Fire;
    public Action Reset;
    #endregion

    #region private

    private Rigidbody2D rb;
    private bool isFiring;

    #endregion

    [SerializeField] private float speed;

    private void Awake()
    {
        instance = this;
        inputManager = new InputManager();
        inputManager.Player.Enable();
        inputManager.Player.Fire.performed += StarFire;
        inputManager.Player.Fire.canceled += StopFire;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isFiring) Fire();
    }
    private void StarFire(InputAction.CallbackContext ctx) { isFiring = true; }
    private void StopFire(InputAction.CallbackContext ctx) { isFiring = false; }
    private void FixedUpdate()
    {
        Move(inputManager.Player.Move.ReadValue<Vector2>());

    }
    private void Move(Vector2 movement)
    {
        rb.velocity = movement * speed * 10 * Time.fixedDeltaTime;
    }
    private void OnDisable()
    {
        inputManager.Player.Disable();
    }
}
