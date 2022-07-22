using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheProphecy.Items
{
    public class ItemDatabase : MonoBehaviour
    {
        [SerializeField]
        private List<HealtRegenItemSO> _healthRegenItems = new List<HealtRegenItemSO>();
        public List<HealtRegenItemSO> HealthRegenItems { get => _healthRegenItems; }

        [SerializeField]
        private List<ExplosiveItemSO> _explosiveItems = new List<ExplosiveItemSO>();
        public List<ExplosiveItemSO> ExplosiveItems { get => _explosiveItems; }

        [SerializeField]
        private List<PowerUpItemSO> _powerUpItems = new List<PowerUpItemSO>();
        public List<PowerUpItemSO> PowerUpItems { get => _powerUpItems;}

        public List<ItemSO> allItems = new List<ItemSO>();

        private void Awake()
        {
            allItems = _healthRegenItems.Cast<ItemSO>()
                .Concat(_explosiveItems.Cast<ItemSO>())
                .Concat(_powerUpItems.Cast<ItemSO>()).ToList();
        }

    }
}

