using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine.Unity.Examples;

public class UI_Tourment_Rivial : MonoBehaviour
{

    public List<Image> LineProcess;
  
    public MixAndMatchGraphic TargetGraphic;

    public int[] Skin;


    private void Start()
    {
        SetRandomGraphics();
    }
    public void SetRandomGraphics()
    {
        TargetGraphic.HeadSprite = CtrlDataGame.Ins.Resource.Heads.Heads[Skin[0]].Img;
        TargetGraphic.handSprite = CtrlDataGame.Ins.Resource.Hands.Heads[Skin[1]].Img;
        TargetGraphic.ItemHandSprite = CtrlDataGame.Ins.Resource.Hands.Heads[Skin[2]].Img;
        TargetGraphic.ItemLegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[Skin[3]].Img;
        TargetGraphic.LegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[Skin[4]].Img;
        TargetGraphic.Apply();
    }
}
