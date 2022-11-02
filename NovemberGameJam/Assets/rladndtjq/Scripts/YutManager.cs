using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThrowResult
{
    BackDo,
    Do,
    Gae,
    Girl,
    Yut,
    Mo,
    Fail,
}

public class YutManager : MonoBehaviour
{
    public List<YutScript> yuts = new List<YutScript>();

    public Vector2 yutClampRange = new Vector2();
    public Vector2 yutInsideRange = new Vector2();

    public bool isThrowing;

    private void Update()
    {

    }

    public ThrowResult ThrowYutsNGetResult(float power)
    {
        for(int i = 0; i < yuts.Count; i++)
        {
            yuts[i].MoveRandomPos(i,power,transform.position, yutClampRange, yutInsideRange, 0.8f);
        }

        bool isBackDo = true;
        for (int i = 0; i < yuts.Count; i++)
        {
            if (yuts[i].isOut)
            {
                return ThrowResult.Fail;
            }

            if (yuts[0].isFront)
            {
                if (i > 0)
                    if (yuts[i].isFront)
                        isBackDo = false;
            }
            else
            {
                isBackDo = false;
            }
        }

        if (isBackDo)
        {
            return ThrowResult.BackDo;
        }

        int successCount = 0;
        foreach (var yut in yuts)
            if (yut.isFront)
                successCount++;

        switch (successCount)
        {
            case 0:
                return ThrowResult.Mo;
            case 1:
                return ThrowResult.Do;
            case 2:
                return ThrowResult.Gae;
            case 3:
                return ThrowResult.Girl;
            case 4:
                return ThrowResult.Yut;
        }

        return ThrowResult.Mo;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, yutClampRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, yutInsideRange);
    }
}
