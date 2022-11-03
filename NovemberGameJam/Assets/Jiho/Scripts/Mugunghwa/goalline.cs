using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalline : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "human")
        {
            if(collision.name == "leftplayer")
            {
                Debug.Log("¿ÞÂÊ ÀÌ±è");
            }
            if (collision.name == "rightplayer")
            {
                Debug.Log("¿À¸¥ÂÊ ÀÌ±è");
            }
        }
    }
}
