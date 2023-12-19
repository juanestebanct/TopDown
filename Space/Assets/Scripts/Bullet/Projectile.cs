using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum BulletType { Enemy,Player }
    [SerializeField] protected int damage;
    [SerializeField] protected float DesactivateTime,curreDesactiveTime;
    [SerializeField] protected BulletType type;
    private int drilling;
    public virtual void DrillingWeapon(int newDrilling)
    {
        drilling = newDrilling;
    }
    public virtual void LostDrilling()
    {
        drilling--;
        if (drilling <= 0)
            gameObject.SetActive(false);
    }
}
