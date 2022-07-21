using UnityEngine;

namespace TheProphecy.Items
{
    [CreateAssetMenu(fileName = "New PowerUp_Item", menuName = "Item/PowerUp_Item")]
    
    public class PowerUpItemSO : ItemSO
    {
        private void Awake()
        {
            itemType = ItemType.POWER_UP;
        }
    }
}

