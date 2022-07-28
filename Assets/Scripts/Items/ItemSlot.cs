namespace TheProphecy.Items
{
    [System.Serializable]
    public class ItemSlot
    {
        public ItemSO item;
        public int stackSize;

        public ItemSlot(ItemSO item, int count)
        {
            this.item = item;
            AddToStack(count);
        }

        public void AddToStack(int count = 1)
        {
            stackSize += count;
        }

        public void RemoveFromStack(int count = 1)
        {
            stackSize -= count;
        }
    }
}
