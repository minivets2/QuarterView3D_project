using UnityEngine;

public enum SoundName
{
    Theme,
    EXP,
    FireSword,
    Button,
}

[System.Serializable]
public class Sound
{
    public SoundName Name;
    public AudioClip Clip;
}