using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundManager : MonoBehaviour
{
    
    public Button aboutUsButton;

    public AudioClip clickClip;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D sound for UI
    }

    void Start()
    {
        if (aboutUsButton == null)
        {
            Debug.LogError("AboutUsButtonSound: No button assigned!");
            return;
        }

        aboutUsButton.onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        if (clickClip != null)
            audioSource.PlayOneShot(clickClip);
        else
            Debug.LogWarning("AboutUsButtonSound: No clickClip assigned!");
    }
}
