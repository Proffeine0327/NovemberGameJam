using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class ExplainScript : MonoBehaviour
{
    UnityAction action;

    [TextArea(3,50)]
    public string explains;

    TextMeshProUGUI tmpro;
    Button closeBtn;

    private void Awake() 
    {
        closeBtn = GetComponentInChildren<Button>();
        tmpro = GetComponentInChildren<TextMeshProUGUI>();
        
        tmpro.text = explains;
    }


    /// <summary>
    /// 설명창 보여주기
    /// </summary>
    /// <param name="startPos">시작점</param>
    /// <param name="gotoPos">종료점</param>
    /// <param name="action">종료 버튼 눌렀을때 실행할 함수</param>
    public void showExplain(Vector2 startPos, Vector2 gotoPos, UnityAction action)
    {
        gameObject.GetComponent<RectTransform>().DOAnchorPos(gotoPos, 1.5f).SetEase(Ease.OutBack);

        this.action = action;
        this.closeBtn.onClick.AddListener(() => {
            gameObject.GetComponent<RectTransform>().DOAnchorPos(startPos, 1.5f).SetEase(Ease.InBack);
        });
        this.closeBtn.onClick.AddListener(action);
    }
}
