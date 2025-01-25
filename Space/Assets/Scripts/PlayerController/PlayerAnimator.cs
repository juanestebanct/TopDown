using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private GameObject _mainTorrent;
    private Animator animator;
    private int stateShip;
    private bool newUpdate;
    
    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("No Animator component found on this GameObject.");
        }
    }

    public void ViewForward(Vector2 direction)
    {
        if(newUpdate) _mainTorrent.transform.up =direction ;  
    } 
    public void ChangeStateWeapon()
    {
        newUpdate = true;
        stateShip++;
        if (stateShip == 1)
        {
            animator.SetBool("NewUpdate", newUpdate);
            animator.SetFloat("UpdateWeapon", stateShip);   
        }
    }
}
