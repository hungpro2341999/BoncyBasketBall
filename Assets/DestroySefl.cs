using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroySefl : Screens
{
 
    public List<Sprite> ListCoins;
    public List<GameObject> RewardSelect;
    public Text reward;
    public Transform PlushSpin;
    public Image imgReward;
    public Transform TransCoin;
    // Start is called before the first frame update

    public void Destroy()
    {
        gameObject.SetActive(true);
    }
    public override void EventOpen()
    {
        AudioCtrl.Ins.Play("GetReward");
        ShowReward(SpinCtrl.Ins.List_Fragment[SpinCtrl.FragmentSelect]);
    }
    public void ShowReward(Fragment fragment)
    {
        switch (fragment.type)
        {
            case typeReward.Coin:
                reward.gameObject.SetActive(true);
                reward.text = fragment.reward.ToString();
                imgReward.gameObject.SetActive(false);
                PlushSpin.gameObject.SetActive(false);
                TransCoin.gameObject.SetActive(true);
                fragment.ApplyReward();
                break;
            case typeReward.Skin:
                reward.gameObject.SetActive(false);
                imgReward.gameObject.SetActive(true);
                PlushSpin.gameObject.SetActive(false);
                imgReward.sprite = fragment.Skin.sprite;
                TransCoin.gameObject.SetActive(false);
                fragment.ApplyReward();
                break;
            case typeReward.Spin:
                SpinCtrl.Ins.RandomSkin();
                reward.gameObject.SetActive(false);
                imgReward.gameObject.SetActive(false);
                PlushSpin.gameObject.SetActive(true);
                TransCoin.gameObject.SetActive(false);

                fragment.ApplyReward();
                break;
        }
    }
   
    
}
