using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithTime : DestroyGameObj
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (time <= 0)
        {
            EndTime();
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    public virtual void EndTime()
    {
        Destroy();
    }


}
