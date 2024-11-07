using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class InventoryText : MonoBehaviour
{
    [SerializeField] private TMP_Text myText; // Reference to the TextMeshPro Text component on the button

    // Set the text of the button and return the TMP_Text component
    public TMP_Text SetText(string myString)
    {
        myText.text = myString; // Set the TextMeshPro component's text to the provided string
        return myText; // Return the TMP_Text component
    }
} 