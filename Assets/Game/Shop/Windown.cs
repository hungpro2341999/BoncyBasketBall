﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeShop { Shop_Hand,Shop_Leg,Shop_Head,Shop_Pack_Coin,Shop_Pack_Skins}

public class Windown : MonoBehaviour
{

    public TypeShop type;
    // Start is called before the first frame update
   public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
