using UnityEngine;


namespace TheProphecy.Enemy
{
    public class BaseEnemy : MonoBehaviour
    {

        private int _health = 5;

        public void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health < 1)
                gameObject.SetActive(false);
        }
    }
}
