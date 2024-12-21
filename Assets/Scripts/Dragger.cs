using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    private Rigidbody _rigidbody;
   private IItem _item;
    private bool _isSelected;
    private void OnEnable()
    {
        CellUI.OnRemoved += Select;
    }
    private void OnDisable()
    {
        CellUI.OnRemoved += Select;
        
    }
    private void Start()
    {
        _item = GetComponent<IItem>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnMouseDrag()
    {
       MoveToMouse();
    }
    private void Update()
    {
        if (_isSelected)
        {
            MoveToMouse();
            if (Input.GetMouseButtonUp(0))
            {
                OfSelect();
            }
        }
    }
    private void OnMouseUp()
    {
      OfSelect();
    }
    private void MoveToMouse()
    {
        if (_item != null)
        {
            _item.IsSelected = true;
        }
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z + transform.position.z);
        Vector3 objectPosithion = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objectPosithion;
        _rigidbody.isKinematic = true;
    }
    private void OfSelect()
    {
        _isSelected = false;
        _rigidbody.isKinematic = false;
        _item.IsSelected = false;
    }
    private void Select(IItem item)
    {
        
        if (item == _item)
        {
            _isSelected = true;
        }
    }
    
}
