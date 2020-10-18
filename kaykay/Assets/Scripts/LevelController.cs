using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
  //  float distance; //Haritanın uzunluğu
  //  float startPosZ;
  //  int level = 1;
  ////  [SerializeField] Transform finalLine;
  ////  [SerializeField] Transform Stack;
  //  [SerializeField] CoinController coinController;
  //  void Start()
  //  {
  //     // startPosZ = Stack.position.z; //kullanıcının başlangıç X değeri her zaman 0'dır. Z değeri değişme ihtimaline karşı al ve değişkene at.
  //   //   GetLevelDistance(); //mesafeyi hesapla
  ////      level = PlayerPrefs.GetInt("level", 1);
  //  }
  //  public void WinLevel(int coin,int NextLevel)
  //  {
  //      coinController.AddCoin(coin);
  //      SceneManager.LoadScene(NextLevel - 1);
  //      //NextLevel();
  //  }
  //  public void NextLevel()
  //  {
  //      level++;
  //      PlayerPrefs.SetInt("level", level);


        
  //      OpenLevel();
  //  }
  //  public void RestartLevel()
  //  {
  //      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  //    //  OpenLevel();
  //  }
  //  void OpenLevel()
  //  {
  //      switch ((level - 1) % 3)
  //      {
  //          case 2: SceneManager.LoadScene(2); break;
  //          case 1: SceneManager.LoadScene(1); break;
  //          case 0: SceneManager.LoadScene(0); break;
  //      }
  //  }
  ///*  void GetLevelDistance() //Level uzunluğunu hesaplamak için kullanılan metoddur.
  //  {
  //      distance = finalLine.position.z - startPosZ; //bitiş çizgisinin z değerinden başlangıç z değerini çıkart.
  //      distance += finalLine.position.x > 0 ? finalLine.position.x : finalLine.position.x * -1; ///Dönüş olabilmesine karşın x değerini de hesaba kat (- değer alabileceği için kontrol et - ise -1le çarp)
  //  }*/
  ///*  public float CalcStep()
  //  {
  //      float passDistance = Stack.position.z - startPosZ; ///Geçilen mesafe
  //      return (passDistance * 100) / distance; ///%de olarak gidilen mesafeyi döndür
  //  }*/

    
    int LevelKont=0;
    int Para;
     void Start()
     {//Her oyun başladığında son kayıt lavel aktarılır
        LevelKont = PlayerPrefs.GetInt("Level");
     }

   

    public void WinGame()
    {
        Para = Random.Range(1000, 2000) * (LevelKont+1);//Kazanılan para için

        LevelKont++;//bir sonraki level ataması için

        PlayerPrefs.SetInt("Level", LevelKont);//Son Level Kayıt

        StartCoroutine(WinGameAnimasyon());


       StartCoroutine(GecisAnimasyonSahnesi());
        
       

    }
    

    public void LoseGame()
    {
        Para = Random.Range(250, 750) * (LevelKont+1);

        //Aktif Sahneyi Bştan Başlatır Hemen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        
    }
    //Her Oyunun başındaki anımasyonu çalıştırır

    IEnumerator WinGameAnimasyon()//Animasyon fonksiyonu
    {
        


        yield return new WaitForSeconds(5f);
        //Animasyon Kodu Yazılacak 
        yield return WinGameAnimasyon();
    }
    IEnumerator GecisAnimasyonSahnesi()
    {
        //Formalite icabı 999 yazıldı 
        SceneManager.LoadScene(999);

        yield return new WaitForSeconds(5f);

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(LevelKont);
        }
    }

}
