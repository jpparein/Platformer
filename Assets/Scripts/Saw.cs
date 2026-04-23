using UnityEngine;

public class Saw : MonoBehaviour
{
    
    void Update()
    {
        transform.Rotate(Vector3.forward * 25f * Time.deltaTime);
    }
}
