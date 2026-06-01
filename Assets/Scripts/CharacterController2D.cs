using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;

    [Header("Ground Detection")]
    [SerializeField] private LayerMask lm;

    [Header("Foot Dust")]
    [SerializeField] private SpriteRenderer dustRenderer;
    [SerializeField] private Sprite[] dustFrames;
    [SerializeField] private float dustFrameTime = 0.06f;
    [SerializeField] private float dustOffsetX = 0.5f;
    [SerializeField] private float dustGroundCheckRadius = 0.2f;
    [SerializeField] private float dustStartScale = 0f;
    [SerializeField] private float dustEndScale = 1f;
    [SerializeField] private float dustScaleSpeed = 1f;

    private Rigidbody2D rb;    
    private Vector2 moveInput;
    private Animator animator;
    private SpriteRenderer sp;
    private bool isGrounded;
    private Transform groundCheck;
    private AudioSource audioSource;
    private int dustFrameIndex;
    private float dustTimer;
    private float currentDustScale;

    void Awake()
    {
        //Assignation des variables
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        groundCheck = transform.Find("GroundCheck"); 
        audioSource = GetComponent<AudioSource>();
    }

    void OnMove(InputValue value) => moveInput = value.Get<Vector2>();   

    void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            rb.linearVelocityY = jumpForce;
            audioSource.Play();
        }
           
    }
    
    void Update()
    {
        //Animations marche
        animator.SetFloat("val_x", Mathf.Abs(moveInput.x));
        if (moveInput.x < 0) sp.flipX = true;
        if (moveInput.x > 0) sp.flipX = false;

        //Personnage au sol ?
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, lm);

        //animation de saut et de fall down
        animator.SetFloat("vel_y", rb.linearVelocityY);
        animator.SetBool("isGrounded", isGrounded);

        UpdateFootDust();
    }

    void FixedUpdate()
    {
        //Movemement gauche droite
        Vector2 v = rb.linearVelocity;
        v.x = moveInput.x * moveSpeed;
        rb.linearVelocity = v;
    }

    void UpdateFootDust()
    {
        bool isRunning = Mathf.Abs(moveInput.x) > 0.1f;
        if(!isGrounded || !isRunning)
        {
            dustRenderer.enabled = false;
            dustFrameIndex = 0;
            dustTimer = 0f;
            currentDustScale = dustStartScale;
            return;
        }

        float direction = Mathf.Sign(moveInput.x);
        Vector3 dustPosition = dustRenderer.transform.localPosition;
        dustPosition.x = -direction * dustOffsetX;
        dustRenderer.transform.localPosition = dustPosition;
        dustRenderer.flipX = direction < 0f;

        if(!Physics2D.OverlapCircle(dustRenderer.transform.position,dustGroundCheckRadius,lm))
        {
            dustRenderer.enabled=false;
            return;
        }

        currentDustScale = Mathf.MoveTowards(currentDustScale, dustEndScale, dustScaleSpeed * Time.deltaTime);
        SetDustAppearance(currentDustScale);

        dustRenderer.enabled = true;

        dustTimer -= Time.deltaTime;

        if(dustTimer <= 0f)
        {
            dustRenderer.sprite = dustFrames[dustFrameIndex];
            dustFrameIndex = (dustFrameIndex + 1) % dustFrames.Length;
            dustTimer = dustFrameTime;
        }
    }

    void SetDustAppearance(float scale)
    {
        dustRenderer.transform.localScale = new Vector3(scale,scale,1f);
        Color color = dustRenderer.color;
        color.a = Mathf.InverseLerp(dustStartScale, dustEndScale, scale);
        dustRenderer.color = color;
    }
}
