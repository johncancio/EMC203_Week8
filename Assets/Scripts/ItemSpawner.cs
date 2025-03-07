using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Item itemToSpawn; 
    public float spawnRate = 1f; 
    public float speed = 5f; 

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 0f, spawnRate);
    }

    private void SpawnObstacle()
    {
        Item spawnedItem = Instantiate(itemToSpawn);
        spawnedItem.itemPosition = new Vector3(0, 0, 0); 
        
        spawnedItem.speed = speed;
    }
}