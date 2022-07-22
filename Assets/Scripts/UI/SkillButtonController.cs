using System.Collections;
using System.Collections.Generic;
using TheProphecy.Player;
using UnityEngine;
using UnityEngine.UI;

namespace TheProphecy
{
    public class SkillButtonController : MonoBehaviour
    {

        [Header("Controllers")]
        [SerializeField] private MovementController _movementController;
        [SerializeField] private InvisibilityController _invisibilityController;

        [Header("Buttons")]
        [SerializeField] private Button _dashButton;
        [SerializeField] private Button _invisibilityButton;


        private void Update()
        {
            OnButtonPressed(_dashButton, _movementController.GetCooldownPercentage());
            OnButtonPressed(_invisibilityButton, _invisibilityController.GetCooldownPercentage());
        }

        public void OnButtonPressed(Button button, float fillPercentage)
        {
            button.image.fillAmount = fillPercentage;
        }
    }

}
