using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlDataGame : MonoBehaviour
{

    public static string Key_Head = "Head";
    public static string Key_Hand = "Hand";
    public static string Key_Item_Hand = "Item_Hand";
    public static string Key_Leg = "Leg";
    public static string Key_Item_Leg = "Item_Leg";

    public DataGame Resource;


    public static CtrlDataGame Ins;


    [Header("TargetCharacter")]
    public EquipButtonExample TargetCharacter;

    // Start is called before the first frame update
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

    public void Init()
    {
        if (!PlayerPrefs.HasKey(Key_Head))
        {
            PlayerPrefs.SetInt(Key_Head, 0);
           
        }
        if (!PlayerPrefs.HasKey(Key_Hand))
        {
            PlayerPrefs.SetInt(Key_Head, 0);
        }
        if (!PlayerPrefs.HasKey(Key_Item_Hand))
        {
            PlayerPrefs.SetInt(Key_Head, 0);
        }
        if (!PlayerPrefs.HasKey(Key_Leg))
        {
            PlayerPrefs.SetInt(Key_Head, 0);
        }
        if (!PlayerPrefs.HasKey(Key_Item_Leg))
        {
            PlayerPrefs.SetInt(Key_Head, 0);
        }




    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ApplySkin();
        }
    }

    public void SetHand(int id)
    {
        PlayerPrefs.SetInt(Key_Hand, id);
        PlayerPrefs.Save();
    }

    public void SetItemHand(int id)
    {
        PlayerPrefs.SetInt(Key_Item_Hand, id);
        PlayerPrefs.Save();
    }

    public void SetLeg(int id)
    {
        PlayerPrefs.SetInt(Key_Leg, id);
        PlayerPrefs.Save();
    }

    public void SetItemLeg(int id)
    {
        PlayerPrefs.SetInt(Key_Item_Leg, id);
        PlayerPrefs.Save();
    }

    public void SetHead(int id)
    {
        PlayerPrefs.SetInt(Key_Head,id);
        PlayerPrefs.Save();
    }


    public int GetIdHead()
    {
        return PlayerPrefs.GetInt(Key_Head);
    }
    public int GetIdLeg()
    {
        return PlayerPrefs.GetInt(Key_Leg);
    }
    public int GetIdItemLeg()
    {
        return PlayerPrefs.GetInt(Key_Item_Leg);
    }
    public int GetIdHand()
    {
        return PlayerPrefs.GetInt(Key_Hand);
    }
    public int GetIdItemHand()
    {
        return PlayerPrefs.GetInt(Key_Item_Hand);
    }



    public Sprite GetHand()
    {
        int id = PlayerPrefs.GetInt(Key_Head, 0);
        return CtrlDataGame.Ins.Resource.Hands.Heads[id].Img;
    }
    public Sprite GetItemHand()
    {
        int id = PlayerPrefs.GetInt(Key_Item_Hand, 0);
        return CtrlDataGame.Ins.Resource.Hands.Heads[id].Img;
    }
    public Sprite GetLeg()
    {
        int id = PlayerPrefs.GetInt(Key_Head, 0);
        return CtrlDataGame.Ins.Resource.Leg.Heads[id].Img;
    }
    public Sprite GetItemLeg()
    {
        int id = PlayerPrefs.GetInt(Key_Head, 0);
        return CtrlDataGame.Ins.Resource.Leg.Heads[id].Img;
    }
    public Sprite GetHead()
    {
        int id = PlayerPrefs.GetInt(Key_Head, 0);
        return CtrlDataGame.Ins.Resource.Heads.Heads[id].Img;
    }

    public void ChangeHand(Sprite s)
    {
        TargetCharacter.Hand.sprite = s;
    }
    public void ChangeItemHand(Sprite s)
    {
        TargetCharacter.ItemHand.sprite = s;
    }
    public void ChangeHead(Sprite s)
    {
        TargetCharacter.Head.sprite = s;
    }
    public void ChangeLeg(Sprite s)
    {
        TargetCharacter.Leg.sprite = s;
    }
    public void ChangeItemLeg(Sprite s)
    {
        TargetCharacter.ItemLeg.sprite = s;
    }
    public void ApplySkin()
    {

        ChangeHand(GetHand());
        ChangeItemHand(GetItemHand());
        ChangeHead(GetHead());
        ChangeLeg(GetLeg());
        ChangeItemLeg(GetItemLeg());
        TargetCharacter.EquipCharacter();
        

    }

   
    
}
