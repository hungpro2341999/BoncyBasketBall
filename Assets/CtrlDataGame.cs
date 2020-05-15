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

    public EquipAssetExample EquipHand;
    public EquipAssetExample EquipItemHand;
    public EquipAssetExample EquipLeg;
    public EquipAssetExample EquipItemLeg;
    public EquipAssetExample EquipHead;
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
        return CtrlDataGame.Ins.Resource.Hands.Heads[id].Img;
    }
    public Sprite GetHand()
    {
        int id = PlayerPrefs.GetInt(Key_Head, 0);
        return CtrlDataGame.Ins.Resource.Hands.Heads[id].Img;
    }
    public Sprite GetHand()
    {
        int id = PlayerPrefs.GetInt(Key_Head, 0);
        return CtrlDataGame.Ins.Resource.Hands.Heads[id].Img;
    }
}
