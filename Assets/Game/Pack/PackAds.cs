using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackAds : Pack
{
    
    public override void BuyPack()
    {
        CtrlDataGame.Ins.ActiveRemoveAds();
    }
}
