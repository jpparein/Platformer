using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField, Range(0.5f, 10f)] float distance = 2f;
   
    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool goingDown = true;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        endPosition = startPosition + Vector2.up * distance;       
    }


    void FixedUpdate()
    {
        Vector2 target = goingDown ? endPosition : startPosition;

        Vector2 nextPosition = Vector2.MoveTowards(
            transform.position,
            target,
            speed * Time.fixedDeltaTime
        );

        rb.MovePosition( nextPosition );

        if (Vector2.Distance(transform.position, target) < 0.01f)
        {
            goingDown = !goingDown;           
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.up * distance;
        Gizmos.DrawLine(start, end);
    }    
}
