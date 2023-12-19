using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpdate", menuName = "UpdateSpeed")]
public class UpdateSpeed : Updates
{
    public int SpeedPorcer;

    public override void ActivateImprovement()
    {
        PlayerController.instance.AplicationUpdate(Type, SpeedPorcer);
    }
}
