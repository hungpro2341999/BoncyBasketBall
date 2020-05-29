using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMission : MonoBehaviour
{

    public int id;
    [SerializeField]
    
    public reward[] reward;
    public int Curr;
    public bool isCompleteMission = false;
    public int ProcessCurr;
    public Image Img_Process;
    public Button ActiveClaimReward;
    public Button ActiveRewardX3;
    public int[] LoppMisison;
    public int[] LoopMisisonCurr;

    public Text textProcessMission;
    public Text text_Mission;
    public Text text_Reward;
    public string Tittle;
   
    public bool UpdateProcess;

    private float TargetProcess;
    private float currProcess;
    private void Start()
    {
       
        ActiveClaimReward.onClick.AddListener(Claim);
        ActiveClaimReward.gameObject.SetActive(false);
        ActiveRewardX3.gameObject.SetActive(false);
    }

   

    // Start is called before the first frame update

    public virtual void ApplyMisson()
    {

    }


    public virtual void UpdateMission(bool Load)
    {
        if (Load)
        {
            LoadCurrProcess();
        }
        

        if (IsActiveMission())
        {

            ActiveClaimReward.gameObject.SetActive(true);
            ActiveRewardX3.gameObject.SetActive(true);

            //    Img_Process.fillAmount = 1;
            StartProcess(1);
            textProcessMission.text = ProcessCurr + "/" + reward[Curr].TargetMission;
           
        }
        else
        {
         

            Debug.Log(ProcessCurr + "  " + reward[Curr].TargetMission);
            float prec = (float)ProcessCurr / (float)reward[Curr].TargetMission;
            StartProcess(prec);
            //     Debug.Log(prec);
            //    Img_Process.fillAmount = prec;
            ActiveClaimReward.gameObject.SetActive(false);
            ActiveRewardX3.gameObject.SetActive(false);

            textProcessMission.text = ProcessCurr+ "/" + reward[Curr].TargetMission;

        }
        string s = "";

        s = Tittle.Replace("X", reward[Curr].TargetMission.ToString());

        text_Mission.text = s;
        text_Reward.text = reward[Curr].RewardValue.ToString();
        


    }

    private void Update()
    {
        if (!gameObject.activeSelf)

            return; 
        if (UpdateProcess)
        {
            currProcess = Mathf.MoveTowards(currProcess, TargetProcess, Time.deltaTime);

            float fill = currProcess;

            Img_Process.fillAmount = Mathf.Clamp(fill,0,1);
            if (currProcess == TargetProcess)
            {
                UpdateProcess = false;

                if (Img_Process.fillAmount == 1)
                {

                    ActiveClaimReward.gameObject.SetActive(true);
                    ActiveRewardX3.gameObject.SetActive(true);
                }
            }
            
        }
    }

    public void StartProcess(float prec)
    {
        UpdateProcess = true;
        TargetProcess = prec;
    }

    public void ClaimReward()
    {
        ManagerAds.Ins.ShowRewardedVideo((done) =>{

            if (done)
            {
                ProcessCurr -= reward[Curr].TargetMission;
                int coint_reward = reward[Curr].RewardValue * 2;
                CtrlDataGame.Ins.AddCoin(coint_reward);



                Curr = RandomMission();

                UpdateMission(false);
                MissonCtrl.Ins.SaveMission();
                currProcess = 0;
                Img_Process.fillAmount = 0;
            }
          
            
        }
        );
      
       
    }

    public void Claim()
    {

        ProcessCurr -= reward[Curr].TargetMission;
        int coint_reward = reward[Curr].RewardValue;
        CtrlDataGame.Ins.AddCoin(coint_reward);


        Curr = RandomMission();

        UpdateMission(false);
        MissonCtrl.Ins.SaveMission();
        currProcess = 0;
        Img_Process.fillAmount = 0;

      
    }


    public void LoadMisson(int id,int Curr,int ProcessCurr,int[] LoopMission)
    {
        this.id = id;
        this.Curr = Curr;
        this.ProcessCurr = ProcessCurr;
        this.LoopMisisonCurr = LoopMission;

        UpdateMission(false);
    }

    public bool IsActiveMission()
    {

      
        if (ProcessCurr >= reward[Curr].TargetMission)
        {
            return true;
            

        }


        return false;

       
    }

   public virtual void LoadCurrProcess()
    {
        ProcessCurr += 1;
    }


   

    public int RandomMission()
    {
        for(int i = 0; i < LoopMisisonCurr.Length; i++)
        {
           

            if(LoopMisisonCurr[i] < LoppMisison[i])
            {
                LoopMisisonCurr[i]++;

                
                return i;
            }

            if (i == (LoopMisisonCurr.Length - 1))
            {
                if (LoopMisisonCurr[i] >= LoppMisison[i])
                {
                    LoopMisisonCurr = new int[3];
                    LoopMisisonCurr[0]++;

                    return 0;
                }

            }


        }
       
      
        return 0;
    }



    
}
[System.Serializable]
public class reward
{
    public int RewardValue;
    public int TargetMission;
}
