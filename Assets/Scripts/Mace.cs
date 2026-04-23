using UnityEngine;

public class Mace : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField, Range(0.5f,10f)] float distance = 2f;
    [SerializeField] Sprite backface;
    private Sprite frontFace;
    private SpriteRenderer sr;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool goingDown = true;

    void Awake()
    {
        startPosition = transform.position;
        endPosition = startPosition + Vector2.down * distance;
        sr = GetComponent<SpriteRenderer>();
        frontFace = sr.sprite;
    }

    
    void Update()
    {
        Vector2 target = goingDown ? endPosition : startPosition;

        transform.position = Vector2.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, target) < 0.01f)
        {
            goingDown = !goingDown;
            sr.sprite = goingDown ? frontFace : backface;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.down * distance;
        Gizmos.DrawLine(start,end);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player") goingDown = !goingDown;
    }
}
