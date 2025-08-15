using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    // Creating singleton
    public static CurrencyManager CurrencyInstance { get; private set; }
    public int CurrentCoins { get; private set; }
    private const string COINS_KEY = "Coins";

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

    public void AddCoins(int amounts)
    {
        CurrentCoins += amounts;
        PlayerPrefs.SetInt(COINS_KEY, CurrentCoins);
        // Also update coins after this
    }

    public bool SpendCoins(int amounts)
    {
        if(CurrentCoins >= amounts)
        {
            CurrentCoins -= amounts;
            PlayerPrefs.SetInt(COINS_KEY, CurrentCoins);
            return true;
        }
        return false;
    }

}
