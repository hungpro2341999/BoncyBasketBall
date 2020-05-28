using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Examples;

public class VsScreen : Screen
{
    public MixAndMatchGraphic P1;
    public MixAndMatchGraphic P2; 
    public float timeRandom;
    public float timedelay;
    public float time;
    public int HeadRandoms;

    public int HandRandoms;
    public int LegRandoms;
    private float time_random;

    public static int[] SkinUse = new int[5];
    public static bool isMatchRandom;
    public static int[] P1_Skin;
    public static int[] P2_Skin;
    public bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        time_random  = timeRandom;
       
        HeadRandoms = CtrlDataGame.Ins.Resource.Heads.Heads.Count;
        HandRandoms = CtrlDataGame.Ins.Resource.Hands.Heads.Count;
        LegRandoms = CtrlDataGame.Ins.Resource.Leg.Heads.Count;
        
       
    }

    public override void OnEnableScreen()
    {
        if (isMatchRandom)
        {
            timeRandom = 5;
        }
        else
        {
            timeRandom = 3;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (timeRandom < 0)
        {
          
            GameMananger.Ins.OpenScreen(TypeScreen.Play);
        }
        else
        {
           

            timeRandom -= Time.deltaTime;
            if (time < 0)
            {
                time = timedelay;
                if (timeRandom > 2)
                {
                    if (isMatchRandom)
                    {
                        RandomSprite();
                    }
                }
               

               
               
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
    }

    public void ApplyPlayer()
    {
        P1.HeadSprite = CtrlDataGame.Ins.GetHead();
        P1.handSprite = CtrlDataGame.Ins.GetHand();
        P1.ItemHandSprite = CtrlDataGame.Ins.GetItemHand();
        P1.LegSprite = CtrlDataGame.Ins.GetLeg();
        P1.ItemLegSprite = CtrlDataGame.Ins.GetItemLeg();
        P1.Apply();
    }

    public void RandomSprite()
    {
        SkinUse[0] = Random.Range(0, HeadRandoms);
        SkinUse[1] = Random.Range(0, HandRandoms);
        SkinUse[2] = Random.Range(0, HandRandoms);
        SkinUse[3] = Random.Range(0, LegRandoms);
        SkinUse[4] = Random.Range(0, LegRandoms);

        P2.HeadSprite = CtrlDataGame.Ins.Resource.Heads.Heads[SkinUse[0]].Img;

        TypeItem typeHand = ShopCtrl.Ins.GetTypeHandByID(SkinUse[1]);
        while (typeHand == TypeItem.Item)
        {
            SkinUse[1] = Random.Range(0, HandRandoms);
            typeHand = ShopCtrl.Ins.GetTypeHandByID(SkinUse[1]);
        }
      
        TypeItem typeItemHand = ShopCtrl.Ins.GetTypeHandByID(SkinUse[2]);
       

            if (typeHand == TypeItem.Default)
        {

            P2.handSprite= CtrlDataGame.Ins.Resource.Hands.Heads[SkinUse[1]].Img;
            if(typeItemHand == TypeItem.Item)
            {
                P2.ItemHandSprite = CtrlDataGame.Ins.Resource.Hands.Heads[SkinUse[2]].Img;
            }
            else
            {
                SkinUse[2] = -1;
                P2.ItemHandSprite = CtrlDataGame.Ins.Resource.Sprite_Null;
            }

        }
        else if(typeHand == TypeItem.FullItem)
        {
            P2.handSprite= CtrlDataGame.Ins.Resource.Hands.Heads[SkinUse[1]].Img;
            SkinUse[2] = -1;
            P2.ItemHandSprite = CtrlDataGame.Ins.Resource.Sprite_Null;

        }
       


        TypeItem typeLeg = ShopCtrl.Ins.GetTypeHandByID(SkinUse[3]);
        TypeItem typeItemLeg = ShopCtrl.Ins.GetTypeHandByID(SkinUse[4]);

        while (typeLeg == TypeItem.Item)
        {
            SkinUse[3] = Random.Range(0, HandRandoms);
            typeLeg = ShopCtrl.Ins.GetTypeHandByID(SkinUse[3]);
        }


        if (typeLeg == TypeItem.Default)
        {
            P2.LegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[SkinUse[3]].Img;
            if(typeItemLeg == TypeItem.Item)
            {
                P2.ItemLegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[SkinUse[4]].Img;
            }
            else
            {

                SkinUse[4] = -1;
                P2.ItemLegSprite = CtrlDataGame.Ins.Resource.Sprite_Null;
            }
        }
        else if(typeLeg == TypeItem.FullItem)
        {
            P2.LegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[SkinUse[3]].Img;
            P2.ItemLegSprite = CtrlDataGame.Ins.Resource.Sprite_Null;
            SkinUse[4] = -1;
        }
        else
        {
            SkinUse[3] = 0;
            P2.LegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[SkinUse[3]].Img;
            SkinUse[4] = -1;
            P2.ItemLegSprite = CtrlDataGame.Ins.Resource.Sprite_Null;

        }



            P2.Apply();
    }
    public override void EventOpen()
    {
        stop = false;
        GameMananger.Ins.TrasUIGenrate.gameObject.SetActive(false);
        GameMananger.Ins.TransSetting.gameObject.SetActive(false);
        ApplyPlayer();
        if (!isMatchRandom)
        {
            P2.HeadSprite = CtrlDataGame.Ins.Resource.Heads.Heads[SkinUse[0]].Img;

            TypeItem typeHand = ShopCtrl.Ins.GetTypeHandByID(SkinUse[1]);

            TypeItem typeItemHand = ShopCtrl.Ins.GetTypeHandByID(SkinUse[2]);


            if (typeHand == TypeItem.Default)
            {

                P2.handSprite = CtrlDataGame.Ins.Resource.Hands.Heads[SkinUse[1]].Img;
                if (typeItemHand == TypeItem.Item)
                {
                    P2.ItemHandSprite = CtrlDataGame.Ins.Resource.Hands.Heads[SkinUse[2]].Img;
                }
                else
                {
                    SkinUse[2] = -1;
                    P2.ItemHandSprite = CtrlDataGame.Ins.Resource.Sprite_Null;
                }

            }
            else if (typeHand == TypeItem.FullItem)
            {
                P2.handSprite = CtrlDataGame.Ins.Resource.Hands.Heads[SkinUse[1]].Img;
                SkinUse[2] = -1;
                P2.ItemHandSprite = CtrlDataGame.Ins.Resource.Sprite_Null;

            }

            TypeItem typeLeg = ShopCtrl.Ins.GetTypeHandByID(SkinUse[3]);
            TypeItem typeItemLeg = ShopCtrl.Ins.GetTypeHandByID(SkinUse[4]);
            if (typeLeg == TypeItem.Default)
            {
                P2.LegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[SkinUse[3]].Img;
                if (typeItemLeg == TypeItem.Item)
                {
                    P2.ItemLegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[SkinUse[4]].Img;
                }
                else
                {

                    SkinUse[4] = -1;
                    P2.ItemLegSprite = CtrlDataGame.Ins.Resource.Sprite_Null;
                }
            }
            else if (typeLeg == TypeItem.FullItem)
            {
                P2.LegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[SkinUse[3]].Img;
                P2.ItemLegSprite = CtrlDataGame.Ins.Resource.Sprite_Null;
                SkinUse[4] = -1;
            }
            else
            {
                SkinUse[3] = 0;
                P2.LegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[SkinUse[3]].Img;
                SkinUse[4] = -1;
                P2.ItemLegSprite = CtrlDataGame.Ins.Resource.Sprite_Null;

            }


            P2.Apply();
        }
    }

    public static void Set_P2(int[] Skins)
    {
        SkinUse = Skins;
    }
    
}