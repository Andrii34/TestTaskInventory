using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;


public class InventoryRequestHandler : MonoBehaviour
{
    [Inject] private BackpackManager _backpackManager;

    private void OnEnable()
    {
        // Подписываемся на уже зарегистрированные рюкзаки
        foreach (var backpack in _backpackManager.Backpacks)
        {
            SubscribeToBackpack(backpack);
        }

        // Подписываемся на новые регистрации
        _backpackManager.OnBackpackRegistered += SubscribeToBackpack;
        _backpackManager.OnBackpackUnregistered += UnsubscribeFromBackpack;
    }

    private void OnDisable()
    {
        // Отписываемся от новых регистраций
        _backpackManager.OnBackpackRegistered -= SubscribeToBackpack;
        _backpackManager.OnBackpackUnregistered -= UnsubscribeFromBackpack;

        // Отписываемся от всех существующих рюкзаков
        foreach (var backpack in _backpackManager.Backpacks)
        {
            UnsubscribeFromBackpack(backpack);
        }
    }

    private void SubscribeToBackpack(BackPack backpack)
    {
        Debug.Log($"Subscribed to backpack: {backpack.name}");
        backpack.OnItemAdded.AddListener(OnItemAdded);
        backpack.OnItemRemoved.AddListener(OnItemRemoved);
    }

    private void UnsubscribeFromBackpack(BackPack backpack)
    {
        Debug.Log($"Unsubscribed from backpack: {backpack.name}");
        backpack.OnItemAdded.RemoveListener(OnItemAdded);
        backpack.OnItemRemoved.RemoveListener(OnItemRemoved);
    }

    private void OnItemAdded(IItem item)
    {
        Debug.Log($"Item added: {item.ID} - {item.Name}");
    }

    private void OnItemRemoved(IItem item)
    {
        Debug.Log($"Item removed: {item.ID} - {item.Name}");
    }
}


