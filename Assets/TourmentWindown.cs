using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourmentWindown : Screen
{
    public override void EventOpen()
    {
        GameMananger.Ins.TransSetting.gameObject.SetActive(false);
    }
}
