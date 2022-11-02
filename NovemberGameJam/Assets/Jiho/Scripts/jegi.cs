using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jegi : MonoBehaviour
{
    public GameObject[] clickline;
    
    void Start()
    {
    }

    void Update()
    {
        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S))&& clickline[0].GetComponent<clickline>().isclick)
        {
            clickline[0].GetComponent<clickline>().isclick = false;
            StartCoroutine(lineoff(0));
        }
        if ((Input.GetKeyUp(KeyCode.K) || Input.GetKeyUp(KeyCode.L)) && clickline[1].GetComponent<clickline>().isclick)
        {
            clickline[1].GetComponent<clickline>().isclick = false;
            StartCoroutine(lineoff(1));
        }
    }

    IEnumerator lineoff(int num)
    {
        clickline[num].SetActive(true);
        yield return new WaitForSeconds(0.3f);
        clickline[num].SetActive(false);
    }
}
