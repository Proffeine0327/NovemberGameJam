using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jegimanager : MonoBehaviour
{
    [SerializeField]
    Image countimage;
    [SerializeField]
    Text count;
    float time = 3.5f;
    [SerializeField]
    GameObject[] jegiobj;
    void Start()
    {
        
    }

    void Update()
    {
        time -= Time.deltaTime;
        count.GetComponent<Text>().text = ((int)time).ToString();
        if(time <= 1)
        {
            countimage.gameObject.SetActive(false);
            jegiobj[0].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            jegiobj[1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }   
    }
}
