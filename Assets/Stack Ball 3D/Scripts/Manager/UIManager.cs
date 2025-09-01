using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

// ------------------------------------------------------------
// -----------  What UI Manager will do?  ---------------------
// ------------------------------------------------------------
// 1. Control in-game menus
// 2. HUD (score, lives, currency)
// 3. Handle showing/hiding panels.
// 4. Update text elements for score or coins.
// 5. Central place to manage animations/transitions for UI.
// ------------------------------------------------------------

[System.Serializable]
public class UIPanel
{
    public string panelName;        // Unique name (like "PauseMenu", "Settings")
    public GameObject panelRef;     // Reference to the actual panel GameObject
}

public class UIManager : MonoBehaviour
{
    // Creating singleton
    public static UIManager UIInstance { get; private set; }

    [Header("Panels List")]
    // list of Panels (convert to dict at runtime for faster lookups)
    [SerializeField] private List<UIPanel> panelList;
    private Dictionary<string, UIPanel> panelDictionary;

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

        // Now converting list to dict
        panelDictionary = new Dictionary<string, UIPanel>();
        foreach (var panel in panelList)
        {
            panelDictionary[panel.panelName] = panel;
        }
    }

    public void ShowPanel(string name)
    {
        if (panelDictionary.TryGetValue(name, out UIPanel panelToFind))
        {
            panelToFind.panelRef.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Panel {name} Not Found");
        }
    }

    public void HidePanel(string name)
    {
        if (panelDictionary.TryGetValue(name, out UIPanel panelToFind))
        {
            panelToFind.panelRef.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Panel {name} Not Found");
        }
    }

    public void HideAllPanels()
    {
        // for when gameplay starts, hide everything just in case (unless you figure otherwise)
        foreach (var panel in panelDictionary.Values)
        {
            panel.panelRef.SetActive(false);
        }
    }

    public void UpdateCoinsDisplay(int coins)
    {
        if (CoinsDisplay != null)
        {
            CoinsDisplay.text = "Coins: " + coins;
        }
    }
}
