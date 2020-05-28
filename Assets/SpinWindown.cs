using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWindown : Screen
{
    public override void EventOpen()
    {
        GameMananger.Ins.TrasUIGenrate.gameObject.SetActive(true);
        GameMananger.Ins.TransSetting.gameObject.SetActive(false);
    }
}
