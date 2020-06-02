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
        ShopCtrl.Ins.SelectItemLeg(idItem);
        ShopCtrl.Ins.TargetGraphic.Apply();
        AudioCtrl.Ins.Play("EquipItem");
    }
    public override void Buy()
    {
        if (isBuy)
        {
            if (CtrlDataGame.Ins.GetCoin() >= cost)
            {
                AudioCtrl.Ins.Play("LockBuyItem");
                CtrlDataGame.Ins.EarnCoin(cost);
                isBuy = false;
                isUsing = true;
                LoadStatusItem();
                ShopCtrl.Ins.SaveShopLeg();
                ShopCtrl.Ins.SelectItemLeg(idItem);
                if(type == TypeItem.FullItem || type == TypeItem.Default)
                {
                    CtrlDataGame.Ins.SetLeg(this.idItem);
                }
                else
                {
                    CtrlDataGame.Ins.SetItemLeg(this.idItem);
                }
                MissonCtrl.Ins.UpdateMission(5);
            }
        }

    }

}
