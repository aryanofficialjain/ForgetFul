using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource[] sfxSources;

    public void MuteMusic(bool mute)
    {
        if (musicSource != null)
            musicSource.mute = mute;
    }

    public void MuteSFX(bool mute)
    {
        foreach (var sfx in sfxSources)
        {
            if (sfx != null)
                sfx.mute = mute;
        }
    }
}