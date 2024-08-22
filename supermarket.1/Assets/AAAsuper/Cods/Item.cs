using UnityEngine;
using TMPro;
using DG.Tweening;

public class Item : MonoBehaviour
{
    public float fadeDuration = 1f; // Duration for fade effect
    public float moveUpDistance = 0.5f; // Distance to move the text up
    public float moveDuration = 0.5f; // Duration for the move
    public float displayDuration = 2f; // Duration to keep the text visible
    public float moveForwardDistance = 3f; // Distance to move the item forward
    public string itemValue; // Value to display when the item is clicked
    private Camera mainCamera; // Reference to the main camera
    public TotalGeter TotalGeter;
    void Start()
    {
        TotalGeter = FindAnyObjectByType<TotalGeter>();
        // Get the main camera
        mainCamera = Camera.main;
       

    }

    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the raycast hit this item
                if (hit.transform == transform)
                {
                    
                    ShowItemValue(hit.point);
                    MoveItemForward();
                    AddItemValueToTotal();
                    //Debug.Log("Number of item prefabs: " + numberOfPrefabs);
                    

                }
            }
        }
    }

    void ShowItemValue(Vector3 spawnPosition)
    {
        // Create a new TextMeshPro object
        GameObject textObject = new GameObject("ItemValueText");
        TextMeshPro textMesh = textObject.AddComponent<TextMeshPro>();

        // Set the text properties
        textMesh.text = "$" + itemValue;
        textMesh.fontSize = 1.5f; // Adjust font size as needed
        textMesh.color = Color.yellow; // Set text color to yellow
        textMesh.alignment = TextAlignmentOptions.Center;

        // Set the text object position at the item's position
        textObject.transform.position = spawnPosition;

        // Rotate the text to 130 degrees on the Y-axis
        textObject.transform.rotation = Quaternion.Euler(0f, 130f, 0f);

        // Animate the text
        textMesh.DOFade(1f, fadeDuration).OnStart(() =>
        {
            // Move the text up from its initial position
            textObject.transform.DOMoveY(spawnPosition.y + moveUpDistance, moveDuration).SetEase(Ease.OutQuad);
        }).OnComplete(() =>
        {
            // Keep the text visible for a specified duration
            DOVirtual.DelayedCall(displayDuration, () =>
            {
                // Fade out the text and move it back down
                textMesh.DOFade(0f, fadeDuration).OnComplete(() =>
                {
                    // Optionally, destroy the text object here
                    Destroy(textObject);
                });
                textObject.transform.DOMoveY(spawnPosition.y, moveDuration).SetEase(Ease.InQuad);
            });
        });
    }

    void MoveItemForward()
    {
        // Move the item forward
        Vector3 forwardPosition = transform.position + transform.forward * moveForwardDistance;
        transform.DOMove(forwardPosition, moveDuration).SetEase(Ease.OutQuad);
    }

    void AddItemValueToTotal()
    {
        // Convert itemValue to a float and add it to totalValue
        if (float.TryParse(itemValue, out float value))
        {
            TotalGeter.ItemTotal += value;
            TotalGeter.textupdate();
           /* Debug.Log("Total Value: $" + totalValue)*/; // Print the updated total value to the console
        }
        else
        {
            Debug.LogWarning("Invalid item value: " + itemValue);
        }
    }
}
