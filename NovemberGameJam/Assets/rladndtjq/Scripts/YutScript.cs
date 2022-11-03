using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YutScript : MonoBehaviour
{
    [SerializeField] Vector3 beforThrowScale = new Vector2();
    [SerializeField] Vector3 afterThrowScale = new Vector2();

    public bool isFront;
    public bool isOut;

    Vector2 beforThrowPos;

    private void Start()
    {
        beforThrowPos = transform.position;
    }

    //0~Clamp까지 랜덤한 위치에 윷을 이동시킨다
    public void MoveRandomPos(int index, float power, Vector2 center, Vector2 clamp, Vector2 inside, float animationTime)
    {
        var diff = (clamp - inside);
        var randomPos = center + (inside + (diff * (power < 0.33f ? 0 : power))) * 0.5f;
        randomPos = new Vector3(Random.Range(center.x - Mathf.Abs(center.x - randomPos.x), randomPos.x), Random.Range(center.y - Mathf.Abs(center.y - randomPos.y), randomPos.y), 0);

        if (randomPos.x < center.x - inside.x / 2 || randomPos.y < center.y - inside.y / 2)
            isOut = true;
        if (randomPos.x > center.x + inside.x / 2 || randomPos.y > center.y + inside.y / 2)
            isOut = true;

        if (index == 0)
            isFront = Random.Range(0, 99.99f) < 75 - 15 * power ? true : false;
        else
            isFront = Random.Range(0, 99.99f) < 20 + 40 * power ? true : false;
            
        var randomRotation = new Vector3(0, (isFront ? 180 : 0), Random.Range(0, 360));

        transform.DOMove(randomPos, animationTime).SetEase(Ease.OutQuad);
        transform.DOScale(afterThrowScale, animationTime).SetEase(Ease.InOutBounce);
        transform.DORotate(randomRotation, animationTime);
    }

    public void Reset()
    {
        transform.localScale = beforThrowScale;
        transform.position = beforThrowPos;
        isFront = false;
        isOut = false;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
