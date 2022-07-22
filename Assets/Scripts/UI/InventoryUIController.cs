using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    public void OnButtonPressed(bool open)
    {

        int numberOfChildren = transform.childCount;

        for (int i = numberOfChildren - 1; i >= 0; i--)
        {
            transform.GetChild(i).gameObject.SetActive(open);
        }

    }
}
