using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectButton : MonoBehaviour
{
    public UnityEvent OnClick;
    void OnMouseDown()
    {
        // Logic to be executed when the GameObject is clicked
        CastRay();
    }

    void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Handle the hit object
            OnClick?.Invoke();
         
        }     
    }

    public void dd()
    {
        print("work");
    }
}
