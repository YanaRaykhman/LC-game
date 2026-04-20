using UnityEngine;
using UnityEngine.UI;

public class Campfire : MonoBehaviour
{
    public static Campfire instance;

    [Header("Heat")]
    public float maxHeat = 100f;
    public float heat = 100f;
    public float heatDecayPerSecond = 2f;
    public float heatPerWood = 20f;
    public float safeRadius = 5f;

    [Header("Interaction")]
    public float interactionDistance = 2f;

    [Header("UI")]
    public Slider heatSlider;

    Transform player;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        heat = maxHeat;

        heatSlider.maxValue = maxHeat;
        heatSlider.value = heat;
    }

    void Update()
    {
        heat -= heatDecayPerSecond * Time.deltaTime;
        heat = Mathf.Clamp(heat, 0, maxHeat);

        heatSlider.value = heat;

        if (heat <= 0)
        {
            GameOver();
        }
    }

    public bool PlayerInRange()
    {
        float dist = Vector2.Distance(player.position, transform.position);
        return dist <= interactionDistance;
    }

    public void AddWood()
    {
        heat += heatPerWood;
        heat = Mathf.Clamp(heat, 0, maxHeat);

        heatSlider.value = heat;
    }

    public void CookMeat()
    {
        Inventory.instance.Add(ResourceType.Meat, 1);
    }

    void GameOver()
    {
        Destroy(gameObject);

        GameOverUI.instance.Show();

        Time.timeScale = 0;
    }
}