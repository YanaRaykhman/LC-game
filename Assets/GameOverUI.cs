using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject panel;

    void Start()
    {
        panel.SetActive(false);
    }

    public void Show()
    {
        panel.SetActive(true);
    }
}