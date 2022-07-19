using System;
using System.Collections;
using System.Collections.Generic;
using TheProphecy.LevelRun;
using UnityEngine;

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

        if (canOpenChest)
        {
            _openedChest.enabled = true;
            _closedChest.enabled = false;
            _interactableBoxCollider.enabled = false;
        }
    }
}
