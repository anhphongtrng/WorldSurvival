using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource uiSource;
    public AudioClip audioClip;
    public AudioClip buttonClickClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip != clip)
        {
            bgmSource.clip = clip;
        }

        if (!bgmSource.isPlaying)
        {
            bgmSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlaySFXWithDuration(AudioClip clip, float duration)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
        StartCoroutine(StopAfterDelay(duration));
    }

    private IEnumerator StopAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sfxSource.Stop();
    }

    public void PlayUIClick()
    {
        uiSource.PlayOneShot(buttonClickClip);
    }


    public void StopBGM()
    {
        bgmSource.Stop();
    }
}
