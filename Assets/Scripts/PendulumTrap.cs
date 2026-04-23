using Unity.VisualScripting;
using UnityEngine;

public class PendulumTrap : MonoBehaviour
{
    [SerializeField] private float maxAngle = 45f;
    [SerializeField] private float speed = 2f;

    private Quaternion startRotation;

    void Awake()
    {
        startRotation = transform.rotation;
    }

   
    void Update()
    {
        float time = Time.time  *speed;
        float sinValue = Mathf.Sin(time);
        float angle = sinValue * maxAngle;
        transform.rotation = startRotation * Quaternion.Euler(0f,0f,angle);
    }
}
