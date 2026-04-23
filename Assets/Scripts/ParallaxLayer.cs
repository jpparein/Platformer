using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    private Transform cam;
    [SerializeField] private float factor = 0.5f;
    private float startX;
   
    void Start()
    {
        cam = Camera.main.transform;
        startX = transform.position.x;
    }
   
    void LateUpdate()
    {
        float camX = cam.position.x;
        transform.position = new Vector3(
            startX + camX * factor,
            transform.position.y,
            transform.position.z
        );
    }
    
}
