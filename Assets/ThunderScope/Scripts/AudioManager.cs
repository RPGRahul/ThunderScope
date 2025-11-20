using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioSource sfxOneShotSource;
    [SerializeField]
    private List<AudioClip> sfxClips;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySFX(int index)
    {
        if (sfxSource.isPlaying)
            sfxSource.Stop();

        sfxSource.clip = sfxClips[index];
        sfxSource.Play();
    }

    public void PlaySFXOneShot(int index)
    {
        sfxOneShotSource.PlayOneShot(sfxClips[index]);
    }
}
