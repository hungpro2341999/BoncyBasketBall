using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlDataGame : MonoBehaviour
{
    public DataGame Resource;
    public static CtrlDataGame Ins;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Ins != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Ins = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
