using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWindown : Screen
{
    // Start is called before the first frame update

    public override void EventOpen()
    {
        GameMananger.Ins.TransSetting.gameObject.SetActive(false);
    }
}
