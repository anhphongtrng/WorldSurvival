using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    public AudioClip sceneMusicClip;

    private void Start()
    {
        AudioController.instance.PlayBGM(sceneMusicClip);
    }
}
