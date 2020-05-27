using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourmentWindown : Screen
{
    public override void EventOpen()
    {
        var a = TourmentCtrl.Ins.GetTourmnet("V_1");
        a.Skin[0]  = CtrlDataGame.Ins.GetIdHead();
        a.Skin[1] = CtrlDataGame.Ins.GetIdHand();
        a.Skin[2] = CtrlDataGame.Ins.GetIdItemHand();
        a.Skin[3] = CtrlDataGame.Ins.GetIdLeg();
        a.Skin[4] = CtrlDataGame.Ins.GetIdItemLeg();

        a.ApplyPlayer();
        if (a.isNext)
        {
            var b = TourmentCtrl.Ins.GetTourmnet("V_2_0");
            b.Skin = a.Skin;
            b.ApplyPlayer();

            if (b.isNext)
            {
                var c = TourmentCtrl.Ins.GetTourmnet("V_3");
                c.Skin = b.Skin;
                c.ApplyPlayer();

            }
        }

        

        GameMananger.Ins.TransSetting.gameObject.SetActive(false);
    }
}
