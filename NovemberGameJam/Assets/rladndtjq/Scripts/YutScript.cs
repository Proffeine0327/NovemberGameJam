using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YutScript : MonoBehaviour
{
    [SerializeField] Vector2 beforThrowScale = new Vector2();
    [SerializeField] Vector2 afterThrowScale = new Vector2();

    Vector2 beforThrowPos;

    private void Start()
    {
        beforThrowPos = transform.position;
    }

    //0~Clamp까지 랜덤한 위치에 윷을 이동시킨다
    public void MoveRandomPos(float power, Vector2 center, Vector2 clamp, Vector2 inside)
    {
        StartCoroutine(MoveAnimation(power, center, clamp, inside));
    }

    IEnumerator MoveAnimation(float power, Vector2 center, Vector2 clamp, Vector2 inside)
    {
        var diff = (clamp - inside);
        var randomPos = center + (inside + diff * power) * 0.5f;
        randomPos = new Vector2(Random.Range(center.x - Mathf.Abs(center.x - randomPos.x), randomPos.x), Random.Range(center.y - Mathf.Abs(center.y - randomPos.y), randomPos.y));

        var randomRotation = new Vector3(0, 0, Random.Range(0, 360));

        transform.DOMove(randomPos, 0.8f).SetEase(Ease.OutQuad);
        transform.DOScale(afterThrowScale, 0.8f).SetEase(Ease.InOutBounce);
        transform.DORotate(randomRotation, 0.8f);

        yield return new WaitForSeconds(1.5f);

        transform.DOKill();
        transform.localScale = beforThrowScale;
        transform.position = beforThrowPos;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
