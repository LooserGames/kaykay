using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
    }
    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * 50, 0);
    }
    public void AddCoin()
    {
        anim.Play();
        gameObject.SetActive(false);
    }
}
