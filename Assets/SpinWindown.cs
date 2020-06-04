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
            Debug.Log("GetSkin 1 : "+ skinSave.rSkin +"  "+ skinSave.typeSkin);
            if (!SpinCtrl.Ins.isBuy(skinSave.rSkin, skinSave.typeSkin))
            {

                Debug.Log("GetSkin 2");
                SpinCtrl.Ins.RandomSkin();
            }
          
        }

      
        SpinCtrl.Ins.ApplyChageFragmentSkin();
        
    }
}
