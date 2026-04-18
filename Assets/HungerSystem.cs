using UnityEngine;
using UnityEngine.UI;

public class HungerSystem : MonoBehaviour
{
    public static HungerSystem instance;

    [Header("Hunger")]
    public float maxHunger = 100f;
    public float hunger = 100f;
    public float hungerDecayPerSecond = 1f;

    [Header("Food values")]
    public float appleFood = 15f;
    public float berryFood = 10f;
    public float meatFood = 40f;

    [Header("UI")]
    public Slider hungerSlider;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        hunger = maxHunger;

        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = hunger;
    }

    void Update()
    {
        hunger -= hungerDecayPerSecond * Time.deltaTime;
        hunger = Mathf.Clamp(hunger, 0, maxHunger);

        hungerSlider.value = hunger;

        if (hunger <= 0)
        {
            GameOver();
        }
    }

    public void Eat(ResourceType foodType)
    {
        float value = 0;

        switch (foodType)
        {
            case ResourceType.Apple:
                value = appleFood;
                break;

            case ResourceType.Berry:
                value = berryFood;
                break;

            case ResourceType.Meat:
                value = meatFood;
                break;
        }

        hunger += value;
        hunger = Mathf.Clamp(hunger, 0, maxHunger);

        hungerSlider.value = hunger;
    }

    void GameOver()
    {
        GameOverUI.instance.Show();
        Time.timeScale = 0;
    }
}