using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public GameObject normalSprite;
    public GameObject faintedSprite;

    void Awake()
    {
        instance = this;
    }

    public void SetUnconscious()
    {
        normalSprite.SetActive(false);
        faintedSprite.SetActive(true);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;

        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerMovement>().StopMovement();
    }

    public void RecoverFromFaint()
    {
        normalSprite.SetActive(true);
        faintedSprite.SetActive(false);

        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<PlayerMovement>().enabled = true;
    }
}