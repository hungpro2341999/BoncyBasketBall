using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMission : MonoBehaviour
{
    [SerializeField]
    public Reward[] reward;
    public int Curr;
    public bool isCompleteMission = false;
    public int ProcessCurr;
    public Button ActiveClaimReward;
    public Button ActiveRewardX3;
    // Start is called before the first frame update
   
    public virtual void ApplyMisson()
    {

    }


    public virtual void UpdateMission()
    {
        if (ProcessCurr >= reward[Curr].TargetMission)
        {
            ActiveClaimReward.interactable = true;
            ActiveRewardX3.interactable = true;
        }
        else
        {
            ActiveClaimReward.interactable = false;
            ActiveRewardX3.interactable = false;
        }
    }

    public void ClaimReward()
    {

    }



    
}
[System.Serializable]
public class Reward
{
    public int ValueReward;
    public int TargetMission;
}
