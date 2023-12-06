using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpdate", menuName = "updateLive")]
public class UpdateLive : Updates
{
    public int MoreLive;
    public override void ActivateImprovement()
    {
        PlayerController.instance.AplicationUpdate(Type, MoreLive);
    }
}
