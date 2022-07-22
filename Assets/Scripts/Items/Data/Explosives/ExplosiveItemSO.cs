using UnityEngine;

namespace TheProphecy.Items
{
    [CreateAssetMenu(fileName = "New Explosive_Item", menuName = "Item/Explosive_Item")]
    
    public class ExplosiveItemSO : ItemSO
    {
        public int damage = 2 ;
        public float explosionRange = 1f;

        private void Awake()
        {
            itemType = ItemType.EXPLOSIVE;
        }

    }
}

