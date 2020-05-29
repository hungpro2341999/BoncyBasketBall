using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCtrl : MonoBehaviour
{
 
    public List<Fragment> List_Fragment = new List<Fragment>();
    public int[] PercentageSpin;
    public float Speed;
    public float FrictionSpin;
    public Transform SpinRoll;
    public Transform Spin;

    public static bool isRolling = false;
    public Vector2 RollCoin50;
    public Vector2 RollCoin200;
    public Vector2 RollCoin500;
    public Vector2 RollCoin1000;
    public Vector2 RollSpin;
    public Vector2 RollItem;

    public Vector2 ApplyRoll;
    public float timeDecresion = 0.02f;
    public bool isStartRolling = false;
    public int i = 0;
    public float speedLoop;
    public List<Transform> LoopLight;

    // Start is called before the first frame update
    void Start()
    {
        PercentageSpin = new int[List_Fragment.Count];
        for(int i = 0; i < List_Fragment.Count; i++)
        {
            PercentageSpin[i] = (int)List_Fragment[i].Percentage;
        }

    }

    // Update is called once per frame
    void Update()
    {
       
       
            if (Input.GetKeyDown(KeyCode.P))
            {
                Spin.eulerAngles = Vector3.zero;
            }
            if (FrictionSpin <= 0)
            {
                FrictionSpin = -1;
                isRolling = false;

            }
            else
            {
                isRolling = true;
                Vector3 pos = Spin.rotation.eulerAngles;
                FrictionSpin -= timeDecresion * 0.1f;
                pos.z += timeDecresion * Speed * FrictionSpin;
                Spin.transform.eulerAngles = pos;
            }

        if (speedLoop < 0)
        {
            speedLoop = 0.5f;
            for(int i = 0; i < LoopLight.Count; i++)
            {
                bool active = LoopLight[i].gameObject.activeSelf;
                LoopLight[i].gameObject.SetActive(!active);
            }
        }
        else
        {
            speedLoop -= Time.deltaTime;
        }
         
       

       
      
    }
    

    public int GetRandom()
    {
        int x = PercentageSpin[0];
        int x1 = PercentageSpin[1];
        int x2 = PercentageSpin[2];
        int x3 = PercentageSpin[3];
        int x4 = PercentageSpin[4];
       
        int r = Random.Range(0, 101);
        if(r>=0 && r <= 50)
        {
            return 0;
        }
        else if(r>50 && r <= 72)
        {
            return 1;
        }
        else if(r>72 && r <= 87)
        {
            return 2;
        }
        else if(r>=87 && r<= 94)
        {
            return 3;
        }
        else if(r>94 && r <= 99)
        {
            
            return 4;
        }
        else
        {
         
            return 5;
        }

       
        return 0;
    }

    public void StartRolling()
    {
        if (isRolling)
            return;
        Spin.eulerAngles = Vector3.zero;
        int item = GetRandom();
        Debug.Log("Item : " +item);
        switch (item)
        {
            case 0:
                ApplyRoll = RollCoin50;
                break;
            case 1:
                ApplyRoll = RollCoin200;
                break;
            case 2:
                ApplyRoll = RollCoin500;
                break;
            case 3:
                ApplyRoll = RollCoin1000;
                break;
            case 4:
                ApplyRoll = RollSpin;
                break;
            case 5:
                ApplyRoll = RollItem;
                break;


        }



        FrictionSpin = ApplyRoll.x;
       

    }

    public void RollSpinByAds()
    {
        GameMananger.Ins.RewardVideo(StartRolling);
    }


    public void ResetSpin()
    {

          
            
         
       
      
    }

     public void Started()
    {
        Debug.Log("Start");
        StartRolling();
    }

   
 

    
}
