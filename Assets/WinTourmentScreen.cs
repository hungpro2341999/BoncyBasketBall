using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Examples;

public class WinTourmentScreen : Screens
{
    public MixAndMatchGraphic TargetGraphic;


   
    public void ApplyGraphics()
    {


        TargetGraphic.HeadSprite = CtrlDataGame.Ins.Resource.Heads.Heads[CtrlDataGame.Ins.GetIdHead()].Img;
        if (CtrlDataGame.Ins.Resource.Hands.Heads[CtrlDataGame.Ins.GetIdHand()].type == TypeItem.FullItem)
        {
            TargetGraphic.handSprite = CtrlDataGame.Ins.Resource.Hands.Heads[CtrlDataGame.Ins.GetIdHand()].Img;
            TargetGraphic.ItemHandSprite = CtrlDataGame.Ins.Resource.Sprite_Null;
        }
        else if (CtrlDataGame.Ins.Resource.Hands.Heads[CtrlDataGame.Ins.GetIdHand()].type == TypeItem.Default)
        {
            TargetGraphic.handSprite = CtrlDataGame.Ins.Resource.Hands.Heads[CtrlDataGame.Ins.GetIdHand()].Img;
            if (CtrlDataGame.Ins.GetIdItemHand() != -1)
            {
                TargetGraphic.ItemHandSprite = CtrlDataGame.Ins.Resource.Hands.Heads[CtrlDataGame.Ins.GetIdItemHand()].Img;
            }
            else
            {
                TargetGraphic.ItemHandSprite = CtrlDataGame.Ins.Resource.Sprite_Null;
            }
            
        }
        if (CtrlDataGame.Ins.Resource.Leg.Heads[CtrlDataGame.Ins.GetIdLeg()].type == TypeItem.FullItem)
        {
            TargetGraphic.ItemLegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[CtrlDataGame.Ins.GetIdLeg()].Img;
            TargetGraphic.LegSprite = CtrlDataGame.Ins.Resource.Sprite_Null;

        }
        else
        {
            TargetGraphic.ItemLegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[CtrlDataGame.Ins.GetIdItemLeg()].Img;
            if (CtrlDataGame.Ins.GetIdItemLeg() != -1)
            {
                TargetGraphic.LegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[CtrlDataGame.Ins.GetIdLeg()].Img;
            }
            else
            {
                TargetGraphic.LegSprite = CtrlDataGame.Ins.Resource.Sprite_Null;
            }
                

        }




        TargetGraphic.Apply();
    }
    private void OnEnable()
    {
        ApplyGraphics();
    }
    public override void EventOpen()
    {
        ApplyGraphics();
    }



}
