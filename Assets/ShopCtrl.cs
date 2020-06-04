using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

using Spine.Unity.Examples;
using UnityEngine.UI;
public enum Type_Shop {Shop_Hand,Shop_Leg_Shop_Head}

    public class ShopCtrl : MonoBehaviour
    {
    public const string Key_First_Shop = "First";
    public const string Key_Shop_Hand = "Key_Shop_Hand";
    public const string Key_Shop_Head = "Key_Shop_Head";
    public const string Key_Shop_Leg = "Key_Shop_Leg";
        
        
        public List<Windown> Shops;

        public static ShopCtrl Ins;
        public MixAndMatchGraphic TargetGraphic;

        public Transform trans_Shop_Hand;
        public Transform trans_Shop_Leg;
        public Transform trans_Shop_Head;

        public Transform trans_Push_Shop_Hand;
        public Transform trans_Push_Shop_Leg;
        public Transform trans_Push_Shop_Head;

        public Item objItem;
        public ItemHand objItemHand;
        public ItemLeg objItemLeg;
 
        [Header("Item")]
        public List<Item> Item_Heads = new List<Item>();
        public List<Item> Item_Hands = new List<Item>();
        public List<Item> Item_Legs = new List<Item>();

        [Header("Select")]
        public GameObject[] SelectHead;
        public GameObject[] SelectHand;
        public GameObject[] SelectLeg;
        
        public Item CurrSelectHand;
        public Item CurrSelectLeg;
        public Item CurrSelectHead;


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
        }
        // Start is called before the first frame update
        void Start()
        {
       

            LoadShop();
            OpenShop(TypeShop.Shop_Head);
        }

        // Update is called once per frame
        void Update()
        {
      
        }

        public void LoadShop()
        {

       //   PlayerPrefs.DeleteKey(Key_First_Shop);


        // LoadHead
        var a = CtrlDataGame.Ins.Resource.Heads.Heads;
        
        for (int i = 0; i < a.Count; i++)
        {
            var Inforitem = a[i];
            var Item = Instantiate(objItem, trans_Push_Shop_Head);
            Item.LoadItem(Inforitem.Img, Inforitem.cost, Inforitem.id, Inforitem.type);
            Item_Heads.Add(Item);
            Item.Default();

           



        }
        // :Load Hand
        var aa = CtrlDataGame.Ins.Resource.Hands.Heads;
        for(int i = 0; i < aa.Count; i++)
         {
            var Inforitem = aa[i];
            var Item = Instantiate(objItemHand, trans_Push_Shop_Hand);
            Item.LoadItem(Inforitem.Img, Inforitem.cost, Inforitem.id,Inforitem.type);
            Item_Hands.Add(Item);
            Item.Default();
        }

        // Load Leg

        var aaa = CtrlDataGame.Ins.Resource.Leg.Heads;
        for (int i = 0; i < aaa.Count; i++)
        {
            var Inforitem = aaa[i];
            var Item = Instantiate(objItemLeg, trans_Push_Shop_Leg);
            Item.LoadItem(Inforitem.Img, Inforitem.cost, Inforitem.id, Inforitem.type);
            Item_Legs.Add(Item);
            Item.Default();
        }
   
        if (!PlayerPrefs.HasKey(Key_First_Shop))
        {
            PlayerPrefs.SetInt(Key_First_Shop, 0);
            PlayerPrefs.Save();

            InitShopHand();
            InitShopHead();
            InitShopLeg();

        }


        var dataHead = GetDataHead();
        for(int i = 0; i < dataHead.Count; i++)
        {
            Item_Heads[i].LoadSaveData(dataHead[i].isFree, dataHead[i].isBuy, dataHead[i].isUse);

        }

        var dataHand = GetDataHand();
        for(int i = 0; i < dataHand.Count; i++)
        {
            Item_Hands[i].LoadSaveData(dataHand[i].isFree, dataHand[i].isBuy, dataHand[i].isUse);
        }

        var dataLeg = GetDataLeg();
        for(int i = 0; i < dataLeg.Count; i++)
        {
            Item_Legs[i].LoadSaveData(dataLeg[i].isFree, dataLeg[i].isBuy, dataLeg[i].isUse);
        }



        


        // Load Key
        CurrSelectHand =  GetHandById(CtrlDataGame.Ins.GetIdHand());
        CurrSelectLeg =  GetLegById(CtrlDataGame.Ins.GetIdLeg());
        CurrSelectHead = GetHeadById(CtrlDataGame.Ins.GetIdHead());

        SelectItemHand(CurrSelectHand.idItem);
        SelectItemLeg(CurrSelectLeg.idItem);
        SelectItemHead(CurrSelectHead.idItem);

        Debug.Log(CurrSelectHand.idItem);

          TargetGraphic.Attachment_Head(CtrlDataGame.Ins.GetHead());
          if(CurrSelectHand.type  == TypeItem.Default)
        {
            Item item = null;
            if (CtrlDataGame.Ins.GetIdItemHand() != -1)
            {
               item = GetHandById(CtrlDataGame.Ins.GetIdItemHand());
            }
            
            Item item1 = GetHandById(CtrlDataGame.Ins.GetIdHand());
           // Debug.Log(CtrlDataGame.Ins.GetIdItemHand() + "  " + CtrlDataGame.Ins.GetIdHand());
          //  Debug.Log("Item : " + item.idItem + " Hand  " + item1.idItem);
            
            TargetGraphic.Attachment_Hand(item1.Img.sprite);
            if (item != null)
            {
                TargetGraphic.Attachment_Item_Hand(item.Img.sprite);
            }
            else
            {
                TargetGraphic.Attachment_Item_Hand(CtrlDataGame.Ins.Resource.Sprite_Null);
            }
           

        }
        else
        {
            TargetGraphic.Attachment_Hand(CurrSelectHand.Img.sprite);
            TargetGraphic.Attachment_Item_Hand(CtrlDataGame.Ins.Resource.Sprite_Null);
        }

        // Leg

        if (CurrSelectLeg.type == TypeItem.Default)

        {
            Item item = null;
            if (CtrlDataGame.Ins.GetIdItemLeg()!=-1)
            {
                item = GetLegById(CtrlDataGame.Ins.GetIdItemLeg());
            }
             
            Item item1 = GetLegById(CtrlDataGame.Ins.GetIdLeg());
          //  Debug.Log(CtrlDataGame.Ins.GetIdItemLeg() + "  " + CtrlDataGame.Ins.GetIdLeg());
          //  Debug.Log("Item : " + item.idItem + " Hand  " + item1.idItem);

            TargetGraphic.Attachment_Leg(item1.Img.sprite);

            if (item != null)
            {
                TargetGraphic.Attachment_Item_Leg(item.Img.sprite);
            }
            else
            {
                TargetGraphic.Attachment_Item_Leg(CtrlDataGame.Ins.Resource.Sprite_Null);
            }
           

        }
        else
        {
            TargetGraphic.Attachment_Leg(CurrSelectLeg.Img.sprite);
            TargetGraphic.Attachment_Item_Leg(CtrlDataGame.Ins.Resource.Sprite_Null);
        }


        TargetGraphic.Apply();

    }

    public void LoadSkinCurr()
    {
        TargetGraphic.Attachment_Head(CtrlDataGame.Ins.GetHead());
        TargetGraphic.Attachment_Hand(CtrlDataGame.Ins.GetHand());
        TargetGraphic.Attachment_Item_Hand(CtrlDataGame.Ins.GetItemHand());
        TargetGraphic.Attachment_Leg(CtrlDataGame.Ins.GetLeg());
        TargetGraphic.Attachment_Item_Leg(CtrlDataGame.Ins.GetItemLeg());

        TargetGraphic.Apply();

    }



    public void Init()
    {

    }

    
  

   public void OpenShop(Windown w)
    {
        Select(w.type);
        foreach (Windown s in Shops)
        {
            if (s.type == w.type)
            {
                s.Open();
            }
            else
            {
                s.Close();
            }
        }
    }

    public void OpenShop(TypeShop type)
    {
        Select(type);
        foreach (Windown w in Shops)
        {
            if(type == w.type)
            {
                w.Open();
            }
            else
            {
                w.Close();
            }
        }
    }

    public void Select(TypeShop type)
    {

       
            SelectHand[0].gameObject.SetActive(false);
       

      
            SelectHead[0].gameObject.SetActive(false);
       
            SelectLeg[0].gameObject.SetActive(false);
        
        switch (type)
        {
            case TypeShop.Shop_Hand:
                SelectHand[0].gameObject.SetActive(true);
                break;
            case TypeShop.Shop_Head:
                SelectHead[0].gameObject.SetActive(true);
                break;
            case TypeShop.Shop_Leg:
                SelectLeg[0].gameObject.SetActive(true);
                break;
        }
    }

    public TypeItem GetTypeHandByID(int id)
    {
        for(int i = 0; i < Item_Hands.Count; i++)
        {
            if(Item_Hands[i].idItem == id)
            {
                return Item_Hands[i].type;
            }
        }
        return TypeItem.Default;
    }

    public TypeItem GetTypeLegByID(int id)
    {
        for (int i = 0; i < Item_Legs.Count; i++)
        {
            if (Item_Legs[i].idItem == id)
            {
                return Item_Legs[i].type;
            }
        }
        return TypeItem.Default;
    }

    public Item GetHandById(int id )
    {
        for(int i = 0; i < Item_Hands.Count; i++)
        {
            if(Item_Hands[i].idItem == id)
            {
                return Item_Hands[i];
            }
        }
        return null;
    }

    public Item GetLegById(int id)
    {
        for (int i = 0; i < Item_Legs.Count; i++)
        {
            if (Item_Legs[i].idItem == id)
            {
                return Item_Legs[i];
            }
        }
        return null;
    }

    public Item GetHeadById(int id)
    {
        for (int i = 0; i < Item_Heads.Count; i++)
        {
            if (Item_Heads[i].idItem == id)
            {
                return Item_Heads[i];
            }
        }
        return null;
    }

    private void InitShopHead()
    {
        List<ItemSave> saveHeads = new List<ItemSave>();
        for (int i = 0; i < Item_Heads.Count; i++)
        {
            ItemSave item = null;
            if (Item_Heads[i].idItem == 0 || Item_Heads[i].idItem == 1 || Item_Heads[i].idItem == 2)
            {
                item = new ItemSave(Item_Heads[i].idItem, false, false, true);
            }
            else
            {
                item = new ItemSave(Item_Heads[i].idItem, true, false, false);
            }
            saveHeads.Add(item);
            


        }

        ListItemSave ListSave = new ListItemSave(saveHeads);

        string json = JsonUtility.ToJson(ListSave);
        PlayerPrefs.SetString(Key_Shop_Head, json);
        PlayerPrefs.Save();
    }

  

    private void InitShopHand()
    {
        List<ItemSave> saveHeads = new List<ItemSave>();
        for (int i = 0; i < Item_Hands.Count; i++)
        {
            ItemSave item = null;
            if (Item_Hands[i].idItem == 0 || Item_Hands[i].idItem == 1 || Item_Hands[i].idItem == 2)
            {
                item = new ItemSave(Item_Hands[i].idItem, false, false, true);
            }
            else
            {
                item = new ItemSave(Item_Hands[i].idItem, true, false, false);
            }
            saveHeads.Add(item);


        }

        ListItemSave ListSave = new ListItemSave(saveHeads);


        string json = JsonUtility.ToJson(ListSave);
        PlayerPrefs.SetString(Key_Shop_Hand, json);
        PlayerPrefs.Save();
        Debug.Log("Data Hand : " + JsonUtility.FromJson<ListItemSave>(PlayerPrefs.GetString(Key_Shop_Hand)).ListItems.Count);
    }

    private void InitShopLeg()
    {
        List<ItemSave> saveHeads = new List<ItemSave>();
        for (int i = 0; i < Item_Legs.Count; i++)
        {
            ItemSave item = null;
            if (Item_Legs[i].idItem == 0 || Item_Legs[i].idItem == 1 || Item_Legs[i].idItem == 2)
            {
                item = new ItemSave(Item_Legs[i].idItem, false, false, true);
            }
            else
            {
                item = new ItemSave(Item_Legs[i].idItem, true, false, false);
            }
            saveHeads.Add(item);


        }

        ListItemSave ListSave = new ListItemSave(saveHeads);


        string json = JsonUtility.ToJson(ListSave);
        PlayerPrefs.SetString(Key_Shop_Leg, json);
        PlayerPrefs.Save();
    }



    public List<ItemSave> GetDataHead()
    {
        
        return JsonUtility.FromJson<ListItemSave>(PlayerPrefs.GetString(Key_Shop_Head)).ListItems;
    }

    public List<ItemSave> GetDataHand()
    {
        Debug.Log("Data Hand : " + JsonUtility.FromJson<ListItemSave>(PlayerPrefs.GetString(Key_Shop_Hand)).ListItems.Count);
        return JsonUtility.FromJson<ListItemSave>(PlayerPrefs.GetString(Key_Shop_Hand)).ListItems;
    }

    public List<ItemSave> GetDataLeg()
    {
        return JsonUtility.FromJson<ListItemSave>(PlayerPrefs.GetString(Key_Shop_Leg)).ListItems;
    }

    public void SelectItemHead(int id)
    {

        for(int i = 0; i < Item_Heads.Count; i++)
        {
            if (Item_Heads[i].idItem == id)
            {
                if (!Item_Heads[i].isBuy)
                {
                    CtrlDataGame.Ins.SetHead(Item_Heads[i].idItem);

                    Item_Heads[i].Select_2();
                }
                else
                {
                    Item_Heads[i].Select();
                }
               
            }
            else
            {
                Item_Heads[i].Unselect();
            }
        }

    }

    public void SelectItemHand(int id)
    {
        for (int i = 0; i < Item_Hands.Count; i++)
        {
            if (Item_Hands[i].idItem == id)
            {
                if (!Item_Hands[i].isBuy)
                {
                    if (Item_Hands[i].type == TypeItem.FullItem )
                    {
                        CtrlDataGame.Ins.SetHand(Item_Hands[i].idItem);
                        CtrlDataGame.Ins.SetItemHand(-1);
                    }
                    else if( Item_Hands[i].type == TypeItem.Default)
                    {
                        CtrlDataGame.Ins.SetHand(Item_Hands[i].idItem);
                    }
                    else
                    {
                        CtrlDataGame.Ins.SetItemHand(Item_Hands[i].idItem);
                    }
                    Item_Hands[i].Select_2();
                }
                else
                {
                    Item_Hands[i].Select();
                }
            }
            else
            {
                Item_Hands[i].Unselect();
            }
        }
    }

    public void SelectItemLeg(int id)
    {
        for (int i = 0; i < Item_Legs.Count; i++)
        {
            if (Item_Legs[i].idItem == id)
            {
                if (!Item_Legs[i].isBuy)
                {
                    if (Item_Legs[i].type == TypeItem.FullItem )
                    {
                        CtrlDataGame.Ins.SetLeg(Item_Legs[i].idItem);
                        CtrlDataGame.Ins.SetItemLeg(-1);
                    }
                    else if(Item_Legs[i].type == TypeItem.Default)
                    {

                        CtrlDataGame.Ins.SetLeg(Item_Legs[i].idItem);
                    }
                    else
                    {
                        CtrlDataGame.Ins.SetItemLeg(Item_Legs[i].idItem);
                    }
                   
                    Item_Legs[i].Select_2();
                }
                else
                {
                    Item_Legs[i].Select();
                }
            }
            else
            {
                Item_Legs[i].Unselect();
            }
        }
    }

     public void SaveShopHand()
     {
        List<ItemSave> listItem = new List<ItemSave>();  
        for(int i = 0; i < Item_Hands.Count; i++)
        {
            
            ItemSave item = new ItemSave(Item_Hands[i].idItem, Item_Hands[i].isBuy, Item_Hands[i].isUsing, Item_Hands[i].isFree);
            listItem.Add(item);
        }

        ListItemSave ListSave = new ListItemSave(listItem);
        Save(ListSave,Key_Shop_Hand);

    }
    public void SaveShopHead()
    {
        List<ItemSave> listItem = new List<ItemSave>();
        for (int i = 0; i < Item_Heads.Count; i++)
        {

            ItemSave item = new ItemSave(Item_Heads[i].idItem, Item_Heads[i].isBuy, Item_Heads[i].isUsing, Item_Heads[i].isFree);
            listItem.Add(item);
        }

        ListItemSave ListSave = new ListItemSave(listItem);
        Save(ListSave, Key_Shop_Head);
    }
    public void SaveShopLeg()
    {
        List<ItemSave> listItem = new List<ItemSave>();
        for (int i = 0; i < Item_Legs.Count; i++)
        {

            ItemSave item = new ItemSave(Item_Legs[i].idItem, Item_Legs[i].isBuy, Item_Legs[i].isUsing, Item_Legs[i].isFree);
            listItem.Add(item);
        }

        ListItemSave ListSave = new ListItemSave(listItem);
        Save(ListSave, Key_Shop_Leg);
    }
    private void Save(ListItemSave save,string key)
    {
        string json = JsonUtility.ToJson(save);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

}
[System.Serializable]

public class ListItemSave
{
   public List<ItemSave> ListItems = new List<ItemSave>();
   public ListItemSave(List<ItemSave> ListItems)
    {
        this.ListItems = ListItems;
    }
}

[System.Serializable]
public class ItemSave
{
   
    public int id;
    public bool isBuy;
    public bool isUse;
    public bool isFree;
    public ItemSave(int id,bool isBuy,bool isUse,bool isFree)
    {
        this.id = id;
        this.isBuy = isBuy;
        this.isUse = isUse;
        this.isFree = isFree;

    }

}





