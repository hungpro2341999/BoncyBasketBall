﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionScreen : Screens
{
    public override void EventOpen()
    {
        GameMananger.Ins.TransSetting.gameObject.SetActive(false);
        GameMananger.Ins.TrasUIGenrate.gameObject.SetActive(true);
    }


}
