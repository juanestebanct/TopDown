using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Updates : ScriptableObject
{
    public enum TypeUpdate
    {
        Live,
        Weapon,
        Speed,
        Moreshoot,
        Drilling,
        Cooldown
    }
    public Sprite ImageUpdate;

    public TypeUpdate Type;

    public string Title;
    public string Description;
    /// <summary>
    /// aqui se va a actualizar la imagen 
    /// </summary>
    /// <param name="Card"></param>
    public virtual void ActivateImprovement()
    {

    }
    public virtual void ActivateImprovement(ProyectileWeapon weapon)
    {

    }
}
