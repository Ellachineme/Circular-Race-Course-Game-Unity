using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonScript : MonoBehaviour
{
    [SerializeField] private Image myIcon; //this will be a reference to the text

    public void SetIcon(Sprite mySprite) //change the text based on some logic
    {
        myIcon.sprite = mySprite;
    }
    
  
}
