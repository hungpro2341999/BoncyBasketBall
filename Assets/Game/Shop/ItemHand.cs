using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHand : Item
{
  
    public override void Click()
    {
      if(type  == TypeItem.Default)
        {
            ShopCtrl.Ins.TargetGraphic.Attachment_Hand(Img.sprite);
        }
        else if(type == TypeItem.FullItem)
        {
            ShopCtrl.Ins.TargetGraphic.Attachment_Hand(Img.sprite);
        }
        else
        {
            ShopCtrl.Ins.TargetGraphic.Attachment_Item_Hand(Img.sprite);
        }

        ShopCtrl.Ins.TargetGraphic.Apply();

    }
}
