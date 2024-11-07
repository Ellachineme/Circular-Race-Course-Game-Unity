using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarChooseManager : MonoBehaviour
{
    public Image chosenCarIMG; // Reference to the Image component displaying the chosen car
    public Sprite[] choosableCars; // Array of sprites representing different cars that can be chosen
    public static int numberOfImageIndex; // Static variable to store the index of the currently chosen car

    public static Sprite chosenCar; // Static variable to store the sprite of the chosen car

    public TextMeshProUGUI carSpeedText; // Reference to the TextMeshProUGUI component for displaying car speed

    private void Start()
    {
        numberOfImageIndex = 0; // Initialize the index to the first car
    }

    private void Update()
    {
        chosenCarIMG.sprite = choosableCars[numberOfImageIndex]; // Update the chosen car image
        chosenCar = choosableCars[numberOfImageIndex]; // Update the static variable with the chosen car sprite

        int speedDisplay = numberOfImageIndex + 1; // Calculate the car speed display value
        carSpeedText.text = "Car Speed: " + speedDisplay; // Update the car speed display text
    }

    // Method to handle right arrow button press
    public void ArrowRight()
    {
        if (numberOfImageIndex < choosableCars.Length - 1)
        {
            numberOfImageIndex++; // Increment the index if not at the last car
        }
    } 
    
    // Method to handle left arrow button press
    public void ArrowLeft()
    {
        if (0 < numberOfImageIndex)
        {
            numberOfImageIndex--; // Decrement the index if not at the first car
        }
    } 
}