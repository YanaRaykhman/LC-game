using UnityEngine;

public class Mountain : MonoBehaviour
{
    public int maxHP = 200;

    int hp;

    int hitCount = 0;

    public MountainHPBar hpBar;

    void Start()
    {
        hp = maxHP;

        hpBar.gameObject.SetActive(false);
    }

    public void Chop()
    {
        hp--;
        hitCount++;

        GiveResource();

        float hpPercent = (float)hp / maxHP;

        hpBar.Show(hpPercent);

        if (hp == 0)
        {
            Destroy(gameObject);
        }
    }

    void GiveResource()
    {
        float coalP = 0.2f * Mathf.Log10(hitCount);
        float crystalP = 0.000005f * hitCount * hitCount;

        float stoneP = 1f - coalP - crystalP;

        float roll = Random.value;

        if (roll < crystalP)
        {
            Inventory.instance.Add(ResourceType.Crystal, 1);
        }
        else if (roll < crystalP + coalP)
        {
            Inventory.instance.Add(ResourceType.Coal, 1);
        }
        else
        {
            Inventory.instance.Add(ResourceType.Stone, 1);
        }
    }
}