using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Material> materials = new List<Material>();////0 Red 1 Green 2 Blue
    public State state;
    [SerializeField] MenuController menuController;
    [SerializeField] ParticleSystem finishParticle;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 2.25f;
        state = State.Stop;
    }
    public void GameOver(int diamondX,int collectedDiamond)
    {
        state = State.Stop;
        if (diamondX > 0) ////DiamondX 0dan büyükse kazanmıştır
        {
            finishParticle.Play(); ///particle patlat
            StartCoroutine(FinishParticle(diamondX, collectedDiamond)); // 1 saniye sonra menüyü aç
        }
        else ///kazanamadıysa
            menuController.GameOver(diamondX, collectedDiamond); ///menüyü aç
    }
    IEnumerator FinishParticle(int diamondX, int collectedDiamond)
    {
        yield return new WaitForSeconds(1);
        menuController.GameOver(diamondX, collectedDiamond);
    }
    public enum State
    { 
        Stop,
        Finish,
        Play
    }
}
