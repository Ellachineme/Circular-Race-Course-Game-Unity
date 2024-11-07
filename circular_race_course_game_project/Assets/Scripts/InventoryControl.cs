using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryControl : MonoBehaviour
{
    private List<PlayerItem> playerInventory; // populate the list

    [SerializeField] private GameObject buttonTemplate; //template for the buttons
    [SerializeField] private GridLayoutGroup gridGroup; //GridLayoutGroup to adjust the column count using code
    [SerializeField] private Sprite[] iconSprites; //array of iconsprites
    void Start()
    {
        playerInventory = new List<PlayerItem>();
        //when we start, populate the inventory
        for (int i = 1; i <= 100; i++)
        {
            PlayerItem newItem = new PlayerItem();
            newItem.iconSprite = iconSprites[Random.Range(0, iconSprites.Length)];//it will create a 100 items
            playerInventory.Add(newItem);
        }
        GenInventory();
    }

    void GenInventory() //if it goes over a certain amount, adjust the player count
    {
        if(playerInventory.Count < 11) //we want 10 columns, if it's 10 or less
        {
            gridGroup.constraintCount = playerInventory.Count;//this is the amount of items we have
        }
        else
        {
            gridGroup.constraintCount = 10;
        }

        foreach (PlayerItem newItem in playerInventory)
        {
            GameObject newButton = Instantiate(buttonTemplate) as GameObject;
            newButton.SetActive(true);

            newButton.GetComponent<InventoryButtonScript>().SetIcon(newItem.iconSprite);
            newButton.transform.SetParent(buttonTemplate.transform.parent, false);//to make sure to spawn correctly
        }

    }

    
    public class PlayerItem //item to be displayed
    {
        public Sprite iconSprite; 
    }
}
