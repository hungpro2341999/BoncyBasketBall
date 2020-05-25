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
        if (!gameObject.activeInHierarchy)
            return;
        StartProcess();
    }
    public void ApplyGraphics()
    {
       

        TargetGraphic.HeadSprite = CtrlDataGame.Ins.Resource.Heads.Heads[Skin[0]].Img;
        if(CtrlDataGame.Ins.Resource.Hands.Heads[Skin[1]].type == TypeItem.FullItem)
        {
            TargetGraphic.handSprite = CtrlDataGame.Ins.Resource.Hands.Heads[Skin[1]].Img;
            TargetGraphic.ItemHandSprite = CtrlDataGame.Ins.Resource.Sprite_Null;
        }
        else if(CtrlDataGame.Ins.Resource.Hands.Heads[Skin[1]].type == TypeItem.Default)
        {
            TargetGraphic.handSprite = CtrlDataGame.Ins.Resource.Hands.Heads[Skin[1]].Img;
            TargetGraphic.ItemHandSprite = CtrlDataGame.Ins.Resource.Hands.Heads[Skin[2]].Img;
        }
        if (CtrlDataGame.Ins.Resource.Leg.Heads[Skin[3]].type == TypeItem.FullItem)
        {
            TargetGraphic.ItemLegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[Skin[3]].Img;
            TargetGraphic.LegSprite = CtrlDataGame.Ins.Resource.Sprite_Null;

        }
        else
        {
            TargetGraphic.ItemLegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[Skin[4]].Img;
            TargetGraphic.LegSprite = CtrlDataGame.Ins.Resource.Leg.Heads[Skin[3]].Img;

        }


       
      
        TargetGraphic.Apply();
    }


    public static int[] RandomSKin()
    {
       // Debug.Log("Hand : " + CtrlDataGame.Ins.TargetCharacter.HandAsset.Length);
        int idHand = CtrlDataGame.Ins.TargetCharacter.HandAsset[Random.Range(0, CtrlDataGame.Ins.TargetCharacter.HandAsset.Length)].idItem;
        int idItemHand = CtrlDataGame.Ins.TargetCharacter.ItemHandAsset[Random.Range(0, CtrlDataGame.Ins.TargetCharacter.ItemHandAsset.Length)].idItem;
        int idLeg = CtrlDataGame.Ins.TargetCharacter.LegAsset[Random.Range(0, CtrlDataGame.Ins.TargetCharacter.LegAsset.Length)].idItem;
        int idItemLeg = CtrlDataGame.Ins.TargetCharacter.LegAsset[Random.Range(0, CtrlDataGame.Ins.TargetCharacter.ItemLegAsset.Length)].idItem;
        int[] Skin = new int[5];
        Skin[0] = Random.Range(0, CtrlDataGame.Ins.Resource.Heads.Heads.Count);
        Skin[1] = idHand;
        Skin[2] = idItemHand;
        Skin[3] = idLeg;
        Skin[4] = idItemLeg;
        return Skin;
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
