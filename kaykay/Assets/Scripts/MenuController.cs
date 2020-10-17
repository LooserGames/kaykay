using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] LevelController levelController;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameMenu;
    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] GameObject coinAnimObj;
    [SerializeField] GameObject gameWinPanel;
    [SerializeField] GameObject gameLossPanel;
    [SerializeField] TextMeshProUGUI gameWinDiamondX;
    [SerializeField] TextMeshProUGUI DiamondCountText;
    [SerializeField] TextMeshProUGUI cubeLevelText;
    [SerializeField] TextMeshProUGUI cubePriceText;
    [SerializeField] private GameObject snowTrailEffect;
    public GameObject powerPanel;
    [SerializeField] Image powerBar;
    [SerializeField] Animation artiBir;
    [SerializeField] TextMeshProUGUI feedbackText;
    CoinController coinController;
    List<GameObject> coinAnimList= new List<GameObject>();
    int coinAnimCount = 5;
    int currentCoinAnimCount = 1;

    int winDiamond = 0;
    private void Start()
    {
        coinController = gameManager.gameObject.GetComponent<CoinController>();
        mainMenu.SetActive(true);
        gameMenu.SetActive(true);
        coinText.text = PlayerPrefs.GetInt("coin", 0).ToString();
        Debug.Log("Start");
        for (int i = 0; i < coinAnimCount; i++)
        {
            coinAnimList.Add(Instantiate(coinAnimObj,coinAnimObj.transform.position,coinAnimObj.transform.rotation,coinAnimObj.transform.parent));
        }
        GetCubeLevel();
        snowTrailEffect.SetActive(false);
        
    }
    private void Update()
    {
      /*  if (gameManager.state == GameManager.State.Play)
        {
            progressBar.fillAmount = levelController.CalcStep() / 100;
        }*/
    }
    public void PlayButton()
    {
        mainMenu.SetActive(false);
        gameManager.state = GameManager.State.Play;
        snowTrailEffect.SetActive(true);
    }

    public void GameOver(int diamondX,int collectedDiamond)
    {
        if (diamondX < 1) //eğer diamond sayısı 1den küçükse
            gameLossPanel.SetActive(true);
        else
        {
            winDiamond = collectedDiamond * diamondX;
            DiamondCountText.text = winDiamond.ToString();
            gameWinDiamondX.text = diamondX.ToString() + "X";
            gameWinPanel.SetActive(true);
        }
    }
    void GetCubeLevel()
    {
        cubeLevelText.text = PlayerPrefs.GetInt("cubeLevel", 1).ToString();
        cubePriceText.text = (PlayerPrefs.GetInt("cubeLevel", 1) * 200).ToString();
    }
    public void AddCube()
    {
        if (coinController.RemoveCoin(int.Parse(cubePriceText.text)))
        {
            PlayerPrefs.SetInt("cubeLevel", int.Parse(cubeLevelText.text) + 1);
            DiamondCountText.text = (int.Parse(DiamondCountText.text) - int.Parse(cubePriceText.text)).ToString();
            GetCubeLevel();
        }
    }
    public void ClaimButton(int nextLevel)
    {
        levelController.WinLevel(winDiamond, nextLevel);
    }
    public void RetryButton()
    {
        levelController.RestartLevel();
    }

    public void AddCoin()
    {
        if (currentCoinAnimCount == coinAnimCount)
            currentCoinAnimCount = 1;
        else
            currentCoinAnimCount++;
        UseCoinAnim(currentCoinAnimCount).SetActive(true);
        UseCoinAnim(currentCoinAnimCount).GetComponent<Animator>().enabled = true;
        StartCoroutine(StopAnimWithSec(currentCoinAnimCount));
    }
    GameObject UseCoinAnim(int animId)
    {
        DiamondAnim();
        Debug.Log("AnimId= "+ (animId - 1).ToString());
        Debug.Log("List sayısı= "+ coinAnimList.Count);
        return coinAnimList[animId-1];
    }
    IEnumerator StopAnimWithSec(int AnimId)
    {
        yield return new WaitForSeconds(1);
        coinText.text = (int.Parse(coinText.text) + 1).ToString();
        UseCoinAnim(AnimId).SetActive(false);
        UseCoinAnim(AnimId).GetComponent<Animator>().enabled = false;
    }
    public void setPowerBar(float fillAmount)
    {
        powerPanel.SetActive(true);
        powerBar.fillAmount = fillAmount;
    }
    void DiamondAnim()
    {
     /*   artiBir.gameObject.SetActive(true);
        artiBir.Play();*/
    }
    public void FeedBackAnim()
    {
      /*  feedbackText.gameObject.SetActive(true);
        string[] feedBackTexts = new string[] { "PERFECT", "AWESOME", "GOOD" };
        feedbackText.text = feedBackTexts[Random.Range(0, 3)];
        feedbackText.gameObject.GetComponent<Animation>().Play();*/
    }
}
