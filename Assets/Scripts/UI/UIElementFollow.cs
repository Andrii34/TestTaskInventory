using UnityEngine;

public class UIElementFollow : MonoBehaviour
{
     private Camera cameraToUse; 
    [SerializeField] private Transform targetObject; 
    [SerializeField] private RectTransform uiElement;
    [SerializeField] private Vector3 _offsetPos;

    private void Start()
    {
        cameraToUse =Camera.main;
    }
    void Update()
    {
        
        Vector3 screenPos = cameraToUse.WorldToScreenPoint(targetObject.position);

       
        uiElement.position = screenPos+_offsetPos;
    }
}
