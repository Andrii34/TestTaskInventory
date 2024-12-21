using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private CellUI[] _cells;
    private void OnEnable()
    {
        BackPack.OnDragged += ShowPanel;
        BackPack.OnDroppedEnd += HidePanel;
    }
    private void OnDisable()
    {
        BackPack.OnDragged -= ShowPanel;
        BackPack.OnDroppedEnd -= HidePanel;
    }
    public void ShowPanel(InventoryCell[] inventoryCells)
    {
        
        _panel.SetActive(true);
        SetSels(inventoryCells);
    }
    public void HidePanel()
    {
        _panel.SetActive(false);
    }
    private void SetSels(InventoryCell[] inventoryCells)
    {
        foreach (InventoryCell cell in inventoryCells)
        {
            foreach (CellUI cellUI in _cells)
            {
                if (cell.Item!=null && cellUI.Type == cell.Type)
                {
                    cellUI.AdItem(cell.Item);
                    
                }
            }
        }
    }

}
