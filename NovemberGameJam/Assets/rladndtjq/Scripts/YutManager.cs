using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YutManager : MonoBehaviour
{
    [SerializeField] List<YutScript> yuts = new List<YutScript>();

    [SerializeField] Vector2 yutClampRange = new Vector2();
    [SerializeField] Vector2 yutInsideRange = new Vector2();

    [SerializeField] float throwPower;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (throwPower < 1)
                throwPower += Time.deltaTime * 0.33f;
            else
                throwPower = 1;
        }

        if (Input.GetMouseButtonUp(0))
        {
            foreach (var yut in yuts) yut.MoveRandomPos(throwPower, transform.position, yutClampRange, yutInsideRange);
            throwPower = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, yutClampRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, yutInsideRange);
    }
}
