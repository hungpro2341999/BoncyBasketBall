using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWindown : Screen
{
    public override void EventOpen()
    {
        


        GameMananger.Ins.TrasUIGenrate.gameObject.SetActive(true);
        GameMananger.Ins.TransSetting.gameObject.SetActive(false);
        var skinSave = SpinCtrl.Ins.GetSkinSave();

        if (!SpinCtrl.Ins.HasRandomSkin())
        {
            SpinCtrl.Ins.RandomSkin();
            Debug.Log("1");
        }
        else
        {
            if (!SpinCtrl.Ins.isBuy(skinSave.rSkin, skinSave.typeSkin))
            {
                SpinCtrl.Ins.RandomSkin();
            }
            Debug.Log("2");
        }

      
        SpinCtrl.Ins.ApplyChageFragmentSkin();
        
    }
}
