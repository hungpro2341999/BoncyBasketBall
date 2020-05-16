using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLeg : ItemHand
{
    public override void Click()
    {
        if (type == TypeItem.Default)
        {
            ShopCtrl.Ins.TargetGraphic.Attachment_Leg(Img.sprite);
            ShopCtrl.Ins.CurrSelectLeg = this;
        }
        else if (type == TypeItem.FullItem)
        {
            ShopCtrl.Ins.TargetGraphic.Attachment_Leg(Img.sprite);
            ShopCtrl.Ins.TargetGraphic.Attachment_Item_Leg(CtrlDataGame.Ins.Resource.Sprite_Null);
            ShopCtrl.Ins.CurrSelectLeg = this;
        }
        else
        {
            if (ShopCtrl.Ins.GetTypeLegByID(ShopCtrl.Ins.CurrSelectLeg.idItem) != TypeItem.FullItem)
            {
                ShopCtrl.Ins.TargetGraphic.Attachment_Item_Leg(Img.sprite);
            }
            else
            {
                ShopCtrl.Ins.TargetGraphic.Attachment_Item_Leg(CtrlDataGame.Ins.Resource.Sprite_Null);
            }
        }

        ShopCtrl.Ins.TargetGraphic.Apply();

    }

}
