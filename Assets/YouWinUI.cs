using UnityEngine;

public class YouWinUI : MonoBehaviour
{
    public static YouWinUI instance;

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