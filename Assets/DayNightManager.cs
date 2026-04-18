using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightManager : MonoBehaviour
{
    public static DayNightManager instance;

    [Header("Time")]
    public float dayDuration = 180f;
    public float nightDuration = 180f;

    float timer;

    public bool isNight = false;
    public int dayCount = 1;

    [Header("Lighting")]
    public Light2D globalLight;
    public float dayLightIntensity = 1f;
    public float nightLightIntensity = 0f;

    [Header("Campfire Light")]
    public Light2D innerFireLight;
    public Light2D outerFireLight;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartDay();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!isNight && timer >= dayDuration)
        {
            StartNight();
        }

        if (isNight && timer >= nightDuration)
        {
            StartDay();
        }
    }

    void StartDay()
    {
        timer = 0;
        isNight = false;

        globalLight.intensity = dayLightIntensity;

        innerFireLight.enabled = false;
        outerFireLight.enabled = false;

        dayCount++;

        DayNightEvents.DayStarted();
    }

    void StartNight()
    {
        timer = 0;
        isNight = true;

        globalLight.intensity = nightLightIntensity;

        innerFireLight.enabled = true;
        outerFireLight.enabled = true;

        DayNightEvents.NightStarted();
        Debug.Log("NIGHT STARTED");
    }
}