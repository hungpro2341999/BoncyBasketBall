using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCrossOver : MonoBehaviour
{
    public Transform[] trans;
    public float speed;
    public float PosReset;
    public float PosInit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < trans.Length; i++)
        {
            if (trans[i].transform.localPosition.x <= PosReset)
            {
                Vector3 pos = trans[i].transform.localPosition;
                pos.x += speed * Time.deltaTime;
                trans[i].transform.localPosition = pos;

            }
            else
            {
                Vector3 pos = trans[i].transform.localPosition;
                pos.x = PosInit;
                trans[i].transform.localPosition = pos;
            }
          
        }
    }
}
