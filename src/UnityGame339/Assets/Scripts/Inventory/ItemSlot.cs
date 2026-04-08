using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    //===ITEM DATA===
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public int price;
    public bool isFull;
    
    //===ITEM SLOT===
    [SerializeField]
    private Text quantityText;
    
    [SerializeField]
    private Image itemImage;

    public void AddItem(string itemName, int quantity, Sprite itemsprite, int price)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemsprite;
        this.price = price;
        this.isFull = true;
        
        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        
        itemImage.sprite = itemsprite;
        itemImage.enabled = true;
        
        
    }
}
