using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : MonoBehaviour
{
    public Image ImageUI;

    public virtual void ChangeImg(Sprite img)
    {
        ImageUI.sprite = img;
        ImageUI.SetNativeSize();
    }
}
