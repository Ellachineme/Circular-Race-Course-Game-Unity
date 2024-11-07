using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundTimeChronicManager : MonoBehaviour
{
    public TextMeshProUGUI roundTimesText; // Reference to the TextMeshProUGUI element for displaying round times

    // Start is called before the first frame update
    void Start()
    {
        DisplayRoundTimes(); // Call the method to display round times when the script starts
    }

    // Method to display round times in the assigned text box
    public void DisplayRoundTimes()
    {
        if (roundTimesText != null) // Check if the text box reference is not null
        {
            roundTimesText.text = "Round Times:\n"; // Set initial text

            // Loop through the round times and append them to the text
            for (int i = 0; i < RoundManager.roundTimes.Count; i++)
            {
                // Display the round number, elapsed time (divided by 10 for formatting), and "s" for seconds
                roundTimesText.text += "Round " + (i + 1) + ": " + (RoundManager.roundTimes[i] / 10).ToString("F2") + "s\n";
            }
        }
        else
        {
            Debug.LogError("Text box reference is null. Make sure to assign the text box reference in the inspector.");
        }
    }
}