using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected int Live;
    [SerializeField] protected int Damage;
    [SerializeField] protected GameObject DeadVfx;
 
}
