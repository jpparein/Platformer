using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] GameObject leverRight, leverLeft;
    [SerializeField] Bridge bridge;
    private AudioSource audioLever;
    private bool activated = false;

    private void Awake()
    {
        audioLever = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated) return;

        activated = true;
        leverRight.SetActive(true);
        leverLeft.SetActive(false);
        audioLever.Play();
        bridge.enabled = true;
    }
}
