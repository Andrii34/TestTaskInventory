using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class BackPack : MonoBehaviour
{
    [Inject] private BackpackManager _backpackManager;

    [SerializeField] private InventoryCell[] _inventoryCells;

    public UnityEvent<IItem> OnItemAdded = new UnityEvent<IItem>();
    public UnityEvent<IItem> OnItemRemoved = new UnityEvent<IItem>();
    public static  event  Action<InventoryCell[]>  OnDragged;
    public static event Action OnDroppedEnd;
    
    private void Awake()
    {
        foreach (var cell in _inventoryCells) 
        {
            cell.Init(this);
        }
        _backpackManager.RegisterBackpack(this);
    }
    private void OnEnable()
    {
        CellUI.OnRemoved += RemoveItem;
    }
    private void OnDisable()
    {
        CellUI.OnRemoved -= RemoveItem;
        
    }
    private void OnDestroy()
    {
        _backpackManager.UnregisterBackpack(this);
    }

    public void AddItem(IItem item)
    {
        foreach (var cell in _inventoryCells)
        {
            if (cell.Type == item.Type)
            {
                if (cell.TryAddItem(item))
                {
                    OnItemAdded.Invoke(item);
                }
            }
        }
    }

    public void RemoveItem(IItem item)
    {
        if (item == null)
        {
            return;
        }
        foreach (var cell in _inventoryCells)
        {
            if (item == cell.Item)
            {
                cell.RemoveItem();
                OnItemRemoved?.Invoke(item);
                break;
            }
        }
        OnDroppedEnd?.Invoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        IItem item = other.GetComponent<IItem>();
        if (item != null) 
        {
            AddItem(item);
        }
    }
    private void OnMouseDown()
    {
        OnDragged?.Invoke(_inventoryCells);
    }
    private void OnMouseUp()
    {
        OnDroppedEnd?.Invoke();
    }
}

