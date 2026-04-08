using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public ItemSlot[] itemSlot;
    
    public List<Item> items = new List<Item>(); // tracks owned item ids

    private bool isMenuActivated;
    
    void Update()
    {
        // Open Inventory with "Q"
        if (Input.GetKeyDown(KeyCode.Q) && !isMenuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            isMenuActivated = true;
        }
        
        else if (Input.GetKeyDown(KeyCode.Q) && isMenuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            isMenuActivated = false;
        }
    }

    public void AddItem(string itemName, int quantity, Sprite sprite, int price)
    {
        Debug.Log("itemName = " + itemName + " quantity = " + quantity + " itemSprite = " + sprite + " price = " + price);
        
        
    }
    
}
