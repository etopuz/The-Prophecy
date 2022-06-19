using UnityEngine;


namespace TheProphecy
{
    public class EnemyHits : MonoBehaviour
    {

        private int _health = 500;

        public void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health < 1)
                gameObject.SetActive(false);
        }
    }
}
