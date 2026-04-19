using UnityEngine;
using UnityEngine.UI;

public class SleepSystem : MonoBehaviour
{
    public static SleepSystem instance;
    public float sleepTimeScale = 10f;
    private float normalTimeScale = 1f;

    [Header("Sleep")]
    public float maxSleep = 100f;
    public float sleep = 100f;

    public float sleepDecayPerSecond = 1f;
    public float sleepRecoverInTent = 8f;

    public float faintRecoverSpeed = 4f;
    public float faintWakeThreshold = 60f;

    [Header("UI")]
    public Slider sleepBar;

    public bool sleeping = false;
    public bool unconscious = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        sleep = maxSleep;

        sleepBar.maxValue = maxSleep;
        sleepBar.value = sleep;
    }

    void Update()
    {
        if (sleeping)
        {
            sleep += sleepRecoverInTent * Time.deltaTime;
        }
        else if (unconscious)
        {
            sleep += faintRecoverSpeed * Time.deltaTime;

            if (sleep >= faintWakeThreshold)
            {
                WakeFromFaint();
            }
        }
        else
        {
            sleep -= sleepDecayPerSecond * Time.deltaTime;
        }

        sleep = Mathf.Clamp(sleep, 0, maxSleep);
        sleepBar.value = sleep;

        if (sleep <= 0 && !unconscious)
        {
            Faint();
        }
    }

    void Faint()
    {
        unconscious = true;
        PlayerController.instance.SetUnconscious();
    }

    void WakeFromFaint()
    {
        unconscious = false;
        PlayerController.instance.RecoverFromFaint();
    }

    public void StartSleeping()
    {
        sleeping = true;
        Time.timeScale = sleepTimeScale;
    }

    public void StopSleeping()
    {
        sleeping = false;
        Time.timeScale = normalTimeScale;
    }
}