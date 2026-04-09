using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //===ITEM DATA===
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public int price;
    public string itemDescription;
    public bool isFull;
    public Sprite emptySprite;
    
    //===ITEM SLOT===
    [SerializeField]
    private Text quantityText;
    
    [SerializeField]
    private Image itemImage;
    
    //===ITEM DESCRIPTION SLOT===
    public Image itemDescriptionImage;
    public Text itemDescriptionText;
    public Text itemDescriptionNameText;
    
    //===OTHER===
    private InventoryManager inventoryManager;
    public GameObject selectedShader;
    public bool isItemSelected;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    
    public void AddItem(string itemName, int quantity, Sprite itemsprite, 
        int price, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemsprite;
        this.price = price;
        this.itemDescription = itemDescription;
        this.isFull = true;
        
        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        
        itemImage.sprite = itemsprite;
        itemImage.enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }                                                            
    }

    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        isItemSelected = true;
        
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;

        if (itemDescriptionImage.sprite == null)
        {
            itemDescriptionImage.sprite = emptySprite;
        }
    }

    public void OnRightClick()
    {
        
    }
}
