using UnityEngine;

public class ItemClickHandler : MonoBehaviour
{
    private Customer customer; // Reference to the Customer script

    public void Init(Customer customerScript)
    {
        // Set the reference to the Customer script when Init is called
        customer = customerScript;
    }

    private void OnMouseDown()
    {
        // Check if customer is not null
        if (customer != null)
        {
            Debug.Log(gameObject.name + " clicked!");
            customer.OnItemClicked();
        }
        else
        {
          
        }
    }
}
