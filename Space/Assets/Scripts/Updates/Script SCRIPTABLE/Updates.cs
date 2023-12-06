using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewUpdate", menuName = "update")]
public class Updates : ScriptableObject
{
    public enum TypeUpdate
    {
        Live,
        Weapon,
        Speed,

    }
    public Image ImageUpdate;

    public TypeUpdate Type;

    public virtual void ActivateImprovement()
    {

    }
}
