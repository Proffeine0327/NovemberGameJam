using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    public int power;
    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "jegi")
        {
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * power);
        }
    }
}
