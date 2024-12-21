using System;
using System.Collections.Generic;
using UnityEngine;

public class BackpackManager
{
    public event Action<BackPack> OnBackpackRegistered;
    public event Action<BackPack> OnBackpackUnregistered;

    private readonly List<BackPack> _backpacks = new List<BackPack>();

    public IReadOnlyList<BackPack> Backpacks => _backpacks;

    public void RegisterBackpack(BackPack backPack)
    {
        if (!_backpacks.Contains(backPack))
        {
            _backpacks.Add(backPack);
            Debug.Log($"Backpack registered: {backPack.name}");

            
            OnBackpackRegistered?.Invoke(backPack);
        }
    }

    public void UnregisterBackpack(BackPack backPack)
    {
        if (_backpacks.Contains(backPack))
        {
            _backpacks.Remove(backPack);
            Debug.Log($"Backpack unregistered: {backPack.name}");

            
            OnBackpackUnregistered?.Invoke(backPack);
        }
    }
}
