using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TotalGeter : MonoBehaviour
{
    public float ItemTotal;
    public TextMeshPro cashtotal;
    // Start is called before the first frame update



    public void textupdate()
    {
        cashtotal.text ="$"+ ItemTotal.ToString();
    }
}
