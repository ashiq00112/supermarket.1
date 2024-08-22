using UnityEngine;

public class CashClick: MonoBehaviour
{
    public CamerManager Manager;

    private void Start()
    {
        Manager = FindAnyObjectByType<CamerManager>();
    }
    // This method is called when the user clicks on the object
    private void OnMouseDown()
    {
        Manager.cashregister();
        // Print "Hello" when the object is clicked
     
    }
}
