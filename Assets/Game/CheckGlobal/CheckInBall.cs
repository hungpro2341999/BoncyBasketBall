using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoardGame { Player,CPU }
public class CheckInBall : MonoBehaviour
{
   
    public BoardGame BoardOf;
    // Start is called before the first frame update
    public Dictionary<string, int> Global = new Dictionary<string, int>();

    public const string Key_Baset_1 = "Basket_1";
    public const string Key_Baset_2 = "Basket_2";
    public const string Key_Global = "Global";

    public bool isCanGlobal = false;
    public bool isGlobal = false;
    public Transform Hool;
    public Transform TargetHool;

    private void Update()
    {
        if(BoardOf == BoardGame.Player)
        {
            Vector2 rotation = TargetHool.position - Hool.position;
            float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            Hool.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        else
        {
            Vector2 rotation =   Hool.position- TargetHool.position;
            float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            Hool.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }


    }
    private void Awake()
    {
        Init();
    }
    private void Start()
    {
       
    }

    public void Init()
    {
        Global.Add(Key_Baset_1, 0);
        Global.Add(Key_Baset_2, 0);
        Global.Add(Key_Global, 0);

    }

    public void RestorCheckBoard()
    {
        isGlobal = false;
        isCanGlobal = true;
        Global[Key_Global] = 0;
        Global[Key_Baset_1] = 0;
        Global[Key_Baset_2] = 0;
    }

    public void SetKey(string key)
    {
        if (isGlobal)

            return;

        Global[key] = 1;

        if (Global[Key_Global] != 0)
        {
            if (isCanGlobal)
            {
                if(Global[Key_Baset_2]==1 && Global[Key_Baset_1] == 0)
                {
                  
                    isCanGlobal = false;
                    return;
                }
                else
                {
                    Global[key] = 1;
                }

                if ((Global[Key_Baset_1] + Global[Key_Baset_2]) == 2)
                {
                    switch (BoardOf)
                    {
                        case BoardGame.Player:
                            CtrlGamePlay.Ins.GlobalPlayer();
                            break;
                        case BoardGame.CPU:
                            CtrlGamePlay.Ins.GlobalCPU();
                            break;
                    }

                    isGlobal = true;
                }

               
            }
            
           
        }

        

    }

    


    public void RestoreKey(string key)
    {
        if(key == "Global")
        {

            isCanGlobal = true;
            Global[Key_Global] = 0;
            Global[Key_Baset_1] = 0;
            Global[Key_Baset_2] = 0;
        }

    }
}
