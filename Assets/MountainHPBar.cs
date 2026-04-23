using UnityEngine;
using UnityEngine.UI;

public class MountainHPBar : MonoBehaviour
{
    public Image fill;

    float timer;

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void Show(float value)
    {
        fill.fillAmount = value;

        gameObject.SetActive(true);

        timer = 1f;
    }
}