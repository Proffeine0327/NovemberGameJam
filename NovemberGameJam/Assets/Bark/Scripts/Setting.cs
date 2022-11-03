using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Setting : MonoBehaviour
{
    [SerializeField] Slider BackgroundSlider, EffectSlider;
    [SerializeField] Image BackgroundImage, EffectImage;
    [SerializeField] Sprite[] sprites;
    private bool isBGmute, isEFmute;
    private void Start()
    {
        BackgroundSlider.onValueChanged.AddListener(BackgroundValue);
        EffectSlider.onValueChanged.AddListener(EffectValue);
        isBGmute = SoundManager.Instance.BGM.mute;
        isEFmute = SoundManager.Instance.Effect[0].mute;
    }
    public void BackgroundValue(float value)
    {
        SoundManager.Instance.BGM.volume = value;
    }
    public void EffectValue(float value)
    {
        for (int i = 0; i < SoundManager.Instance.Effect.Count; i++)
        {
            SoundManager.Instance.Effect[i].volume = value;
        }
    }
    public void SettingWindowOff()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        SoundManager.Instance.Effect[1].Play();
    }
    public void BackgroundMute()
    {
        if (isBGmute)
        {
            isBGmute = false;
            SoundManager.Instance.BGM.mute = false;
            BackgroundImage.sprite = sprites[0];
        }
        else
        {
            isBGmute = true;
            SoundManager.Instance.BGM.mute = true;
            BackgroundImage.sprite = sprites[1];
        }
        SoundManager.Instance.Effect[1].Play();
    }
    public void EffectMute()
    {
        if (isEFmute)
        {
            isEFmute = false;
            for (int i = 0; i < SoundManager.Instance.Effect.Count; i++)
            {
                SoundManager.Instance.Effect[i].mute = false;
                EffectImage.sprite = sprites[0];
            }
        }
        else
        {
            isEFmute = true;
            for (int i = 0; i < SoundManager.Instance.Effect.Count; i++)
            {
                SoundManager.Instance.Effect[i].mute = true;
                EffectImage.sprite = sprites[1];
            }
        }
        SoundManager.Instance.Effect[1].Play();
    }
}
