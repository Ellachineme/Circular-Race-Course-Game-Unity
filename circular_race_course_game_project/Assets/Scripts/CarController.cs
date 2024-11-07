using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class CarController : MonoBehaviour
{
    //Variables for car properties_
    public SpriteRenderer carSprite;
    
    [SerializeField] private Rigidbody2D carRigidbody2D;
    [SerializeField] public Tilemap roadTilemap;
    [SerializeField] private Tilemap grassTilemap;
//Speed-related variables
    private float baseSpeed = (0.0125f * CarChooseManager.numberOfImageIndex) + 0.05f; 
    private float collisionSpeedMultiplier = 0.5f; // Adjust this multiplier as needed
//Input variables
    private float _verticalInput;
    private float _horizontalInput;
    public static int coinCounter;
    private ReadConfigFile readConfigFile;
    private bool fixedUpdateCalled = false;
    //Reference to RoundManager
    private RoundManager roundManager;

    float maxSpeed = (0.25f * CarChooseManager.numberOfImageIndex) + 1;
    float turnFactor= 20f;
    //float driftFactor= .95f;
    
    float rotationAngle;
    
    private void Start()
    {
        //initialise coin counter and get refrences
        coinCounter = 0;
        roundManager = FindObjectOfType<RoundManager>();
        readConfigFile = FindObjectOfType<ReadConfigFile>();
        
        Debug.Log(baseSpeed);
        //set the car sprite to the chosen car
        carSprite.sprite = CarChooseManager.chosenCar;
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    //called at a fixed time interval
    public void FixedUpdate()
    {
        //check if the car can drive and handle the input and movement
        if (CountDownManager.canDrive)
        {
            ApplyEngineForce();
            ApplySteering();
        }
    }
    
    void ApplyEngineForce()
    {
        float currentSpeed = baseSpeed;
        
        if (IsCollidingWithTilemap())
        {
            currentSpeed = baseSpeed;
        }
        else
        {
            currentSpeed = baseSpeed / 2;
        }

        if (_verticalInput == 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 2, Time.fixedDeltaTime * 3);
        }
        else
        {
            carRigidbody2D.drag = 0.1f;
        }

        // Calculate how much "forward" we are going in terms of the direction of our velocity
        float velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        // Limit so we cannot go faster than the max speed in the "forward" direction
        if (velocityVsUp > maxSpeed && _verticalInput == 0)
            return;

        // Create a force for the engine
        Vector2 engineForceVector = transform.up * currentSpeed * _verticalInput;

        // Apply force and push the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }
    
    void ApplySteering()
    {
        // Update the rotation angle based on input
        rotationAngle -= _horizontalInput * turnFactor * Time.fixedDeltaTime;

        // Apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }
    
//move the car based on user input
    public void MoveCar()
    {
        Vector2 moveDirection = new Vector2(_horizontalInput, _verticalInput).normalized;

        // Calculate the new position based on the movement direction, speed, and deltaTime
        float currentSpeed = baseSpeed;
        
        if (IsCollidingWithTilemap())
        {
            currentSpeed = baseSpeed;
        }
        else
        {
            currentSpeed = baseSpeed * collisionSpeedMultiplier;
        }
    }
    
    public void HandleInput()
    {
        //retrieve the player's position and store in these variables
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }
//Check if the car is colliding with the road tilemap
    private bool IsCollidingWithTilemap()
    {
        Vector3Int cellPosition = roadTilemap.WorldToCell(carRigidbody2D.transform.position);

        // Check if the car is colliding with the road tilemap or grass tilemap
        return roadTilemap.HasTile(cellPosition);
    }
//Called when the object enters a trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected");
//Handle different collisions based on tags
        //Switch based on the tag of the colliding object
        switch (collision.gameObject.tag)
        {
            case "Eggplant":
                Destroy(collision.gameObject);
                CoinLoss();
                baseSpeed = baseSpeed * 0.1f;
                StartCoroutine(GetBackToBaseSpeed(ReadConfigFile.speedLossTime));
                break;
            case "Coin":
                Destroy(collision.gameObject);
                CoinGain();
                break;
            case "Oil Puddle":
                Destroy(collision.gameObject);
                CoinLoss();
                baseSpeed = baseSpeed * 0.1f;
                StartCoroutine(GetBackToBaseSpeed(ReadConfigFile.speedLossTime));
                break; 
            case "StartLine":
                roundManager.RoundFinished();
                break;
        }
    }
//Coroutine to restore the base speed after a delay
    private IEnumerator GetBackToBaseSpeed(float delay)
    {
        yield return new WaitForSeconds(delay * 10);
        baseSpeed *= 10;
    }
    //public property for CarRigidbody2D
    public Rigidbody2D CarRigidbody2D // Public property
    {
        get { return carRigidbody2D; }
        set { carRigidbody2D = value; }
    }

//Decrease coin counter on coin loss
    private void CoinLoss()
    {
        coinCounter -= ReadConfigFile.coinsLoss;
        
        //Ensure the coin counter does not go below 0
        if (coinCounter < 0)
        {
            coinCounter = 0;
        }
        else
        {
            if (coinCounter < ReadConfigFile.coinsForMoreSpeed)
            {
                baseSpeed -= 0.0125f;
            }
        }
    }
//Increase coin counter on coin gain
    private void CoinGain()
    {
        coinCounter++;
        
        if (coinCounter == ReadConfigFile.coinsForMoreSpeed)
        {
            baseSpeed += 0.0125f;
        }
    }
}