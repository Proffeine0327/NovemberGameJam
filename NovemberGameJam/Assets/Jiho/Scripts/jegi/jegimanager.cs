using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jegimanager : MonoBehaviour
{
    float time = 3.5f;
    [SerializeField]
    GameObject[] jegiobj;
    public GameObject Outline;
    void Start()
    {
        
    }

    void Update()
    {
        if (Outline.GetComponent<outline>().isPlayGame)
        {
            jegiobj[0].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            jegiobj[1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }   
    }
}
