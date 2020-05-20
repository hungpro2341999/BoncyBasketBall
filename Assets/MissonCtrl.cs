using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissonCtrl : MonoBehaviour
{
    public const string Key_Misson = "Key_Mission";
    public static MissonCtrl Ins;
    public ItemMission ObjDailyMission;
    public List<ItemMission> ListItemMission;
    public Transform TranMisson;
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
    private void Start()
    {
        Load();
    }
    public void Load()
    {
        var a = CtrlDataGame.Ins.MissionGame;
        for(int i = 0; i < a.Count; i++)
        {
            var mission = Instantiate(ObjDailyMission, TranMisson);
            
        }

    }


}
[System.Serializable]
public class SaveMisson
{
    public int idMisson;
    public int Process;
    public int MaxProcess;
     
}