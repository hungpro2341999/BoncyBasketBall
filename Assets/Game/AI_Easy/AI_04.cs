using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_04 : AI_01
{
    public override void Start()
    {
        delayTimeMove = 0.5f;
    }

    private void MoveToPos(int posTarger)
    {
        posTarger = Mathf.Clamp(posTarger, 0, CountSperateDistance);
        Debug.Log(CurrPos + "  " + posTarger);
        if ((Mathf.Abs(CurrPos - posTarger) != 0))
        {



            if (Mathf.Sign(CurrPos - posTarger) == 1)
            {
                Debug.Log("Right");
                isMoveRight = true;
                isMoveLeft = false;
            }
            else
            {
                Debug.Log("left");
                isMoveRight = false;
                isMoveLeft = true;
            }

        }
        else
        {
            Debug.Log("None");
            isMoveLeft = false;
            isMoveRight = false;
        }


    }

    public override void OnMoveToBall()
    {
      
    }

}
