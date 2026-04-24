using UnityEngine;
using UnityEngine.UI;

public class UpgradeRequirementUI : MonoBehaviour
{
    public Image icon;
    public Text amountText;

    public ResourceType resourceType;
    public int remainingAmount;

    public void Setup(ResourceType type, int amount, Sprite iconSprite)
    {
        resourceType = type;
        remainingAmount = amount;

        if (icon != null)
            icon.sprite = iconSprite;

        UpdateUI();
    }

    public void Decrease()
    {
        remainingAmount--;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (amountText != null)
            amountText.text = "X " + remainingAmount;
    }
}