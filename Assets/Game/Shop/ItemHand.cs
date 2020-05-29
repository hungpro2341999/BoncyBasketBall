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
            ShopCtrl.Ins.CurrSelectHand = this; 
        }
        else if(type == TypeItem.FullItem)
        {
            ShopCtrl.Ins.TargetGraphic.Attachment_Hand(Img.sprite);
            ShopCtrl.Ins.TargetGraphic.Attachment_Item_Hand(CtrlDataGame.Ins.Resource.Sprite_Null);
            ShopCtrl.Ins.CurrSelectHand = this;
        }
        else
        {
            if (ShopCtrl.Ins.GetTypeHandByID(ShopCtrl.Ins.CurrSelectHand.idItem) != TypeItem.FullItem)
            {
                ShopCtrl.Ins.TargetGraphic.Attachment_Item_Hand(Img.sprite);
            }
            else
            {
                ShopCtrl.Ins.TargetGraphic.Attachment_Item_Hand(CtrlDataGame.Ins.Resource.Sprite_Null);
            }
            
        }
        ShopCtrl.Ins.SelectItemHand(idItem);

        ShopCtrl.Ins.TargetGraphic.Apply();
        AudioCtrl.Ins.Play("EquipItem");
    }
    public override void Buy()
    {

        Debug.Log("Buy");
        if (isBuy)
        {
         
            if (CtrlDataGame.Ins.GetCoin() >= cost)
            {
                AudioCtrl.Ins.Play("LockBuyItem");
                int coin = CtrlDataGame.Ins.GetCoin() - cost;
                CtrlDataGame.Ins.SaveCoin(coin);
                isBuy = false;
                isUsing = true;
                LoadStatusItem();
                ShopCtrl.Ins.SaveShopHand();
                ShopCtrl.Ins.SelectItemHand(idItem);
                if (type == TypeItem.FullItem || type == TypeItem.Default)
                {
                    CtrlDataGame.Ins.SetHand(this.idItem);
                }
                else
                {
                    CtrlDataGame.Ins.SetItemHand(this.idItem);
                }

                MissonCtrl.Ins.UpdateMission(6);
            }
        }

    }
}
