using UnityEngine;
using UnityEngine.UI;

// Attach to any Canvas — auto-wires click sound to every Button in the scene.
public class ButtonClickSFX : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.ignoreListenerPause = true; // plays even when game is paused

        foreach (var btn in FindObjectsOfType<Button>(true))
            btn.onClick.AddListener(PlayClick);
    }

    void PlayClick()
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }
}
