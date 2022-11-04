using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICoinAmount : MonoBehaviour
{
    TextMeshProUGUI text;
    public int showPlayerType;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = $"x {YutGameManager.manager.players[showPlayerType].coinAmount}";
    }
}
