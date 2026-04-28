using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class SelectLevel : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private Transform panelLevels;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UnlockLevelAtStart();
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


    void UnlockLevelAtStart()
    {
        //PlayerPrefs.DeleteAll();
        int unlockLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        foreach (Transform levelButton in panelLevels)
        {
            TMP_Text txtNum = levelButton.Find("TxtNum").GetComponent<TMP_Text>();
            Image imLocked = levelButton.Find("ImLocked").GetComponent<Image>();
            int levelNumber = int.Parse(txtNum.text);
            imLocked.enabled = levelNumber > unlockLevel;
        }
    }
}
