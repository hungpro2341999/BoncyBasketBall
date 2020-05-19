//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using Spine.Unity.Examples;
//using Spine.Unity.Examples;
//[CustomEditor(typeof(AssetSkin))]

//public class LoadAssetSkin : Editor
//{
//    public AssetSkin Data;



//    public override void OnInspectorGUI()
//    {
//        Data = (AssetSkin)target;
//        base.OnInspectorGUI();
       

//        GUILayout.Label("ID GG Shet");
//        GUILayout.Space(20);

//        GUILayout.BeginHorizontal();

      
//        GUILayout.EndHorizontal();
//        GUILayout.BeginHorizontal();

     
//        GUILayout.EndHorizontal();

//        GUILayout.Space(20);

//        GUILayout.BeginHorizontal();

//        if (GUILayout.Button("Load"))
//        {
//            Debug.Log("Load");
//            Load_Asset_Head();
//        }

//        GUILayout.EndHorizontal();
//    }



//    public void Load_Asset_Head()
//    {
//        var a = GameHelper.GetAllAssetAtPath<EquipAssetExample>(null, "Assets/Game/AssetSkin/HeadAsset");
//        var aa = GameHelper.GetAllAssetAtPath<EquipAssetExample>(null, "Assets/Game/AssetSkin/LegAsset");
//        var aaa = GameHelper.GetAllAssetAtPath<EquipAssetExample>(null, "Assets/Game/AssetSkin/HandAsset");
//        Data.AssetHand = aaa;
//        Data.AssetLeg = aa;
//        Data.AssetHead = a;

      

//        for (int i = 0; i < a.Count; i++)
//        {
//            a[i].idItem = i;
//        }

     

//        for (int i = 0; i < aa.Count; i++)
//        {
//            aa[i].idItem = i;
//        }

      
//        for (int i = 0; i < aaa.Count; i++)
//        {
//            aaa[i].idItem = i;
//        }

//    }



//}
