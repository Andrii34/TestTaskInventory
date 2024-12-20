using UnityEngine;
using UnityEngine.Events;
using Zenject;
using static UnityEditor.Progress;

public class BackPack : MonoBehaviour
{
    [Inject] private BackpackManager _backpackManager;
    [SerializeField]private InventoryCell[] _inventoryCells;
    public UnityEvent<IItem> OnItemAdded = new UnityEvent<IItem>();    
    public UnityEvent<IItem> OnItemRemoved = new UnityEvent<IItem>();
    private void Awake()
    {
        
        _backpackManager.RegisterBackpack(this);
    }

    private void OnDestroy()
    {
        _backpackManager.UnregisterBackpack(this);
    }
    public void ItemAdded(IItem item)
    {
        foreach (var cell in _inventoryCells)
        {
            if (cell.Type== item.Type)
            {
                if (cell.TryAddItem(item))
                {

                     OnItemAdded.Invoke(item);
                }
            }
        }
    }
    public void ItemRemoved(IItem item)
    {
        foreach (var cell in _inventoryCells)
        {
            if (item == cell.Item)
            {
                cell.RemoveItem();
                OnItemRemoved?.Invoke(item);
            }
        }
    }    
    
}

