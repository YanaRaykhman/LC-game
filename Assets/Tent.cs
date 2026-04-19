using UnityEngine;

public class Tent : MonoBehaviour
{
    public Transform sleepPoint;
    public GameObject playerVisual;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void EnterTent()
    {
        player.transform.position = sleepPoint.position;

        player.GetComponent<PlayerMovement>().StopMovement();

        playerVisual.SetActive(false);

        SleepSystem.instance.StartSleeping();

        Debug.Log("Player entered tent");
    }

    public void ExitTent()
    {
        SleepSystem.instance.StopSleeping();

        playerVisual.SetActive(true);
        
        Debug.Log("Player exited tent");
    }
}