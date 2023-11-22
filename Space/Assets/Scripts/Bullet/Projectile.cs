using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum BulletType { Enemy,Player }
    [SerializeField] protected int damage;
    [SerializeField] protected float DesactivateTime,curreDesactiveTime;
    [SerializeField] protected BulletType type;
 }
