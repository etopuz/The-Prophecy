using TheProphecy;
using TheProphecy.LevelRun;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryUI;
    [SerializeField] private GameObject _inGameUI;
    [SerializeField] private GameObject _deathScreenUI;

    static public Canvas mainCanvas;

    private void Start()
    {
        mainCanvas = transform.GetComponent<Canvas>();
    }

    public void ToggleInventoryPanel(bool open)
    {
        _inventoryUI.SetActive(open);
        _inGameUI.SetActive(!open);
    }

    public void ToggleDeathScreen(bool open)
    {
        _deathScreenUI.SetActive(open);

        if (open)
        {
            _inventoryUI.SetActive(false);
            _inGameUI.SetActive(false);
        }

        else
        {
            _inGameUI.SetActive(true);
        }
    }

    public void OnPlayAgainButtonPressed()
    {
        LevelManager.instance.ResetLevel();
    }
}
