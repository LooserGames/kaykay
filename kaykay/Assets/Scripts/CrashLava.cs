using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashLava : MonoBehaviour
{
    bool isLava = false;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Lava")
        {
            isLava = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Lava")
            isLava = false;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (isLava || collision.transform.tag == "Cube")
            Destroy(collision.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
