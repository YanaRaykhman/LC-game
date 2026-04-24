using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CampfireLevelData
{
    public CampfireLevel level;
    public Sprite sprite;

    public int requiredWood;
    public int requiredStone;
    public int requiredCrystal;
    public int requiredCoal;

    public float maxHeat;
    public float safeZoneRadius;
}

public class Campfire : MonoBehaviour
{
    public static Campfire instance;

    public CampfireLevel currentLevel = CampfireLevel.SmallFire;

    public CampfireLevelData[] levels;

    public SpriteRenderer spriteRenderer;

    float holdTime = 0f;
    float requiredHold = 1f;

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
        ApplyLevel();
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

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject && PlayerInRange())
            {
                holdTime += Time.deltaTime;

                if (holdTime >= requiredHold)
                {
                    CampfireUpgradeUI.instance.Open();
                    holdTime = 0f;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            holdTime = 0f;
        }

        
    }

     public CampfireLevelData GetNextLevelData()
    {
        int nextIndex = (int)currentLevel + 1;

        if (nextIndex >= levels.Length)
            return null;

        return levels[nextIndex];
    }

    public void Upgrade()
    {
        currentLevel++;

        ApplyLevel();

        if (currentLevel == CampfireLevel.Beacon)
        {
            YouWin();
        }
    }

    void ApplyLevel()
    {
        CampfireLevelData data = levels[(int)currentLevel];

        spriteRenderer.sprite = data.sprite;

        maxHeat = data.maxHeat;
        heat = Mathf.Clamp(heat, 0, maxHeat);

        heatSlider.maxValue = maxHeat;

        if (data.safeZoneRadius > 0)
        {
            safeRadius = data.safeZoneRadius;
        }
    }

    public bool PlayerInRange()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");

            if (p == null)
                return false;

            player = p.transform;
        }

        float dist = Vector2.Distance(player.position, transform.position);
        return dist <= interactionDistance;
    }

    public void AddWood()
    {
        heat += heatPerWood;
        heat = Mathf.Clamp(heat, 0, maxHeat);

        heatSlider.value = heat;
        Debug.Log("Wood added");
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
        void YouWin()
    {
        YouWinUI.instance.Show();
        Time.timeScale = 0;
    }
}