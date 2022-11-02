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
            lefttext.color = Color.red;
            righttext.text = "WIN";
            righttext.color = Color.green;
            leftimage.color = new Color(0, 0, 0, 0.5f);
        }
        if(collision.name == "rightjegi")
        {
            Time.timeScale = 0;
            lefttext.text = "WIN";
            lefttext.color = Color.green;
            righttext.text = "LOSE";
            righttext.color = Color.red;
            rightimage.color = new Color(0, 0, 0, 0.5f);
        }
    }
}
