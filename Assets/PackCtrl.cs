using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum TypePack { Coin,Skin,Ads}
public class PackCtrl : MonoBehaviour
{
    public const string Key_Pack = "Key_Pack";
    public static PackCtrl Ins;
    public List<Pack> PackSkin;
    public List<Pack> PackCoin;

    public Button SelectPackCoins;
    public Button SelectPackSkins;
    public List<Windown> Windowns;

    private void Awake()
    {
        if (Ins != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Ins = this;
        }

        Init();
    }

    public void Init()
    {
      //  PlayerPrefs.DeleteKey(Key_Pack);
        if (!PlayerPrefs.HasKey(Key_Pack))
        {
            List<InforPack> ListInforPack = new List<InforPack>();
            for(int i = 0; i < PackCoin.Count; i++)
            {
                InforPack infor = new InforPack();
                infor.idPack = i;
                ListInforPack.Add(infor);

            }

            for(int i = 0; i < ListInforPack.Count; i++)
            {
                if (i == 0)
                {
                    ListInforPack[i].FirstBuy = false;
                    ListInforPack[i].CanBuy = true;
                    ListInforPack[i].typePack = TypePack.Coin;


                }
                else if (i != 5)
                {
                    ListInforPack[i].FirstBuy = true;
                    ListInforPack[i].CanBuy = true;
                    ListInforPack[i].typePack = TypePack.Coin;
                }
                else
                {
                    ListInforPack[i].FirstBuy = true;
                    ListInforPack[i].CanBuy = true;
                    ListInforPack[i].typePack = TypePack.Ads;

                }
            }

            ListPack Packs = new ListPack(ListInforPack);
            
            string json = JsonUtility.ToJson(Packs);
            PlayerPrefs.SetString(Key_Pack, json);
            PlayerPrefs.Save();

        }


        var a = GetPackCoin();
        Debug.Log("PACKS : " + a.Count);
        for(int i = 0; i < PackCoin.Count; i++)
        {
            PackCoin[i].LoadPack(a[i].FirstBuy, a[i].CanBuy);
        }
        
    }

   


    public List<InforPack> GetPackCoin()
    {
        return JsonUtility.FromJson<ListPack>(PlayerPrefs.GetString(Key_Pack)).Packs;
    }

    public void SavePackCoins()
    {
        List<InforPack> ListInforPack = new List<InforPack>();
        for (int i = 0; i < PackCoin.Count; i++)
        {
            InforPack infor = new InforPack();
            infor.idPack = i;
            infor.FirstBuy = PackCoin[i].isFirstBuy;
            infor.CanBuy = PackCoin[i].Canbuy;
            ListInforPack.Add(infor);


        }

        ListPack Packs = new ListPack(ListInforPack);

        string json = JsonUtility.ToJson(Packs);
        PlayerPrefs.SetString(Key_Pack, json);
        PlayerPrefs.Save();
    }

    public void Open(Windown windown)
    {
        for(int i = 0; i < Windowns.Count; i++)
        {
            if(windown.type == Windowns[i].type)
            {
                Windowns[i].Open();
            }
            else
            {
                Windowns[i].Close();
            }
        }

        if(windown.type == TypeShop.Shop_Pack_Coin)
        {
            SelectPackCoins.gameObject.SetActive(false);
            SelectPackSkins.gameObject.SetActive(true);
        }
        else
        {

            SelectPackCoins.gameObject.SetActive(true);
            SelectPackSkins.gameObject.SetActive(false);
        }
    }
}


[System.Serializable]
public class ListPack
{
    public ListPack(List<InforPack> Packs)
    {
        this.Packs = Packs;
    }
    public List<InforPack> Packs = new List<InforPack>();
}




[System.Serializable]
public class InforPack
{
    public TypePack typePack;
    public int idPack;
  
    public bool FirstBuy = false;
  
    public bool CanBuy;

    public InforPack()
    {

    }
    public InforPack(TypePack typePack,bool FirstBuy,bool CanBuy)
    {
        this.typePack = typePack;
        this.FirstBuy = FirstBuy;
        this.CanBuy = CanBuy;
    }
}