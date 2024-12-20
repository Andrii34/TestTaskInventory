using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    
    public ItemType Type=>_type;
    private IItem _item;
    public IItem Item=>_item;
    public bool TryAddItem(IItem item)
    {
        if(_item == null)
        {
            _item = item;
            _item.GameObject.transform.position =transform.position;
            _item.GameObject.transform.parent = transform;
            _item.SetRigidbodyState(false);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void RemoveItem()
    {
        if (_item != null)
        {
            _item.GameObject.transform.parent = null;
            _item.SetRigidbodyState(true);
            _item = null;
        }
    }
}
