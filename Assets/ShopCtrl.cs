using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Examples;

public enum Type_Shop {Shop_Hand,Shop_Leg_Shop_Head}

    public class ShopCtrl : MonoBehaviour
    {
        public static ShopCtrl Ins;
        public MixAndMatchGraphic TargetGraphic;

        public Transform trans_Shop_Hand;
        public Transform trans_Shop_Leg;
        public Transform trans_Shop_Head;

        public Transform trans_Push_Shop_Hand;
        public Transform trans_Push_Shop_Leg;
        public Transform trans_Push_Shop_Head;

        public Item objItem;

        [Header("Item")]
        public List<Item> Item_Heads = new List<Item>();
        public List<Item> Item_Hands = new List<Item>();

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
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadShop()
        {
            // LoadHand
            //var a = CtrlDataGame.Ins.Resource.Heads.Heads;
            //for (int i = 0; i < a.Count; i++)
            //{
            //    var Inforitem = a[i];
            //    var Item = Instantiate(objItem, trans_Push_Shop_Head);
            //    Item.LoadItem(Inforitem.Img, Inforitem.cost, Inforitem.id);
            //    Item_Heads.Add(Item);



            //}

        var aa = CtrlDataGame.Ins.Resource.Hands.Heads;
        for(int i = 0; i < aa.Count; i++)
        {
            var Inforitem = aa[i];
            var Item = Instantiate(objItem, trans_Push_Shop_Hand);
            Item.LoadItem(Inforitem.Img, Inforitem.cost, Inforitem.id);
            Item_Hands.Add(Item);
        }
        }

        public void SelectShopHand()
        {
            trans_Shop_Hand.gameObject.SetActive(true);
            trans_Shop_Head.gameObject.SetActive(false);
            trans_Shop_Leg.gameObject.SetActive(false);


        }

        public void SelectShopHead()
        {
            trans_Shop_Hand.gameObject.SetActive(false);
            trans_Shop_Head.gameObject.SetActive(true);
            trans_Shop_Leg.gameObject.SetActive(false);
        }

        public void SelectShopLeg()
        {
            trans_Shop_Hand.gameObject.SetActive(false);
            trans_Shop_Head.gameObject.SetActive(false);
            trans_Shop_Leg.gameObject.SetActive(true);
        }



    }




