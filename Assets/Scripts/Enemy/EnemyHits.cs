using UnityEngine;


namespace TheProphecy
{
    public class EnemyHits : MonoBehaviour
    {

        private int _health = 5;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(TagLayerData.BULLET))
            {

                collision.gameObject.SetActive(false);

                _health -= 1;

                if (_health < 1)
                    gameObject.SetActive(false);
            }
        }
    }
}
