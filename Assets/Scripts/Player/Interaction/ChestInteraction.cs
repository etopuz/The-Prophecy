using UnityEngine;
using TheProphecy.LevelRun;
using TheProphecy.Items;
using TheProphecy.Interfaces;

public class ChestInteraction : MonoBehaviour, IInteractable
{

    [SerializeField] private SpriteRenderer _openedChest;
    [SerializeField] private SpriteRenderer _closedChest;
    [SerializeField] private BoxCollider2D _interactableBoxCollider;

    public void OnInteract()
    {
        OpenChest();
    }

    private void OpenChest()
    {
        LevelRunStats levelStats = LevelManager.instance.levelRunStats;
        bool canOpenChest = levelStats.TryToUseKey();

        ItemDatabase itemDatabase = InventoryManager.instance.itemDatabase;

        if (canOpenChest)
        {
            _openedChest.enabled = true;
            _closedChest.enabled = false;
            _interactableBoxCollider.enabled = false;
            ItemSO randomItem = itemDatabase.allItems.ReturnRandomElement();
            Debug.Log(randomItem.itemType);
        }
    }
}
