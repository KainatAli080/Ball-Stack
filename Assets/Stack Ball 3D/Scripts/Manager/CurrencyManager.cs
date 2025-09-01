using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    // Creating singleton
    public static CurrencyManager CurrencyInstance { get; private set; }
    public int CurrentCoins { get; private set; }
    private const string COINS_KEY = "Coins";

    // IMP: Use Awake() for initializing data/state
    // BUT Use Start() for communicating with other objects/managers
    private void Awake()
    {
        if(CurrencyInstance != null && CurrencyInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        CurrencyInstance = this;
        DontDestroyOnLoad(gameObject);

        CurrentCoins = PlayerPrefs.GetInt(COINS_KEY, 0);
    }

    private void Start()
    {
        UIManager.UIInstance.UpdateCoinsDisplay(CurrentCoins);
    }

    public void AddCoins(int amounts)
    {
        CurrentCoins += amounts;
        PlayerPrefs.SetInt(COINS_KEY, CurrentCoins);
        UIManager.UIInstance.UpdateCoinsDisplay(CurrentCoins);
        AudioManager.AudioInstance.PlaySFX("CoinsCollected");
    }

    public bool SpendCoins(int amounts)
    {
        if(CurrentCoins >= amounts)
        {
            CurrentCoins -= amounts;
            PlayerPrefs.SetInt(COINS_KEY, CurrentCoins);
            UIManager.UIInstance.UpdateCoinsDisplay(CurrentCoins);
            return true;
        }
        return false;
    }

}
