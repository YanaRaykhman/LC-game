using UnityEngine;

public class Deer : MonoBehaviour
{
    public float huntChance = 0.5f;

    public int fleshAmount = 1;

    void OnMouseDown()
    {
        Hunt();
    }

    public void Hunt()
    {
        float roll = Random.value;

        if (roll <= huntChance)
        {
            Inventory.instance.Add(ResourceType.Flesh, fleshAmount);
        }

        Destroy(gameObject);
    }
}