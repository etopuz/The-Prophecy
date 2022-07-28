using TheProphecy;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryUI;
    static public Canvas mainCanvas;

    private void Start()
    {
        _inventoryUI.SetActive(false);
        mainCanvas = transform.GetComponent<Canvas>();
    }

    public void ToggleInventoryPanel(bool open)
    {
        _inventoryUI.SetActive(open);
    }
}
