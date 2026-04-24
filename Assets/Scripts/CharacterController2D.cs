using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private LayerMask lm;

    private Rigidbody2D rb;    
    private Vector2 moveInput;
    private Animator animator;
    private SpriteRenderer sp;
    private bool isGrounded;
    private Transform groundCheck;
    private AudioSource audioSource;

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
    }

    void FixedUpdate()
    {
        //Movemement gauche droite
        Vector2 v = rb.linearVelocity;
        v.x = moveInput.x * moveSpeed;
        rb.linearVelocity = v;
    }
}
