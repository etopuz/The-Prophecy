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
            OnButtonPressed(_dashButton, movement.DashCooldownPercentage);
        }

        public void OnButtonPressed(Button button, float fillPercentage)
        {
            button.image.fillAmount = fillPercentage;
        }
    }

}
