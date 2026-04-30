using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private bool useEscapePause;

    private Animator animator;
    private bool isOpen;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f);
        audioSource = GetComponentInChildren<AudioSource>();

        SetMusicVolume(musicSlider.value);
        SetSfxVolume(sfxSlider.value);
    }

    private void Update()
    {
        if (!useEscapePause) return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isOpen)
                HideOptions();
            else
                ShowOptions();

            audioSource.Play();
        }
    }

    public void ShowOptions()
    {
        isOpen = true;
        if (useEscapePause)
            Time.timeScale = 0f;
        animator.SetTrigger("Show");
    }

    public void HideOptions()
    {
        isOpen = false;
        if (useEscapePause)
            Time.timeScale = 1f;
        animator.SetTrigger("Hide");
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSfxVolume(float value)
    {
        audioMixer.SetFloat("SfxVolume", Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat("SfxVolume", value);
    }

    public void MuteSound()
    {
        musicSlider.value = 0.0001f;
        sfxSlider.value = 0.0001f;
    }



}
