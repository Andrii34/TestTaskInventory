using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public interface IItem
{
    string Name { get; }
    string ID { get; }
    float Weight { get; }
    ItemType Type { get; }
    GameObject GameObject { get; }
    public bool IsSelected { get; set; }
    public void SetRigidbodyState(bool isEnabled);

}


