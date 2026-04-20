using UnityEngine;
using System;

public class WolfSpawner : MonoBehaviour
{
    public GameObject wolfPrefab;

    public float spawnRadius = 15f;

    private Transform campfire;

    void Start()
    {
        campfire = Campfire.instance.transform;
    }

    Vector2 RandomSpawnPosition()
    {
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);

        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        return (Vector2)campfire.position + dir * spawnRadius;
    }

    public void SpawnWolves(int day)
    {
        int wolves = Mathf.FloorToInt((float)(Math.Log(day, 1.4) + 2));

        for (int i = 0; i < wolves; i++)
        {
            Vector2 pos = RandomSpawnPosition();

            Instantiate(wolfPrefab, pos, Quaternion.identity);
        }

        Debug.Log("Spawned wolves: " + wolves);
    }


    public void RemoveAllWolves()
    {
        Wolf[] wolves = FindObjectsOfType<Wolf>();

        foreach (Wolf wolf in wolves)
        {
            Destroy(wolf.gameObject);
        }
    }
}