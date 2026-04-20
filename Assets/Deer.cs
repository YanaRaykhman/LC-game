using UnityEngine;

public class Deer : MonoBehaviour
{
    public float huntChance = 0.5f;

    public int fleshAmount = 1;

    public GameObject deerSprite;

    bool active = true;

    void OnMouseDown()
    {
        Hunt();
    }

    public void Hunt()
    {
        if (!active)
            return;

        float roll = Random.value;

        if (roll <= huntChance)
        {
            Inventory.instance.Add(ResourceType.Flesh, fleshAmount);
        }

        Destroy(gameObject);
    }

    void OnEnable()
    {
        DayNightEvents.OnNightStart += HideDeer;
        DayNightEvents.OnDayStart += ShowDeer;
    }

    void OnDisable()
    {
        DayNightEvents.OnNightStart -= HideDeer;
        DayNightEvents.OnDayStart -= ShowDeer;
    }

    void HideDeer()
    {
        deerSprite.SetActive(false);
        active = false;
    }

    void ShowDeer()
    {
        deerSprite.SetActive(true);
        active = true;
    }

}