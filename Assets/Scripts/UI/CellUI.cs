using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
     private Color _withItemColor = Color.green;
     private Color _withoutItemColor;
    [SerializeField] private Image _icon;
    [SerializeField] private ItemType _type;
    private IItem _item;
    private bool isMouseOver = false; 
    private bool isMouseHeld = false;
    public static event Action<IItem> OnRemoved;
    public ItemType Type=>_type;
    
    private void Start()
    {
        
        
        _withoutItemColor = Color.green;
        _withoutItemColor.a = 0.5f;
        _icon.color = _withoutItemColor;
       
        
    }

    public void AdItem(IItem item)
    {
        _item = item;
        _icon.color = _withItemColor;
    }
    public void RemoveItem()
    {
        _icon.color =_withoutItemColor;
        OnRemoved?.Invoke(_item);
        _item = null;

    }
   

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false; 
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) 
        {
            isMouseHeld = true;
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            isMouseHeld = false;
        }

        
        if (isMouseOver && isMouseHeld)
        {
            RemoveItem();

        }
    }


}
