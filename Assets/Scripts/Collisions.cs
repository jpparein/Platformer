using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditorInternal;
using DG.Tweening;

public class Collisions : MonoBehaviour
{

    [Header("UI - Coin Counter")]
    [SerializeField] private TMP_Text txtCoin;

    [Header("UI - Health Bar")]
    [SerializeField] private TMP_Text txtValue;
    [SerializeField] private Image imValue;

    [Header("Visual Effects")]
    [SerializeField] private ParticleSystem particle;

    [Header("Audio - Sound Effects")]
    [SerializeField] private AudioClip sfxCoin;
    [SerializeField] private AudioClip sfxHit;
    [SerializeField] private AudioClip sfxWater;
    [SerializeField] private AudioClip sfxWin;
    [SerializeField] private AudioClip sfxGameOver;

    [Header("UI Panels")]
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private GameObject panelLevelComplete;

    private int totalCoins = 0;
    private float life = 100;
    private CharacterController2D cc;
    private Vector2 startPosition;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        cc = GetComponent<CharacterController2D>();
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            totalCoins++;
            txtCoin.text = "x" + totalCoins;
            audioSource.PlayOneShot(sfxCoin);

            txtCoin.transform.DOScale(txtCoin.transform.localScale * 1.5f, 0.1f)
                .SetLoops(2, LoopType.Yoyo);
        }

        if (collision.gameObject.tag == "End")
        {
            GetComponent<PlayerInput>().enabled = false;
            particle.Play();
            audioSource.PlayOneShot(sfxWin);
            GameObject.Find("Music").GetComponent<AudioSource>().Stop();
            panelLevelComplete.SetActive(true);
            OnLevelComplete();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Water")
        {
            ApplyHurt(25);
            transform.position = startPosition;
            audioSource.PlayOneShot(sfxWater);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mace")
        {
            ApplyHurt(20);
            ApplyImpact(collision);
           
        }

        if (collision.gameObject.tag == "Spike")
        {
            ApplyHurt(10);
            ApplyImpact(collision);
        }

        if (collision.gameObject.tag == "Saw")
        {
            ApplyHurt(15);
            ApplyImpact(collision);
        }
    }

    void ApplyImpact(Collision2D collision)
    {
        audioSource.PlayOneShot(sfxHit);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = ((Vector2)transform.position - (Vector2)collision.transform.position).normalized;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * 20f, ForceMode2D.Impulse);
        cc.enabled = false;
        Invoke(nameof(ReenableController), 0.15f);
    }

    void ReenableController()
    {
        cc.enabled = true;
    }

    void ApplyHurt(float value)
    {
        spriteRenderer.DOKill();
        spriteRenderer.DOFade(0.2f, 0.05f).
            SetLoops(8, LoopType.Yoyo)
            .OnComplete(() => spriteRenderer.color = Color.white);

        life -= value;
        life = Mathf.Clamp(life,0,100);
        txtValue.text = life + " %";
        //imValue.fillAmount = life/100;
        imValue.DOFillAmount(life / 100, 0.25f);

        if (life == 0)
        {
            panelGameOver.SetActive(true);
            GetComponent<PlayerInput>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            audioSource.PlayOneShot(sfxGameOver);
            GameObject.Find("Music").GetComponent<AudioSource>().Stop();
            StartCoroutine(LoadSelectLevel());
        }
    }

    IEnumerator LoadSelectLevel(bool victory = false)
    {
        yield return new WaitForSeconds(4f);

        if(victory) 
            SceneManager.LoadScene("Victory");
        else
            SceneManager.LoadScene("SelectLevel");
    }

    void OnLevelComplete()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string levelNumberText = sceneName.Replace("Level ", "");
        int currentLevel = int.Parse(levelNumberText);
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1);
            PlayerPrefs.Save();
        }

        if(currentLevel==10)
            StartCoroutine(LoadSelectLevel(true));
        else
            StartCoroutine(LoadSelectLevel(false));
    }

}
