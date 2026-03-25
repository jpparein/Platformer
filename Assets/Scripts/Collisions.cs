using TMPro;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    private int totalCoins = 0;
    [SerializeField] private TMP_Text txtCoin;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            totalCoins++;
            txtCoin.text = "x" + totalCoins;
        }
    }
}
