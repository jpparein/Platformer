using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SelectLevel : MonoBehaviour
{
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnSelectLevel(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        GameObject clickedObject = pointerData.pointerClick;
        string  LevelName = "Level " + clickedObject.transform.Find("TxtNum").GetComponent<TMP_Text>().text;

        Image im = clickedObject.transform.Find("ImLocked").GetComponent<Image>();

        if (im.enabled)
        {
            audioSource.Play();
            im.transform.DOKill(true);
            im.transform.DOPunchScale(Vector3.one * 0.25f,0.25f);

        }
        else
        {
            SceneManager.LoadScene(LevelName);
        }
    }

    public void LoadMenu() => SceneManager.LoadScene("Menu");



}
