using UnityEngine;

public class Wolf : MonoBehaviour
{
    public float speed = 3f;

    private Transform player;
    private Transform campfire;

    private float safeRadius;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        campfire = Campfire.instance.transform;

        safeRadius = Campfire.instance.safeRadius;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 nextPos = (Vector2)transform.position + direction * speed * Time.fixedDeltaTime;

        float distToFire = Vector2.Distance(nextPos, campfire.position);

        if (distToFire > safeRadius)
        {
            transform.position = nextPos;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameOver();
        }
    }
    void GameOver()
    {
        GameOverUI.instance.Show();
        Time.timeScale = 0;
    }
}