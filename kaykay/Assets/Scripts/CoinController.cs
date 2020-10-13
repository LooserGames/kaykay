using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    int coin;
    public int collectedCoin = 0;
    [SerializeField] MenuController menuController;
    void Start()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
    }
    public void AddCollectedCoin()
    {
        collectedCoin++;
        menuController.AddCoin();
    }
    public void AddCoin(int coinCount)
    {
        PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + coinCount); ///Coin değerini al coinCount kadar arttır.
        coin = PlayerPrefs.GetInt("coin", 0);
    }
    public bool RemoveCoin(int coinCount)
    {
        coin = PlayerPrefs.GetInt("coin", 0);
        if (coin > coinCount) ///Eğer mevcut para azalacak paradan fazlaysa
        {
            PlayerPrefs.SetInt("coin", coin - coinCount); ///Coin değerini al coinCount kadar arttır.
            coin = PlayerPrefs.GetInt("coin", 0);
            return true;
        }
        return false;
    }
}
