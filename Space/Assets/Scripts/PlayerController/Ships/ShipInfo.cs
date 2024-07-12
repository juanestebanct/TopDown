using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipTemplate", menuName = "Ships")]
public class ShipInfo : ScriptableObject
{
    public Sprite ImageRefence;
    public string Name;
    public string Weapon;
    public int Speed;
    public int Live;
}
