using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SpawnAndMove : MonoBehaviour
{
    public GameObject[] prefabs;
    public float[] prefabValues;
    public Transform[] spawnPositions;
    public BoxCollider targetArea;
    public float moveDuration = 2f;
    public int stackThreshold = 8;
    public float stackOffset = 1f;
    private int prefabCount = 0;
    public float giving;
    public TextMeshPro givingText;
    public float change;
    private List<GameObject> spawnedPrefabs = new List<GameObject>();
    private Dictionary<GameObject, float> prefabValuesMap = new Dictionary<GameObject, float>();
    private List<Transform> originalPositions = new List<Transform>();

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
        spawnedPrefabs.Add(spawnedObject);
        originalPositions.Add(spawnTransform);
        prefabValuesMap[spawnedObject] = prefabValue;

        // Apply random rotation using DOTween
        float randomRotationZ = GetRandomRotationZ();
        spawnedObject.transform.DORotate(new Vector3(0, randomRotationZ, 0), 0.5f);

        // Calculate the random and adjusted position
        Vector3 randomPosition = GetRandomPositionInsideCollider(targetArea);
        Vector3 adjustedPosition = AdjustPositionForStacking(randomPosition);

        // Move the object upwards first, then move to the adjusted position using DOTween
        Vector3 upwardPosition = spawnedObject.transform.position + Vector3.up * .6f; // Move up by 2 units (adjust as needed)
        spawnedObject.transform.DOMove(upwardPosition, 0.5f).SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                spawnedObject.transform.DOMove(adjustedPosition, moveDuration).SetEase(Ease.OutQuad);
            });

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

        int lastIndex = spawnedPrefabs.Count - 1;
        GameObject lastSpawnedObject = spawnedPrefabs[lastIndex];
        Transform originalSpawnPosition = originalPositions[lastIndex];
        float lastPrefabValue = prefabValuesMap[lastSpawnedObject];

        lastSpawnedObject.transform.DOMove(originalSpawnPosition.transform.position, moveDuration).SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                Destroy(lastSpawnedObject);
                giving -= lastPrefabValue;
                UpdateGivingText();
            });

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
            int dollars = Mathf.FloorToInt(giving);
            int cents = Mathf.RoundToInt((giving - dollars) * 100);
            givingText.text = $"${dollars}.{cents:D2}";
            if (change == giving)
            {
                print("done");
            }
        }
    }
}