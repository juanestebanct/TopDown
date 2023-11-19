using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerController : MonoBehaviour
{
    #region public 
    public static PlayerController instance;
    public FloatingJoystick floatingJoystick;
    public InputManager inputManager;
    public Action OnPause,OnReset,Fire,Reset;
    public Shield Shield;
    public RayGun RayGun;
    #endregion

    #region private

    private Rigidbody2D rb;
    private bool isFiring;
    private PlayerStats playerStats;

    #endregion

    [SerializeField] private float speed;
    [SerializeField] private float brakeSpeed;

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
    }
    private void Move(Vector2 movement)
    {
       
        if (movement.magnitude>0)
        {
            rb.velocity = movement * speed * 10 * Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, brakeSpeed * Time.fixedDeltaTime);
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
}
