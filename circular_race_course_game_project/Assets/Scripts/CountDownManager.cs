using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownManager : MonoBehaviour
{
    public TextMeshProUGUI startSignalText; // Reference to the TextMeshProUGUI component for displaying countdown
    public static bool canDrive; // Static variable to control whether the player can drive

    // Start is called before the first frame update
    void Start()
    {
        canDrive = false; // Initialize the ability to drive as false
        StartCoroutine(StartCountdown()); // Start the countdown coroutine
    }

    // Update is called once per frame
    void Update()
    {
        // Your other update logic here
    }

    // Coroutine to handle the countdown
    IEnumerator StartCountdown()
    {
        //execution paused for 10 seconds
        yield return new WaitForSeconds(10f); // Optional delay before the countdown starts, micro timing issues with server communication

        int countdownValue = 3; // Initial countdown value

        // Loop through the countdown
        while (countdownValue > 0)
        {
            startSignalText.text = countdownValue.ToString(); // Update the countdown text
            yield return new WaitForSeconds(10f); // 10 second delay in between countdown display
            countdownValue--; // Decrease the countdown value
        }

        startSignalText.text = "GO!"; // Display "GO!" when the countdown ends
        yield return new WaitForSeconds(10f); // Optional delay after the countdown ends
        canDrive = true; // Enable the ability to drive
        startSignalText.gameObject.SetActive(false); // Optionally hide the countdown text after the countdown
    }
}