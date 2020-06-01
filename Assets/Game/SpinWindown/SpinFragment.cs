using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpinFragment :Fragment
{

    public override void ApplyReward()
    {
        SpinCtrl.Ins.AddFreeRoll();
    }
}
