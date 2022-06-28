using System.Collections;
using System.Collections.Generic;
using TheProphecy.Player;
using UnityEngine;
using UnityEngine.UI;

namespace TheProphecy
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private MovementController movement;
        [SerializeField] private Button _dashButton;

        private void Update()
        {
            OnDashButtonPressed(movement.DashCooldownPercentage);
        }

        public void OnDashButtonPressed(float fillPercentage)
        {
            _dashButton.image.fillAmount = fillPercentage;
        }
    }

}
