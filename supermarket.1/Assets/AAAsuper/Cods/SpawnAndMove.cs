using UnityEngine;
using System.Collections;

public class SpawnAndMove : MonoBehaviour
{
    public GameObject[] prefabs;         // Array of prefabs to spawn
    public float[] prefabValues;         // Array of values corresponding to each prefab
    public BoxCollider targetArea;       // The target area defined by a BoxCollider
    public float moveSpeed = 2f;         // The speed at which the prefab moves
    public int stackThreshold = 8;       // The number of prefabs before moving them upwards
    public float stackOffset = 1f;       // The vertical offset for stacking

    private int prefabCount = 0;         // Count of prefabs spawned
    public float giving;
    public void Moneymover(int prefabIndex)
    {
        if (prefabs.Length == 0 || targetArea == null)
        {
            Debug.LogError("Prefabs array is empty or TargetArea is not assigned.");
            return;
        }

        if (prefabIndex < 0 || prefabIndex >= prefabs.Length)
        {
            Debug.LogError("Prefab index is out of range.");
            return;
        }

        // Ensure prefabValues array matches the length of prefabs array
        if (prefabValues.Length != prefabs.Length)
        {
            Debug.LogError("PrefabValues array size does not match Prefabs array size.");
            return;
        }

        prefabCount++;
        GameObject selectedPrefab = prefabs[prefabIndex];
        float prefabValue = prefabValues[prefabIndex];

        GameObject spawnedObject = Instantiate(selectedPrefab, transform.position, Quaternion.identity);
        // Set the parent of the spawned prefab to the targetArea GameObject
        spawnedObject.transform.parent = targetArea.transform;

        // Apply a random rotation on the Z axis
        float randomRotationZ = GetRandomRotationZ();
        spawnedObject.transform.Rotate(0, randomRotationZ, 0);

        Vector3 randomPosition = GetRandomPositionInsideCollider(targetArea);
        Vector3 adjustedPosition = AdjustPositionForStacking(randomPosition);

        StartCoroutine(MoveToPosition(spawnedObject, adjustedPosition));

        // Print the value associated with the prefab
        Debug.Log($"Prefab Index: {prefabIndex}, Value: {prefabValue}");
        giving += prefabValue;
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
        // Return a random value of 15, -15, or 0
        return Random.value > 0.66f ? 15 : (Random.value > 0.33f ? -15 : 0);
    }

    IEnumerator MoveToPosition(GameObject obj, Vector3 target)
    {
        while (Vector3.Distance(obj.transform.position, target) > 0.01f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        obj.transform.position = target; // Ensure it ends exactly at the target position
    }
}
