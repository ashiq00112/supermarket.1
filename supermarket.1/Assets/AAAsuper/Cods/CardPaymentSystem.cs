using UnityEngine;
using TMPro;

public class CardPaymentSystem : MonoBehaviour
{
    public float totalAmount = 0f;
    public TextMeshPro totalAmountText;

    public void AddValue(string value)
    {
        if (totalAmountText != null)
        {
            string currentText = totalAmountText.text;

            // Prevent entering a decimal point as the first character
            if (value == "." && currentText.Length == 0)
            {
                return; // Do nothing if trying to enter a full stop as the first value
            }

            // Limit the total number of characters to 10
            if (currentText.Length >= 10)
            {
                return; // Do nothing if the length exceeds 10 characters
            }

            // Check if a decimal point already exists
            if (currentText.Contains("."))
            {
                int decimalIndex = currentText.IndexOf(".");
                // Check if there are already two numbers after the decimal point
                if (currentText.Length - decimalIndex > 2)
                {
                    return; // Do nothing if there are already two digits after the decimal point
                }
            }

            totalAmountText.text += value;
        }
    }

    public void RemoveLastValue()
    {
        if (totalAmountText != null && totalAmountText.text.Length > 0)
        {
            totalAmountText.text = totalAmountText.text.Substring(0, totalAmountText.text.Length - 1);
        }
    }

    void UpdateTotalAmountText()
    {
        if (totalAmountText != null)
        {
            totalAmountText.text = totalAmount.ToString();
        }
    }
}
