﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataGame))]

public class LoadAsset : Editor
{
    public DataGame Data;
    public override void OnInspectorGUI()
    {
        Data = (DataGame)target;
        base.OnInspectorGUI();
        //data = (CardData)target;

        GUILayout.Label("ID GG Shet");
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();

        //ID = EditorGUILayout.TextField("ID: ", ID);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        //sheetName = EditorGUILayout.TextField("SheetName: ", sheetName);

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
        List<Sprite> ImgHead = new List<Sprite>();
       var a =  GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Game/Hand");
        for(int i = 0; i < a.Count; i++)
        {
            ImgHead.Add(a[i]);
            i += 1;
        }
        List<InforItem> Hands = new List<InforItem>();
        for(int i = 0; i < ImgHead.Count; i++)
        {

            //InforItem aa = new InforItem();
            //aa.id = i;
            //aa.Img = ImgHead[i];

            //Hands.Add(aa);

            string s = ImgHead[i].name;
            InforItem aa = new InforItem();
            aa.id = i;
            aa.Img = ImgHead[i];
          

            if (s.StartsWith("d"))
            {
                aa.type = TypeItem.Default;
            }
            else if (s.StartsWith("t"))
            {
               
                aa.type = TypeItem.FullItem;

            }
            else
            {
                aa.type = TypeItem.Item;

            }
            Hands.Add(aa);
        }
         Data.Hands.Heads = Hands;
    }

    //public void Load_A

}
