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

    public bool isRolling = false;
    public Vector2 RollCoin50;
    public Vector2 RollCoin200;
    public Vector2 RollCoin500;
    public Vector2 RollCoin1000;
    public Vector2 RollSpin;
    public Vector2 RollItem;

    

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
        if (FrictionSpin < 0)
            return;
        Vector3 pos = Spin.rotation.eulerAngles;
        FrictionSpin -= Time.deltaTime * 0.1f;
        pos.z += Time.deltaTime * Speed*FrictionSpin;
        Spin.transform.eulerAngles = pos;
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

   
 

    
}
