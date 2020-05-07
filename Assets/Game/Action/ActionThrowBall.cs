using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionThrowBall : Action
{
    public float Force;
    public Transform Trans;
    public override void SetAction()
    {
       
        //Debug.Log("PushBall");
        CtrlGamePlay.Ins.PushBall((Trans.position - transform.position).normalized*Force);
    }

  
}
