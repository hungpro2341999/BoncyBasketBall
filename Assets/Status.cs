using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public float timeLife;
    public Text Staus;
    public  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLife > 0)
        {
            timeLife -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}
