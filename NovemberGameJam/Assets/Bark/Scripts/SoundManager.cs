using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource BGM;
    public List<AudioSource> Effect = new List<AudioSource>();

    private void Awake() 
    {
        if(Instance == null)
            Instance = this;

        DontDestroyOnLoad(Instance);
    }
}