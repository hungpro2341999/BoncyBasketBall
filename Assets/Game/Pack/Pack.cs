using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Pack : MonoBehaviour
{
    public int id;
    public int Value;
    public float Cost;
    public int Reward;
    public Image FirstBuy;
    public bool isFirstBuy;
    public bool Canbuy;

    public bool isBuy;
   

    public virtual void LoadPack(bool IsFirstBuy,bool Canbuy)
    {
        if (IsFirstBuy)
        {
            FirstBuy.gameObject.SetActive(true);
        }
        else
        {
            FirstBuy.gameObject.SetActive(false);
        }
  
        this.isFirstBuy = IsFirstBuy;
        this.Canbuy = Canbuy;
    }

   
    public virtual void BuyPack()
    {
        CtrlDataGame.Ins.AddCoin(Value);

        if (isFirstBuy)
        {
            isFirstBuy = false;
            CtrlDataGame.Ins.AddCoin(Reward);
            PackCtrl.Ins.SavePackCoins();
            FirstBuy.gameObject.SetActive(false);
        }
        
    }

    public virtual void LoadStatus()
    {
        if (!isBuy)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

        
    }
}
