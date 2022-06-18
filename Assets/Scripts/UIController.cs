using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button _dashButton;
    
    public void ControlDashButton(float fillPercentage)
    {
        _dashButton.image.fillAmount = fillPercentage;
    }
}
