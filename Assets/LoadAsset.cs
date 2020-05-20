using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Spine.Unity.Examples;

[CustomEditor(typeof(EquipAssetExample))]

public class LoadAsset : Editor
{
    public DataGame Data;
    public EquipAssetExample Data1;


    public override void OnInspectorGUI()
    {
        Data1 = (EquipAssetExample)target;
        base.OnInspectorGUI();


        GUILayout.Label("ID GG Shet");
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();


        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();



        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Load"))
        {
            Debug.Log("Load");
            Load_Asset_Head();
        }

        GUILayout.EndHorizontal();
    }
    public void Load_Asset_Head()
    {
        LoadAssetHead();
        LoadAssetHand();
        LoadAssetLeg();



        List<Sprite> ImgHead = new List<Sprite>();
        List<InforItem> Hands = new List<InforItem>();
        var a = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Game/Leg");
        for (int i = 0; i < a.Count; i++)
        {
            ImgHead.Add(a[i]);
            i += 1;
        }

        for (int i = 0; i < ImgHead.Count; i++)
        {
            InforItem aa = new InforItem();
            aa.id = i;
            aa.Img = ImgHead[i];
            Hands.Add(aa);


        }

        for (int i = 0; i < ImgHead.Count; i++)
        {

            InforItem aa = new InforItem();
            aa.id = i;
            aa.Img = ImgHead[i];


            var s = aa.Img.name;



            if (s.StartsWith("0"))
            {
                aa.type = TypeItem.Default;
            }
            else if (s.StartsWith("b") || s.StartsWith("s"))
            {

                aa.type = TypeItem.Item;

            }
            else
            {
                aa.type = TypeItem.FullItem;


            }
            Hands.Add(aa);

        }
        Data.Leg.Heads = Hands;
    }
    public void LoadAssetHead()
    {
        List<Sprite> img = new List<Sprite>();
        var a = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Game/Head");
        for (int i = 0; i < a.Count; i++)
        {
            img.Add(a[i]);
            i += 1;
        }


        var aa = GameHelper.GetAllAssetAtPath<EquipAssetExample>(null, "Assets/Game/AssetSkin/HeadAsset");

        for (int i = 0; i < aa.Count; i++)
        {

            aa[i].equipType = EquipSystemExample.EquipType.Head;
            aa[i].sprite = img[i];
        }
    }
    public void LoadAssetHand()
    {
        List<Sprite> img = new List<Sprite>();
        var a = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Game/Hand");
        for (int i = 0; i < a.Count; i++)
        {
            img.Add(a[i]);
            i += 1;
        }


        var aa = GameHelper.GetAllAssetAtPath<EquipAssetExample>(null, "Assets/Game/AssetSkin/HandAsset");



        for (int i = 0; i < aa.Count; i++)
        {

            aa[i].sprite = img[i];
            if (img[i].name.StartsWith("v"))
            {
                aa[i].equipType = EquipSystemExample.EquipType.ItemHand;
            }
            else
            {
                aa[i].equipType = EquipSystemExample.EquipType.Hand;
            }
        }
    }

    public void LoadAssetLeg()
    {
        List<Sprite> img = new List<Sprite>();
        var a = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Game/Leg");
        for (int i = 0; i < a.Count; i++)
        {
            img.Add(a[i]);
            i += 1;
        }


        var aa = GameHelper.GetAllAssetAtPath<EquipAssetExample>(null, "Assets/Game/AssetSkin/LegAsset");



        for (int i = 0; i < aa.Count; i++)
        {

            aa[i].sprite = img[i];
            if (img[i].name.StartsWith("s") || img[i].name.StartsWith("b"))
            {
                aa[i].equipType = EquipSystemExample.EquipType.ItemLeg;
            }
            else
            {
                aa[i].equipType = EquipSystemExample.EquipType.Leg;
            }
        }
    }


}
