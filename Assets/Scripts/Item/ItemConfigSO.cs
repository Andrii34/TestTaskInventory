
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemConfig", menuName = "Inventory/ItemConfig")]
public class ItemConfigSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField, TextArea] private string _id;
    [SerializeField] private float _weight;     
    [SerializeField] private ItemType _type;   

    public string Name => _name;
    public string ID => _id;
    public float Weight => _weight;
    public ItemType Type => _type;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(_id))
        {
            _id = GenerateUniqueID();
        }
    }
    /// <summary>
    /// Generates a globally unique identifier (GUID).
    /// Useful for ensuring uniqueness of item IDs.
    /// </summary>
    private string GenerateUniqueID()
    {
        return System.Guid.NewGuid().ToString();
    }
}
