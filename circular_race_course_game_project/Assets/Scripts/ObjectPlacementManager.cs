using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

// Class responsible for managing object placement using WebSocket communication
public class ObjectPlacementManager : MonoBehaviour
{
    private string serverUrl = "wss://softwaregrund.pro:443/jekt/ws/amount"; // WebSocket server URL

    private ClientWebSocket webSocket; // WebSocket instance for communication

    public int coinsFromServer; // Number of coins received from the server
    public int eggplantsFromServer; // Number of eggplants received from the server

    private PrefabGenerator prefabGenerator; // Reference to the PrefabGenerator class for object generation

    // Start is called before the first frame update
    private async void Start()
    {
        ServerConnect(); // Start the WebSocket connection
    }

    // Method to initiate the WebSocket connection and communicate with the server
    public async void ServerConnect()
    {
        prefabGenerator = FindObjectOfType<PrefabGenerator>(); // Find the PrefabGenerator instance

        webSocket = new ClientWebSocket(); // Create a new WebSocket instance

        try
        {
            // Connect to the WebSocket server
            await webSocket.ConnectAsync(new Uri(serverUrl), CancellationToken.None);

            // Send JSON request to the server
            string jsonRequest = CreateJsonRequest();
            byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
            await webSocket.SendAsync(new ArraySegment<byte>(requestData), WebSocketMessageType.Text, true,
                CancellationToken.None);

            // Receive and process the server's response
            string jsonResponse = await ReceiveResponse();
            ProcessResponse(jsonResponse);
        }
        catch (Exception ex)
        {
            Debug.LogError($"WebSocket connection error: {ex.Message}");
        }
        finally
        {
            // Close the WebSocket connection
            if (webSocket != null && webSocket.State == WebSocketState.Open)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "WebSocket connection closed",
                    CancellationToken.None);
            }
        }
    }

    // Method to create a JSON request for the server
    private string CreateJsonRequest()
    {
        // Create a JSON request based on the provided JSON schema
        return "{\"coins\":{\"min\":2,\"max\":4},\"eggplants\":{\"min\":1,\"max\":4}}";
    }

    // Method to receive the server's response
    private async Task<string> ReceiveResponse()
    {
        // Receive and process the server's response
        byte[] buffer = new byte[1024];
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        return Encoding.UTF8.GetString(buffer, 0, result.Count);
    }

    // Method to process the server's response
    private void ProcessResponse(string jsonResponse)
    {
        // Deserialize and process the server's response
        // Replace this with your actual JSON deserialization and processing logic
        Debug.Log($"Server Response: {jsonResponse}");

        ProcessServerResponse(jsonResponse);
    }

    // Call this function and pass the server response as a parameter
    private void ProcessServerResponse(string serverResponse)
    {
        try
        {
            // Deserialize the JSON response using a JSON library (e.g., Newtonsoft.Json)
            ServerResponseData response = JsonUtility.FromJson<ServerResponseData>(serverResponse);

            // Check if the response has a successful code (200)
            if (response.code == 200)
            {
                // Access the number of coins and eggplants from the data field
                coinsFromServer = response.data.coins;
                eggplantsFromServer = response.data.eggplants;

                // Clear existing objects with specific tags
                ClearObjectsWithTag("Coin");
                ClearObjectsWithTag("Eggplant");
                ClearObjectsWithTag("Oil Puddle");

                // Invoke the GenerateObjects method after a delay
                Invoke("GenerateObjects", 0.5f);

                // Now you can use the coins and eggplants variables as needed
                Debug.Log($"Coins: {coinsFromServer}, Eggplants: {eggplantsFromServer}");
            }
            else
            {
                Debug.LogError($"Server responded with an error code: {response.code}");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error processing server response: {ex.Message}");
        }
    }

    // Method to generate objects using the PrefabGenerator class
    private void GenerateObjects()
    {
        prefabGenerator.GenerateObjects();
    }

    // Method to clear objects with a specific tag
    private void ClearObjectsWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        if (objectsWithTag != null && objectsWithTag.Length > 0)
        {
            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }
        }
    }

    // Class to represent the structure of the server response
    [System.Serializable]
    public class ServerResponseData
    {
        public int code;
        public Data data;
    }

    // Class to represent the data field in the server response
    [System.Serializable]
    public class Data
    {
        public int coins;
        public int eggplants;
    }
}
