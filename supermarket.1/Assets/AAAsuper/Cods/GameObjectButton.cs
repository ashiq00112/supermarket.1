using UnityEngine;
using UnityEngine.Events;

public class GameObjectButton : MonoBehaviour
{
    public UnityEvent OnClick;
    public GameObject outline;
    private bool isHolding = false;

    void Update()
    {
        if (isHolding)
        {
            PrintHello(); // Continuously print "Hello" while holding
        }
    }

    void OnMouseDown()
    {
        isHolding = true; // Start holding
    }

    void OnMouseUp()
    {
        isHolding = false; // Stop holding
        CastRay(); // Invoke the raycast event on release
    }

    void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(outline != null)
            {
                outline.SetActive(false);

            }
            OnClick?.Invoke(); // Invoke the event if a hit is detected
        }
    }

    public void PrintHello()
    {
        
        if (outline != null)
        {
            outline.SetActive(true);

        }
    }
}
