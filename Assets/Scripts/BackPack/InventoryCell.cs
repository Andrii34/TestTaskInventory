using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryCell : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    private BackPack _backPack;
    public ItemType Type=>_type;
    private IItem _item;
    public IItem Item=>_item;
    public void Init(BackPack backPack)
    {
        _backPack = backPack;
    }
    public bool TryAddItem(IItem item)
    {
        if(_item == null)
        {
            _item = item;
            _item.GameObject.transform.position =transform.position;
            _item.GameObject.transform.parent = transform;
            _item.GameObject.transform.rotation= transform.rotation;
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
    private void OnTriggerEnter(Collider other)
    {
        IItem item = other.GetComponent<IItem>();
        if (item != null)
        {
            _backPack.AddItem(item);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IItem item = other.GetComponent<IItem>();
        if (item != null&&item==_item)
        {
            if (item.IsSelected)
            {

            _backPack.RemoveItem(_item);
            }
        }
    }
}
