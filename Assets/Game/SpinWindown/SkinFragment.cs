using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinFragment : Fragment
{
    public override void ApplyReward()
    {
        var skin = SpinCtrl.Ins.GetSkinSave();
        switch (skin.typeSkin)
        {

            case TypeShop.Shop_Hand:

                ShopCtrl.Ins.Item_Hands[skin.rSkin].isBuy = false;
                ShopCtrl.Ins.Item_Hands[skin.rSkin].LoadStatusItem();
                ShopCtrl.Ins.SaveShopHand();

                break;
            case TypeShop.Shop_Head:

                ShopCtrl.Ins.Item_Heads[skin.rSkin].isBuy = false;
                ShopCtrl.Ins.Item_Heads[skin.rSkin].LoadStatusItem();
                ShopCtrl.Ins.SaveShopHead();
                break;
            case TypeShop.Shop_Leg:

                ShopCtrl.Ins.Item_Legs[skin.rSkin].isBuy = false;
                ShopCtrl.Ins.Item_Legs[skin.rSkin].LoadStatusItem();
                ShopCtrl.Ins.SaveShopLeg();
                break;
        }
    }
}
