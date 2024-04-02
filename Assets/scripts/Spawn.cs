using UnityEngine;
using System.Collections.Generic;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab of the coin
    public Transform[] spawnPoints; // Array of spawn points (3D planes)

    public int numberOfCoins = 10; // Number of coins to spawn

    private List<Vector3> usedPositions = new List<Vector3>(); // List to store used spawn positions

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length); // Select random spawn point

            // Get position of spawn point
            Vector3 spawnPosition = GetUniqueSpawnPositionForCoin(spawnPointIndex);

            // Set the y-axis position to be the same for all spawned coins
            spawnPosition.y = spawnPoints[spawnPointIndex].position.y;

            Quaternion spawnRotation = Quaternion.identity; // No rotation for simplicity, you can change this if needed

            Instantiate(coinPrefab, spawnPosition, spawnRotation); // Spawn coin at position

            Debug.Log("Spawned coin at position: " + spawnPosition); // Log spawn position
        }
    }

    Vector3 GetUniqueSpawnPositionForCoin(int spawnPointIndex)
    {
        Vector3 spawnPosition;
        int maxAttempts = 10; // Maximum attempts to find a unique spawn position
        int attempts = 0;
        float minDistance = 1f; // Minimum distance between spawn positions

        // Try to find a unique spawn position
        do
        {
            spawnPosition = spawnPoints[spawnPointIndex].position + Random.insideUnitSphere * 5f; // Add some random offset

            // Check if the spawn position is already used or too close to another position
            bool positionValid = true;
            foreach (Vector3 usedPosition in usedPositions)
            {
                if (Vector3.Distance(spawnPosition, usedPosition) < minDistance)
                {
                    positionValid = false;
                    break;
                }
            }

            // If position is valid, add it to the used positions list and return
            if (positionValid)
            {
                usedPositions.Add(spawnPosition); // Add spawn position to the used positions list
                return spawnPosition; // Return the unique spawn position
            }

            attempts++;
        } while (attempts < maxAttempts);

        // If no unique position is found, just return the spawn position without checking for uniqueness
        return spawnPosition;
    }
}
