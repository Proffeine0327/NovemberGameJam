using System.Collections.Generic;
using UnityEngine;
public class SoundManager : Singleton<SoundManager>
{
    public AudioSource BGM;
    public List<AudioSource> Effect = new List<AudioSource>();
}