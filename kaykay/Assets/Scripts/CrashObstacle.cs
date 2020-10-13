using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashObstacle : MonoBehaviour
{
    GameManager gameManager;
    CoinController coinController;
    MenuController menuController;
    [SerializeField] ParticleSystem boobleParticle;
    Transform cubes;
    bool crashObstacle = false;
    int diamondX = 0;
    private void Start()
    {
        boobleParticle = GameObject.Find("BubbleParticle").GetComponent<ParticleSystem>();
        menuController = GameObject.Find("Canvas").GetComponent<MenuController>();
        coinController = GameObject.Find("GameManager").GetComponent<CoinController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (crashObstacle == false && collision.transform.tag == "Obstacle")
        {
            Vibration.Vibrate(40);
            crashObstacle = true;
            cubes = transform.parent;
            transform.parent = null;
            collision.transform.GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            if (cubes.childCount < 1)
            {
                GameOver();
            }
        }
        else if (collision.transform.tag == "EndMap")
            GameOver();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Lava")
        {
            Vibration.Vibrate(40);
            Destroy(gameObject);
            cubes = transform.parent;
            boobleParticle.Play();
            if (cubes.childCount < 1)
            {
                GameOver();
            }
        }
        else if (other.transform.tag == "Diamond")
        {
            Vibration.Vibrate(29);
            other.gameObject.SetActive(false);
            coinController.AddCollectedCoin();
        }
        switch (other.transform.tag)
        {
            case "X": diamondX = 1; break;
            case "2X": diamondX = 2; break;
            case "3X": diamondX = 3; break;
            case "4X": diamondX = 4; break;
            case "5X": diamondX = 5; break;
            case "10X": diamondX = 10; break;
        }
        Debug.Log("diamondX= " + diamondX);
    }
    void GameOver()
    {
        gameManager.GameOver(diamondX, coinController.collectedCoin);
    }
}
