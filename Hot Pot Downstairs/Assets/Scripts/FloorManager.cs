using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] GameObject[] floorPrefabs;
    public void SpawnFloor()
    {
        int num = Random.Range(0, floorPrefabs.Length);
        GameObject floor = Instantiate(floorPrefabs[num], transform); // Instantiate a random floor prefab at the position of this manager
        floor.transform.position = new Vector3(Random.Range(-4f, 4f), -6f, 0f); // Set the position of the new floor
    }
}
