using TheProphecy.LevelRun;
using TMPro;
using UnityEngine;

public class UpdateCounters : MonoBehaviour
{

    [Header("Text References")]
    [SerializeField] private TextMeshProUGUI _killText;
    [SerializeField] private TextMeshProUGUI _keyText;
    [SerializeField] private TextMeshProUGUI _coinText;

    LevelRunStats levelStats;

    private void Start()
    {
        levelStats = LevelManager.instance.levelRunStats;
    }

    private void Update()
    {
        UpdateOnChange();
    }

    private void UpdateOnChange()
    {
        _killText.text = levelStats.killCount.ToString();
        _keyText.text = levelStats.keyCount.ToString();
        _coinText.text = levelStats.coinCount.ToString();
    }
}
