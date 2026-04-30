using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinalVictory : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;

    [SerializeField]
    private string[] messages =
    {
        "Congratulations!",
        "You completed all levels",
        "Platformer 2D - Unity Tutorial Project",
        "Created by Jean-Philippe Parein",
        "YouTube: UPLN Gaming",
        "Project available on GitHub",
        "https://github.com/jpparein",
        "Thanks for playing!"
    };

    private CanvasGroup cg;

    
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        StartCoroutine(ShowMessages());
    }

    IEnumerator ShowMessages()
    {
        for (int i = 0; i < messages.Length; i++)
        {
            messageText.text = messages[i];
            messageText.color = (i == messages.Length - 1) ? Color.yellow : Color.white;

            messageText.transform.localScale = Vector3.zero;
            cg.alpha = 0;

            cg.DOFade(1, 0.4f);
            messageText.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack);

            yield return new WaitForSeconds(2f);

            cg.DOFade(0, 0.4f);
            yield return new WaitForSeconds(0.4f);
        }

        SceneManager.LoadScene("Menu");
    }
}
