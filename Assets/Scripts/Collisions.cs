using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Collisions : MonoBehaviour
{
    private int totalCoins = 0;
    [SerializeField] private TMP_Text txtCoin;
    [SerializeField] private TMP_Text txtValue;
    [SerializeField] private Image imValue;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AudioClip sfxJump, sfxHit, sfxWater, sfxWin;
    private float life = 100;

    private CharacterController2D cc;
    private Vector2 startPosition;
    private AudioSource audioSource;


    void Awake()
    {
        cc = GetComponent<CharacterController2D>();
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            totalCoins++;
            txtCoin.text = "x" + totalCoins;
            audioSource.PlayOneShot(sfxJump);
        }

        if (collision.gameObject.tag == "End")
        {
            GetComponent<PlayerInput>().enabled = false;
            particle.Play();
            audioSource.PlayOneShot(sfxWin);
            GameObject.Find("Music").GetComponent<AudioSource>().Stop();
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
        life -= value;
        life = Mathf.Clamp(life,0,100);
        txtValue.text = life + " %";
        imValue.fillAmount = life/100;
    }
}
