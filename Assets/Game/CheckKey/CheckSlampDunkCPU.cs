﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSlampDunkCPU : Check
{
    private void Start()
    {
       
    }
    public string KeyAciton;
    public override void CheckKey(Collider2D collision)
    {

        if (collision.gameObject.tag == "HandCPU")
        {
            //  Debug.Log("Coll : " + collision.gameObject.name + " " + collision.gameObject.tag);
            Debug.Log("Coll With");
            var AI = (AI)CtrlGamePlay.Ins.AI;
            if (AI.isBall)
            {
                AI.OnActionEFFWithKey(KeyAciton);
                AI.ArrayAction = transform.GetComponentsInChildren<Action>();
            }
           
            

            

        }
    }
    private void Update()
    {
      
    }

}
