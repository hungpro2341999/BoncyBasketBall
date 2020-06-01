using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum typeReward { Coin, Skin, Spin }
public class Fragment : MonoBehaviour
{
    public int id;
    public typeReward type;
    public float Percentage;
    // Start is called before the first frame update
    public int reward;
    public Image Skin;

    public virtual void ApplyReward()
    {
        CtrlDataGame.Ins.AddCoin(reward);

    }
}
