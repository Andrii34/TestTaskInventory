using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    private Rigidbody _rigidbody;
   
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z + transform.position.z);
        Vector3 objectPosithion = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objectPosithion;
        _rigidbody.isKinematic = true;
    }
    private void OnMouseUp()
    {
        _rigidbody.isKinematic = false;
    }

}
