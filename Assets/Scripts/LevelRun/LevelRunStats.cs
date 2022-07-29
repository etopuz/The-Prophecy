
namespace TheProphecy.LevelRun
{
    public class LevelRunStats
    {
        public int killCount;
        public int keyCount;
        public int coinCount;

        public LevelRunStats()
        {
            killCount = 0;
            keyCount = 10;
            coinCount = 0;
        }

        public void Reset()
        {
            killCount = 0;
            keyCount = 10;
            coinCount = 0;
        }

        public void AddKey()
        {
            keyCount++;
        }

        public bool TryToUseKey()
        {
            if (keyCount > 0)
            {
                keyCount--;
                return true;
            }

            return false;
        }

        public void AddKill()
        {
            killCount++;
        }

        public int GetNumberOfKills()
        {
            return killCount;
        }

        public void AddCoins(int numberOfCoins)
        {
            coinCount += numberOfCoins;
        }

        public bool TryToUseCoins(int numberOfCoins)
        {
            if (coinCount >= numberOfCoins)
            {
                coinCount -= numberOfCoins;
                return true;
            }

            return false;
        }
    }
}

