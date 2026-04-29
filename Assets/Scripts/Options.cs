using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f);

        SetMusicVolume(musicSlider.value);
        SetSfxVolume(sfxSlider.value);
    }

    public void ShowOptions()
    {
        animator.SetTrigger("Show");
    }

    public void HideOptions()
    {
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
