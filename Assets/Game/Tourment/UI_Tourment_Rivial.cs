using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine.Unity.Examples;


public class UI_Tourment_Rivial : MonoBehaviour
{
    public string KeyMatch;
    public string KeyNextRound;
    public string KeyRoundCurr;

    public List<Image> LineProcess;
    public bool isNext = true;
    public MixAndMatchGraphic TargetGraphic;
    
    

    public float time;

    public int[] Skin;

    public int i = 0;
    public float speed;
    public bool done = false;
    public bool runProcess;
    
    public Transform TransSkin;


    private void Start()
    {
        
       
        for(int i = 0; i < LineProcess.Count; i++)
        {
            LineProcess[i].fillAmount = 0;
        }
    }
    private void Update()
    {
        StartProcess();
    }
    public void SetRandomGraphics()
    {
        Skin = new int[] { 1, 2, 4, 5, 4 };
        TargetGraphic.HeadSprite = CtrlDataGame.Ins.Resource.Heads.Heads[Skin[0]].Img;
        TargetGraphic.handSprite = CtrlDataGame.Ins.Resource.Hands.Heads[Skin[1]].Img;
        TargetGraphic.ItemHandSprite = CtrlDataGame.Ins.Resource.Hands.Heads[Skin[2]].Img;
        TargetGraphic.ItemLegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[Skin[3]].Img;
        TargetGraphic.LegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[Skin[4]].Img;
        TargetGraphic.Apply();
    }
    public void StartProcess()
    {
        if (!runProcess)
            return;
        if (!isNext)
            return;
        if (i >= LineProcess.Count)
        {
            if (!done)
            {
                if (TourmentCtrl.Ins.GetTourmnet(KeyNextRound) != null)
                {
                    TransformCharacterTo(TourmentCtrl.Ins.GetTourmnet(KeyNextRound));
                }
                done = true;
               
            }
        }
        else
        {
            if (LineProcess[i].fillAmount != 1)
            {
                LineProcess[i].fillAmount = LineProcess[i].fillAmount + speed * Time.deltaTime;
            }
            else
            {


                i++;
            }
        }
            
       
       
            
        
          
    }

    public void TransformCharacterTo(UI_Tourment_Rivial Next_Round)
    {
        Next_Round.TransSkin.gameObject.SetActive(true);
        Debug.Log("Change : " + Next_Round.KeyRoundCurr);
        Next_Round.TargetGraphic.HeadSprite = TargetGraphic.HeadSprite;
        Next_Round.TargetGraphic.handSprite = TargetGraphic.handSprite;
        Next_Round.TargetGraphic.ItemHandSprite = TargetGraphic.ItemHandSprite;
        Next_Round.TargetGraphic.LegSprite = TargetGraphic.LegSprite;
        Next_Round.TargetGraphic.ItemLegSprite = TargetGraphic.ItemLegSprite;
        Next_Round.runProcess = true;

        Next_Round.TargetGraphic.Apply();
    }

    public void SelectGraphic(int head,int hand,int itemhand,int leg,int itemleg)
    {
        TargetGraphic.HeadSprite = CtrlDataGame.Ins.Resource.Heads.Heads[Skin[head]].Img;
        TargetGraphic.handSprite = CtrlDataGame.Ins.Resource.Hands.Heads[Skin[hand]].Img;
        TargetGraphic.ItemHandSprite = CtrlDataGame.Ins.Resource.Hands.Heads[Skin[itemhand]].Img;
        TargetGraphic.ItemLegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[Skin[leg]].Img;
        TargetGraphic.LegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[Skin[itemleg]].Img;
        TargetGraphic.Apply();
    }
}
