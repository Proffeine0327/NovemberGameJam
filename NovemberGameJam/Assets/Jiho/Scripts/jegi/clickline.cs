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
            StartCoroutine(kicksprite(collision));
            collision.GetComponent<Rigidbody2D>().AddForce(transform.up * power);
            StartCoroutine(clicktrue());
        }
    }

    IEnumerator clicktrue()
    {
        yield return new WaitForSeconds(0.1f);
        isclick = true;
    }

    IEnumerator kicksprite(Collider2D col)
    {
        col.GetComponent<jegiobject>().ischange = false;
        col.GetComponent<SpriteRenderer>().sprite = col.GetComponent<jegiobject>().sprites[0];
        yield return new WaitForSeconds(0.05f);
        col.GetComponent<jegiobject>().ischange = true;
    }
}
