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

    public void AddItem(string itemName, int quantity, Sprite itemSprite, 
        int price, string itemDescription)
    {
        Debug.Log("itemName = " + itemName + " quantity = " + quantity + " itemSprite = " + itemSprite + 
                  " price = " + price + " itemDescription = " + itemDescription);

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite, price, itemDescription);
                return;
            }
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].isItemSelected = false;
        }
    }
}
