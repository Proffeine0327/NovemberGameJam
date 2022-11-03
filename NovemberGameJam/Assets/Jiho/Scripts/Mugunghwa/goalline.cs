using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalline : MonoBehaviour
{
    [SerializeField]
    TextMesh[] tm;
    [SerializeField]
    GameObject Mugunghwamanager;
    [SerializeField]
    GameObject Tagger;
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
                Tagger.GetComponent<tagger>().isnotend = false;
                tm[0].color = Color.green;
                tm[0].text = "Win!";
                tm[1].color = Color.red;
                tm[1].text = "Lose!";
                Mugunghwamanager.GetComponent<player>().enabled = false;
                Tagger.GetComponent<tagger>().gameend();
            }
            if (collision.name == "rightplayer")
            {
                Tagger.GetComponent<tagger>().isnotend = false;
                tm[0].color = Color.red;
                tm[0].text = "Lose!";
                tm[1].color = Color.green;
                tm[1].text = "Win!";
                Mugunghwamanager.GetComponent<player>().enabled = false;
                Tagger.GetComponent<tagger>().gameend();
            }
        }
    }

    
}
