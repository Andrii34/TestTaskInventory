using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour,IItem
{
    private string _name;
    private string _id;
    private float _weight;
    private ItemType _type;
    private Rigidbody _rigidbody;
   
    public string Name => _name;
    public string ID => _id;
    public float Weight => _weight;
    public ItemType Type => _type;
    public GameObject GameObject=>gameObject;
    private bool _isInInventory;
    private BackPack _backPack;
    [SerializeField] private ItemConfigSO _config;
   
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_config != null)
        {
            _name = _config.Name;
            _id = _config.ID;
            _weight = _config.Weight;
            _type = _config.Type;
            _rigidbody.mass = _weight;
        }

        Debug.Log($"Item '{_name}' initialized with ID: {_id}");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_isInInventory)
        {

              BackPack backPack = other.GetComponent<BackPack>();
            if (backPack != null)
            {
            _backPack = backPack;
            _backPack.ItemAdded(this);  
            _isInInventory = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_isInInventory)
        {

        InventoryCell inventoryCell = other.GetComponent<InventoryCell>();
        if (inventoryCell != null)
        {
            _backPack.ItemRemoved(this);
            _backPack = null;
            _isInInventory= false;
        }    
        }
    }
    public void SetRigidbodyState(bool isEnabled)
    {
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = !isEnabled; 
        }
       
    }



}
