using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    float rotateY;
    [SerializeField] float amount;

    void Update()
    {
        rotateY += amount;
        if(rotateY >= 360)
            rotateY = 0;
        transform.rotation = Quaternion.Euler(0, rotateY, 0);
    }
}
