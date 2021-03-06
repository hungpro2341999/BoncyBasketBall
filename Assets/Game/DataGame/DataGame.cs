﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeItem {Default,Item,FullItem}
[CreateAssetMenu(fileName = "Data", menuName = "Resource")]

public class DataGame : ScriptableObject
{

  

    public Head Heads;
    public Hand Hands;
    public Leg Leg;
    public Sprite Sprite_Null;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadToGame()
    {
        
    }

    public void Init()
    {
        
    } 

   
   
}
[System.Serializable]
public class Head
{
    public List<InforItem> Heads = new List<InforItem>();

   

}
[System.Serializable]
public class Hand : Head
{
  
}
[System.Serializable]
public class Leg : Head
{

}

[System.Serializable]
public class InforItem
{
    public TypeItem type;
    public int id;
    public Sprite Img;
    public int cost;
}

[System.Serializable] 
public class ListSprite
{
    public TypeItem type;
    public Sprite Img;
}



