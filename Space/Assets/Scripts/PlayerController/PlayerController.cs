using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static Updates;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerController : MonoBehaviour
{
    #region public 
    public static PlayerController instance;
    public FloatingJoystick floatingJoystick;
    public FloatingJoystick floatingJoystickShoot;
    public InputManager inputManager;

    public Action OnPause, OnReset, Fire, Reset;
    public Shield Shield;
    public RayGun RayGun;

    public ProyectileWeapon[] Weapons; 
    #endregion

    #region private
    private Rigidbody2D rb;
    private bool isFiring;
    private PlayerStats playerStats;

    #endregion
    [SerializeField] private Transform poinToShoot;
    [SerializeField] private float speed;
    [SerializeField] private float brakeSpeed;
    [SerializeField] private ProyectileWeapon weapon;


    private void Awake()
    {
        instance = this;
        EnableInputs();
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (isFiring) Fire();

    }
    private void StarFire(InputAction.CallbackContext ctx) { isFiring = true; }
    private void StopFire(InputAction.CallbackContext ctx) { isFiring = false; }
    private void TriggerPause(InputAction.CallbackContext ctx) { if (OnPause != null) OnPause(); }
    private void TriggerReset(InputAction.CallbackContext ctx) { if (OnReset != null) OnReset(); }
    private void FixedUpdate()
    {
        Move(inputManager.Player.Move.ReadValue<Vector2>());
        Move(new Vector2(floatingJoystick.Horizontal, floatingJoystick.Vertical));
        NewPosition(new Vector2(floatingJoystickShoot.Horizontal, floatingJoystickShoot.Vertical));
    }
    private void Move(Vector2 movement)
    {
        // Check if there is any forward movement input
        if (movement.y != 0 || movement.x != 0)
        {
            //Fire();
            // Calculate the target rotation based on the input
            float targetRotation = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;

            // Smoothly rotate towards the target rotation
            float rotationSpeed = 1000f;  // Adjust the rotation speed as needed
            float newRotation = Mathf.MoveTowardsAngle(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(newRotation);

            // Move forward in the direction of the current rotation
            rb.velocity = movement * speed * 10 * Time.fixedDeltaTime;
        }
        else
        {
            // If there is no forward movement input, gradually slow down the Rigidbody
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, brakeSpeed * Time.fixedDeltaTime);
        }
    }
    private void OnDisable()
    {
        inputManager.Player.Disable();
    }
    private void EnableInputs()
    {
        inputManager = new InputManager();
        inputManager.Player.Enable();
        inputManager.Player.Fire.performed += StarFire;
        inputManager.Player.Pause.performed += TriggerPause;
        inputManager.Player.Reset.performed += TriggerReset;
        inputManager.Player.Fire.canceled += StopFire;
    }
    private void NewPosition(Vector2 direction)
    {
        if (direction.y <= -0.2f || direction.y >= 0.2f || direction.x <= -0.2f || direction.x >= 0.2f)
        {
            poinToShoot.transform.forward = direction;
            Fire();
        }
    }
    private void AplicateSpeed(int moreSpeed)
    {
        speed += ((float)moreSpeed/100) * speed;
     
    }
    public void ActiveShield()
    {
        Shield.ActiveShield();
        playerStats.DesactiveDamage();
    }
    public void ActiveRay()
    {
        RayGun.gameObject.SetActive(true);
        RayGun.DesactivarObjeto();
    }
    public void ChangeWeapon(ProyectileWeapon NewWeapon)
    {
        weapon = NewWeapon;
    }
    public void AplicationUpdate(TypeUpdate typeUpdate,int Update)
    {
        StartCoroutine(playerStats.RelockTime());

        switch (typeUpdate)
        {
            case TypeUpdate.Live:
                playerStats.UpdateMoreLive(Update);
                break;
            case TypeUpdate.Speed:
                AplicateSpeed(Update);
                break;

            case TypeUpdate.Moreshoot:
                weapon.AddShoot(Update);
                break;
            case TypeUpdate.Drilling:
                weapon.AddDilling(Update);
                break;
            case TypeUpdate.Cooldown:
                break;


        }
    }
    
}
