using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickline : MonoBehaviour
{
    public int power;
    public bool isclick;
    void Start()
    {
        isclick = true;
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "jegi")
        {
            collision.GetComponent<Rigidbody2D>().AddForce(transform.up * power);
            StartCoroutine(clicktrue(collision));
        }
    }

    IEnumerator clicktrue(Collider2D col)
    {
        yield return new WaitForSeconds(0.1f);
        isclick = true;
    }
}
