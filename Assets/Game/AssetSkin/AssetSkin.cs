using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AssetSkin",menuName ="AssetSkin")]
public class AssetSkin : ScriptableObject
{

    public List<EquipAssetExample> AssetHead = new List<EquipAssetExample>();
    public List<EquipAssetExample> AssetHand = new List<EquipAssetExample>();
    public List<EquipAssetExample> AssetLeg = new List<EquipAssetExample>();
    public EquipAssetExample Null_Hand;
    public EquipAssetExample Null_Item_Hand;
    public EquipAssetExample Null_Leg;
    public EquipAssetExample Null_Item_Leg;
    


}
