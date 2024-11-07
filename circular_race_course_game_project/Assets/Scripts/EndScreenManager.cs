using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    public TextMeshProUGUI coinTXT; // Reference to the TextMeshProUGUI component for displaying coin information
    
    // Start is called before the first frame update
    void Start()
    {
        coinTXT.text = "You collected " + CarController.coinCounter + " Coins."; // Set the text to show the number of coins collected
    }
}