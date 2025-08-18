using TMPro;
using UnityEngine;

// ------------------------------------------------------------
// -----------  What UI Manager will do?  ---------------------
// ------------------------------------------------------------
// 1. Control in-game menus
// 2. HUD (score, lives, currency)
// 3. Handle showing/hiding panels.
// 4. Update text elements for score or coins.
// 5. Central place to manage animations/transitions for UI.
// ------------------------------------------------------------

public class UIManager : MonoBehaviour
{
    // Creating singleton
    public static UIManager UIInstance { get; private set; }

    [Header("HUD Elements")]
    [SerializeField] private TextMeshProUGUI CoinsDisplay;

    private void Awake()
    {
        if (UIInstance != null && UIInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        UIInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void UpdateCoinsDisplay(int coins)
    {
        if (CoinsDisplay != null)
        {
            CoinsDisplay.text = "Coins: " + coins;
        }
    }
}
