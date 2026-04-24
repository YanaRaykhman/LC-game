using UnityEngine;
using UnityEngine.UI;

public class UpgradeRequirementUI : MonoBehaviour
{
    public Image icon;
    public Text amountText;

    public ResourceType resourceType;
    public int remainingAmount;

    public void Setup(ResourceType type, int amount)
    {
        resourceType = type;
        remainingAmount = amount;
        UpdateUI();
    }

    public void Decrease()
    {
        remainingAmount--;
        UpdateUI();
    }

    void UpdateUI()
    {
        amountText.text = "X " + remainingAmount;
    }
}