using DG.Tweening;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Transform cashierPoint;
    public float walkDuration = 3f;
    public float rotationDuration = 0.5f;
    public float transitionDuration = 0.3f;
    public Animator animator;

    public GameObject[] itemPrefabs;
    public Transform spawnStartPoint;
    public float spacing = 0.2f;
    public int itemsPerRow = 3;
    public float rowSpacing = 0.5f;

    public Transform handTransform; // Assign the player's hand Transform in the Inspector
    public GameObject objectToSpawn; // Assign the prefab to spawn in the player's hand
    private int totalItems;
    private int clickedItems;

    void Start()
    {

        WalkToCashier();
    }

    void WalkToCashier()
    {
        animator.Play("Walk", -1, 0f);
        transform.DOMove(cashierPoint.position, walkDuration).OnComplete(() =>
        {
            animator.CrossFade("Idle", transitionDuration);
            DOVirtual.DelayedCall(0, TurnLeft);
            SpawnItems();
        });
    }

    void TurnLeft()
    {
        transform.DORotate(new Vector3(0f, -90f, 0f), rotationDuration, RotateMode.Fast);
    }

    void SpawnItems()
    {
        Vector3 currentPosition = spawnStartPoint.position;
        int rowItemCount = 0;

        totalItems = itemPrefabs.Length;
        clickedItems = 0;

        foreach (GameObject itemPrefab in itemPrefabs)
        {
            GameObject item = Instantiate(itemPrefab, currentPosition, spawnStartPoint.rotation);


            // Ensure Init is called properly to set the reference to the Customer script
            ItemClickHandler clickHandler = item.AddComponent<ItemClickHandler>();
            clickHandler.Init(this); // Pass 'this' to reference the Customer script

            Collider itemCollider = item.GetComponent<Collider>();
            if (itemCollider != null)
            {
                float itemWidth = itemCollider.bounds.size.x;
                currentPosition += new Vector3(itemWidth + spacing, 0f, 0f);
                rowItemCount++;

                if (rowItemCount >= itemsPerRow)
                {
                    currentPosition = spawnStartPoint.position + new Vector3(0f, 0f, -rowSpacing);
                    rowItemCount = 0;
                }
            }
            else
            {
                currentPosition += new Vector3(spacing, 0f, 0f);
                rowItemCount++;

                if (rowItemCount >= itemsPerRow)
                {
                    currentPosition = spawnStartPoint.position + new Vector3(0f, 0f, -rowSpacing);
                    rowItemCount = 0;
                }
            }
        }

        Debug.Log("Items spawned on the cashier table in two rows.");
    }

    public void OnItemClicked()
    {
        clickedItems++;

        if (clickedItems >= totalItems)
        {
            // Use CrossFade to transition smoothly into the "UpHand" animation
            animator.CrossFade("UpHand", 0.08f, -1, 0f);

            // Call a method to spawn the object after the animation is complete
            DOVirtual.DelayedCall(.6f, SpawnObjectInHand);
        }
    }

    void SpawnObjectInHand()
    {
        // Create the rotation with a 90-degree rotation on the x-axis
        Quaternion handRotation = handTransform.rotation * Quaternion.Euler(90f, 0f, 0f);

        // Instantiate the object at the hand's position with the adjusted rotation
        Instantiate(objectToSpawn, handTransform.position, handRotation, handTransform);
    }

}
