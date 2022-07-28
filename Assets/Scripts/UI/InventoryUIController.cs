using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TheProphecy.Items;
using System;
using TMPro;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventorySystem;

    [SerializeField] private Transform _inventoryPanel;
    [SerializeField] private ItemSlotController[] _itemSlotControllers;

    private void Start()
    {
        _itemSlotControllers = new ItemSlotController[_inventoryPanel.childCount];

        for (int i = 0; i < _itemSlotControllers.Length; i++)
        {
            _itemSlotControllers[i] = _inventoryPanel.GetChild(i).GetComponent<ItemSlotController>();
        }
    }

    private void Update()
    {
        UpdateUI(_inventorySystem.inventory);
    }

    public void UpdateUI(List<ItemSlot> usedInventory)
    {
        for (int i = 0; i < _itemSlotControllers.Length; i++)
        {
            if (i < usedInventory.Count)
            {
                Image icon = _itemSlotControllers[i].icon;
                icon.enabled = true;
                icon.sprite = usedInventory[i].item.icon;

                TextMeshProUGUI stackText = _itemSlotControllers[i].stackText;

                int stackSize = usedInventory[i].stackSize;
                Debug.Log(stackSize);

                stackText.text = stackSize.ToString();

                if(stackSize == 0)
                {
                    stackText.text = "";
                }
            }
        }

    }


}
