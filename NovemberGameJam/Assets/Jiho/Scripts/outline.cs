using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class outline : MonoBehaviour
{
    [SerializeField]
    Text lefttext;
    [SerializeField]
    Text righttext;
    [SerializeField]
    Image leftimage;
    [SerializeField]
    Image rightimage;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "leftjegi")
        {
            Time.timeScale = 0;
            lefttext.text = "LOSE";
            righttext.text = "WIN";
            leftimage.color = new Color(0, 0, 0, 50);
        }
        if(collision.name == "rightjegi")
        {
            Time.timeScale = 0;
            lefttext.text = "WIN";
            righttext.text = "LOSE";
            rightimage.color = new Color(0, 0, 0, 50);
        }
    }
}
