using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.state == GameManager.State.Play)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + -.03f);
        }
    }
}
