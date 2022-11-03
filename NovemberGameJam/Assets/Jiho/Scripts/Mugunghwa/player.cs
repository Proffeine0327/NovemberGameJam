using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    GameObject[] players;
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    float speed;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            players[0].transform.position -= new Vector3(1, 0) * speed * Time.deltaTime;
            players[0].GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            players[0].GetComponent<SpriteRenderer>().sprite = sprites[1];
        }    
        if (Input.GetKey(KeyCode.L))
        {
            players[1].transform.position -= new Vector3(1, 0) * speed * Time.deltaTime;
            players[1].GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            players[1].GetComponent<SpriteRenderer>().sprite = sprites[3];
        }
    }
}
