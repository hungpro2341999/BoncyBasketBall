using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

using Spine.Unity.Examples;
using UnityEngine.UI;
public enum Type_Shop {Shop_Hand,Shop_Leg_Shop_Head}

    public class ShopCtrl : MonoBehaviour
    {

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



}





