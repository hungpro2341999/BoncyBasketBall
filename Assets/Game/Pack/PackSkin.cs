using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackSkin : Pack 
{ 
    [SerializeField]
    public SkinPack[] PacksSkin;
    public int RewardCoin = 0;
    
    // Start is called before the first frame update
    public void UnclockSkin()
    {
        for(int i = 0; i < PacksSkin.Length; i++)
        {
            UnclockBuy(PacksSkin[i].id, PacksSkin[i].typeShop);
        }

        ShopCtrl.Ins.SaveShopHand();
        ShopCtrl.Ins.SaveShopHead();
        ShopCtrl.Ins.SaveShopLeg();

        CtrlDataGame.Ins.AddCoin(RewardCoin);
    }

    public void UnclockBuy(int i, TypeShop type)
    {
        switch (type)
        {
            case TypeShop.Shop_Hand:



                ShopCtrl.Ins.Item_Hands[i].isBuy = false;
                ShopCtrl.Ins.Item_Hands[i].LoadStatusItem();

                break;
            case TypeShop.Shop_Leg:


                ShopCtrl.Ins.Item_Legs[i].isBuy = false;
                ShopCtrl.Ins.Item_Legs[i].LoadStatusItem();

                break;
            case TypeShop.Shop_Head:

                ShopCtrl.Ins.Item_Heads[i].isBuy = false;
                ShopCtrl.Ins.Item_Heads[i].LoadStatusItem();
                break;
        }
        
    }
}

[System.Serializable]

public class SkinPack
{
    public int id;
    public TypeShop typeShop;
}
