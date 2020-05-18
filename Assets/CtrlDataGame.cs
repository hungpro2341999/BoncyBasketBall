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
    public const string KeyCoin = "Key_Coins";
    public DataGame Resource;
    public AssetSkin AssetSkin;


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
        if (!PlayerPrefs.HasKey(KeyCoin))
        {
            PlayerPrefs.SetInt(KeyCoin, 99999999);
            PlayerPrefs.Save();
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

        if (Input.GetKeyDown(KeyCode.L))
        {
            TargetCharacter.equipSystem.ResetData();
           
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
        int id = PlayerPrefs.GetInt(Key_Hand, 0);
        if (id == -1)
        {
            return CtrlDataGame.Ins.Resource.Sprite_Null;
        }
        return CtrlDataGame.Ins.Resource.Hands.Heads[id].Img;
    }
    public Sprite GetItemHand()
    {
        int id = PlayerPrefs.GetInt(Key_Item_Hand, 0);
        if (id == -1)
        {
            return CtrlDataGame.Ins.Resource.Sprite_Null;
        }
        return CtrlDataGame.Ins.Resource.Hands.Heads[id].Img;
    }
    public Sprite GetLeg()
    {
        int id = PlayerPrefs.GetInt(Key_Leg, 0);
        return CtrlDataGame.Ins.Resource.Leg.Heads[id].Img;
    }
    public Sprite GetItemLeg()
    {
        int id = PlayerPrefs.GetInt(Key_Item_Leg, 0);
        if (id == -1)
        {
            return CtrlDataGame.Ins.Resource.Sprite_Null;
        }
        return CtrlDataGame.Ins.Resource.Leg.Heads[id].Img;
    }
    public Sprite GetHead()
    {
        int id = PlayerPrefs.GetInt(Key_Head, 0);
        return CtrlDataGame.Ins.Resource.Heads.Heads[id].Img;
    }

    public void ChangeHand(int id)
    {
        TargetCharacter.EquipHand(id);
    }
    public void ChangeItemHand(int id)
    {

        TargetCharacter.EquipItemHand(id);
    }
    public void ChangeHead(int id)
    {
        TargetCharacter.EquipHead(id);
    }
    public void ChangeLeg(int id)
    {
        TargetCharacter.EquipLeg(id);
    }
    public void ChangeItemLeg(int id)
    {
        TargetCharacter.EquipItemLeg(id);
    }

   

   

   

   
    public void ApplySkin()
    {


        ChangeHand(CtrlDataGame.Ins.GetIdHand());
        if (CtrlDataGame.Ins.GetIdItemHand() != -1)
        {
           
            ChangeItemHand(CtrlDataGame.Ins.GetIdItemHand());
        }
        else
        {
           TargetCharacter.EquipItemHandNull();
        }
       
        ChangeHead(CtrlDataGame.Ins.GetIdHead());
        if (CtrlDataGame.Ins.GetIdItemLeg() != -1)
        {
            
            ChangeItemLeg(CtrlDataGame.Ins.GetIdItemLeg());
        }
        else
        {
            TargetCharacter.EquipItemLegNull();
        }
       
        ChangeLeg(CtrlDataGame.Ins.GetIdLeg());

        TargetCharacter.EquipCharacter();


    }

    public int GetCoin()
    {
        return PlayerPrefs.GetInt(KeyCoin);
    }
    public void SaveCoin(int coin)
    {
        PlayerPrefs.SetInt(KeyCoin, coin);
        PlayerPrefs.Save();
    }



}
