using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinCtrl : MonoBehaviour
{
    public static int RollFree;
    public static SpinCtrl Ins;
    public const string Key_Random_Skin = "Key_Random_Skin";
    public const string Key_Roll_Free = "Key_Roll_Free";
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
    public static int FragmentSelect;
    public static bool StopSpin;
    public Button ButtonRoll;
    private void Awake()
    {
        if(Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PercentageSpin = new int[List_Fragment.Count];
        for(int i = 0; i < List_Fragment.Count; i++)
        {
            PercentageSpin[i] = (int)List_Fragment[i].Percentage;
        }
        StopSpin = true;

        if (!PlayerPrefs.HasKey(Key_Roll_Free))
        {
            PlayerPrefs.SetInt(Key_Roll_Free, 0);
            PlayerPrefs.Save();
        }
        LoadStatus();


    }
    public void AddFreeRoll()
    {
        int roll = PlayerPrefs.GetInt(Key_Roll_Free, 0);
        if (roll <= 0)
        {
            roll += 1;
            PlayerPrefs.SetInt(Key_Roll_Free, roll);
            PlayerPrefs.Save();

        }
        LoadStatus();

    }
    public int GetFreeRoll()
    {
        return PlayerPrefs.GetInt(Key_Roll_Free);
    }
    public bool HasRandomSkin()
    {
        return PlayerPrefs.HasKey(Key_Random_Skin);
    }

    public void LoadStatus()
    {
        var roll = GetFreeRoll();
        if (roll > 0)
        {
            ButtonRoll.interactable = true;
        }
        else
        {
            ButtonRoll.interactable = false;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
       
       
            if (Input.GetKeyDown(KeyCode.P))
            {
                Spin.eulerAngles = Vector3.zero;
            }
        if (StopSpin)
            return;

            if (FrictionSpin <= 0)
            {
                FrictionSpin = -1;
                isRolling = false;
                StopSpin = true;
                GameMananger.Ins.OpenSingle(TypeScreen.ShowReward);
            LoadStatus();

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
    
    public void ApplyChageFragmentSkin()
    {
        List_Fragment[5].Skin.sprite = GetSkin();
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
            FragmentSelect = 0;
            return 0;
        }
        else if(r>50 && r <= 72)
        {
            FragmentSelect = 1;
            return 1;
        }
        else if(r>72 && r <= 87)
        {
            FragmentSelect = 2;
            return 2;
        }
        else if(r>=87 && r<= 94)
        {
            FragmentSelect = 3;
            return 3;
        }
        else if(r>94 && r <= 99)
        {
            FragmentSelect = 4;
            return 4;
        }
        else
        {
            FragmentSelect = 5;
            return 5;
        }

       
        return 0;
    }

    public void StartRolling()
    {
        if (isRolling)
            return;
        int r = GetFreeRoll();
        if (r > 0)
        {
            EarnFreeRoll();
            StopSpin = false;
            Spin.eulerAngles = Vector3.zero;
            int item = GetRandom();
            Debug.Log("Item : " + item);
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


    }

    

    public void StartRollSpin()
    {
        if (isRolling)
            return;
        GameMananger.Ins.RewardVideo(StartRolling);
    }
    public void AddRoll()
    {
        if (isRolling)
            return;
        GameMananger.Ins.RewardVideo(AddFreeRoll);
    }


    public void ResetSpin()
    {

          
            
         
       
      
    }

    public void RandomSkin()
    {
        int rSkin = Random.Range(0, 3);


        switch (rSkin)
        {
            case 0:

                int r = Random.Range(0, ShopCtrl.Ins.Item_Heads.Count);

                while(true)
                {
                    if(isBuy(r, TypeShop.Shop_Head))
                    {
                        SkinSave save = new SkinSave( TypeShop.Shop_Head,r);
                        string json = JsonUtility.ToJson(save);
                        PlayerPrefs.SetString(Key_Random_Skin, json);
                        PlayerPrefs.Save();
                        break;
                    }

                    r = Random.Range(0, ShopCtrl.Ins.Item_Heads.Count);
                }

                break;
            case 1:


                int rr = Random.Range(0, ShopCtrl.Ins.Item_Hands.Count);

                while (true)
                {
                    if (isBuy(rr, TypeShop.Shop_Hand))
                    {
                        SkinSave save = new SkinSave(TypeShop.Shop_Hand, rr);
                        string json = JsonUtility.ToJson(save);
                        PlayerPrefs.SetString(Key_Random_Skin, json);
                        PlayerPrefs.Save();
                        break;
                    }

                    rr = Random.Range(0, ShopCtrl.Ins.Item_Hands.Count);
                }

                break;
            case 2:
                int rrr = Random.Range(0, ShopCtrl.Ins.Item_Legs.Count);

                while (true)
                {
                    if (isBuy(rrr, TypeShop.Shop_Leg))
                    {
                        SkinSave save = new SkinSave(TypeShop.Shop_Leg, rrr);
                        string json = JsonUtility.ToJson(save);
                        PlayerPrefs.SetString(Key_Random_Skin, json);
                        PlayerPrefs.Save();
                        break;
                    }

                    rrr = Random.Range(0, ShopCtrl.Ins.Item_Legs.Count);
                }
                break;
        }
       

    }

    public SkinSave GetSkinSave()
    {
        return JsonUtility.FromJson<SkinSave>(PlayerPrefs.GetString(Key_Random_Skin));
    }

    public Sprite GetSkin()
    {
        var skin = JsonUtility.FromJson<SkinSave>(PlayerPrefs.GetString(Key_Random_Skin));
        switch (skin.typeSkin)
        {
            case TypeShop.Shop_Head:
                return ShopCtrl.Ins.Item_Heads[skin.rSkin].Img.sprite;
               
            case TypeShop.Shop_Hand:
                return ShopCtrl.Ins.Item_Hands[skin.rSkin].Img.sprite;
                
            case TypeShop.Shop_Leg:
                return ShopCtrl.Ins.Item_Legs[skin.rSkin].Img.sprite;
               
        }
        return null;
    }
    public bool isBuy(int i,TypeShop type)
    {
        switch (type)
        {
            case TypeShop.Shop_Hand:
                
                 
                if (ShopCtrl.Ins.Item_Heads[i].isBuy)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                break;
            case TypeShop.Shop_Leg:

              
                if (ShopCtrl.Ins.Item_Legs[i].isBuy)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                break;
            case TypeShop.Shop_Head:

                if (ShopCtrl.Ins.Item_Heads[i].isBuy)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                break;
        }
        return false;
    }

    

    

     public void Started()
    {
        Debug.Log("Start");
      
        StartRolling();
    }

    public void EarnFreeRoll()
    {
        int r = GetFreeRoll();
        r--;
        r = Mathf.Clamp(r, 0, 100);
        PlayerPrefs.SetInt(Key_Roll_Free, r);
        PlayerPrefs.Save();
    }
   
 

    
}
[System.Serializable]
public class SkinSave
{
    public TypeShop typeSkin;
    public int rSkin;
    public SkinSave(TypeShop typeSkin, int rSkin)
    {
        this.typeSkin = typeSkin;
        this.rSkin = rSkin;
    }
}
