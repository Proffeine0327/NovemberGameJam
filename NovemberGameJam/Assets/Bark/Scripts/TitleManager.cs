using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject TitleWindow;

    public void GameStart()
    {
        SceneManager.LoadScene("YutPlay");
        SoundManager.Instance.Effect[1].Play();
    }
    public void GameOver()
    {
        Application.Quit();
        SoundManager.Instance.Effect[1].Play();
    }
    public void OpenSetting()
    {
        TitleWindow.SetActive(true);
        SoundManager.Instance.Effect[1].Play();
    }
}
