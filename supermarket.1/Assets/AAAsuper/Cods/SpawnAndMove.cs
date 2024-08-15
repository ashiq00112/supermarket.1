using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SpawnAndMove : MonoBehaviour
{
    public GameObject[] prefabs;         // Array of prefabs to spawn
    public float[] prefabValues;         // Array of values corresponding to each prefab
    public Transform[] spawnPositions;   // Array of spawn positions using Transforms
    public BoxCollider targetArea;       // The target area defined by a BoxCollider
    public float moveDuration = 2f;      // The duration for moving the prefab
    public int stackThreshold = 8;       // The number of prefabs before moving them upwards
    public float stackOffset = 1f;       // The vertical offset for stacking
    private int prefabCount = 0;         // Count of prefabs spawned
    public float giving;                 // Accumulated value of spawned prefabs

    public TextMeshPro givingText;   // Reference to the TextMeshProUGUI component
    
    private List<GameObject> spawnedPrefabs = new List<GameObject>();  // List to store spawned prefabs
    private Dictionary<GameObject, float> prefabValuesMap = new Dictionary<GameObject, float>(); // Dictionary to store prefab values
    private List<Transform> originalPositions = new List<Transform>(); // List to store the original spawn positions

    public void Moneymover(int prefabIndex)
    {
        if (prefabs.Length == 0 || targetArea == null || spawnPositions.Length == 0)
        {
            Debug.LogError("Prefabs array, TargetArea, or SpawnPositions are not assigned.");
            return;
        }
        if (prefabIndex < 0 || prefabIndex >= prefabs.Length)
        {
            Debug.LogError("Prefab index is out of range.");
            return;
        }

        if (prefabValues.Length != prefabs.Length || spawnPositions.Length != prefabs.Length)
        {
            Debug.LogError("PrefabValues or SpawnPositions array size does not match Prefabs array size.");
            return;
        }

        prefabCount++;

        GameObject selectedPrefab = prefabs[prefabIndex];
        float prefabValue = prefabValues[prefabIndex];
        Transform spawnTransform = spawnPositions[prefabIndex];
        GameObject spawnedObject = Instantiate(selectedPrefab, spawnTransform.position, Quaternion.identity);

        spawnedObject.transform.parent = targetArea.transform;
        spawnedPrefabs.Add(spawnedObject);  // Add the spawned prefab to the list
        originalPositions.Add(spawnTransform);
        prefabValuesMap[spawnedObject] = prefabValue; // Store the prefab value in the dictionary

        // Apply random rotation using DOTween
        float randomRotationZ = GetRandomRotationZ();
        spawnedObject.transform.DORotate(new Vector3(0, randomRotationZ, 0), 0.5f);

        // Calculate the random and adjusted position
        Vector3 randomPosition = GetRandomPositionInsideCollider(targetArea);
        Vector3 adjustedPosition = AdjustPositionForStacking(randomPosition);

        // Move the object to the adjusted position using DOTween
        spawnedObject.transform.DOMove(adjustedPosition, moveDuration).SetEase(Ease.OutQuad);

        // Update giving
        giving += prefabValue;
        UpdateGivingText();

        Debug.Log($"Prefab Index: {prefabIndex}, Value: {prefabValue}");
    }

    public void ReturnLastPrefabToSpawnPosition()
    {
        if (spawnedPrefabs.Count == 0)
        {
            Debug.Log("No prefabs to return.");
            return;
        }

        // Get the last spawned prefab and its value
        int lastIndex = spawnedPrefabs.Count - 1;
        GameObject lastSpawnedObject = spawnedPrefabs[lastIndex];
        Transform originalSpawnPosition = originalPositions[lastIndex];
        float lastPrefabValue = prefabValuesMap[lastSpawnedObject];

        // Move the object back to its original spawn position using DOTween
        lastSpawnedObject.transform.DOMove(originalSpawnPosition.transform.position, moveDuration).SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                Destroy(lastSpawnedObject); // Destroy the object after reaching the position
                giving -= lastPrefabValue; // Deduct the value from giving
                UpdateGivingText();
            });

        // Remove the last prefab from the lists after moving it back
        spawnedPrefabs.RemoveAt(lastIndex);
        originalPositions.RemoveAt(lastIndex);
        prefabValuesMap.Remove(lastSpawnedObject);
        prefabCount--;
    }

    Vector3 GetRandomPositionInsideCollider(BoxCollider boxCollider)
    {
        Vector3 center = boxCollider.center + boxCollider.transform.position;
        Vector3 size = boxCollider.size * 0.5f;
        return new Vector3(
            Random.Range(center.x - size.x, center.x + size.x),
            Random.Range(center.y - size.y, center.y + size.y),
            Random.Range(center.z - size.z, center.z + size.z)
        );
    }

    Vector3 AdjustPositionForStacking(Vector3 position)
    {
        if (prefabCount > stackThreshold)
        {
            position.y += stackOffset;
        }
        return position;
    }

    float GetRandomRotationZ()
    {
        return Random.value > 0.66f ? 15 : (Random.value > 0.33f ? -15 : 0);
    }

    void UpdateGivingText()
    {
        if (givingText != null)
        {
            // Convert giving to dollars and cents
            int dollars = Mathf.FloorToInt(giving);
            int cents = Mathf.RoundToInt((giving - dollars) * 100);
            givingText.text = $"${dollars}.{cents:D2}";
        }
    }
}
