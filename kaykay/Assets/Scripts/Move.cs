using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Image tapImage;
    [SerializeField] MenuController menuController;
    public float tapAmount = 0;
    public float movementSpeed;
    [SerializeField] float controlSpeed;
    //Touch settings
    [SerializeField] bool isTouching;
    float touchPosX;
    bool powerPanelActive = false;
    bool slide = false;
    Transform slidePipe;
    void Update()
    {
            GetInput();
    }
    void FixedUpdate()
    {
        if (gameManager.state == GameManager.State.Play)
        {
            transform.position += Vector3.forward * movementSpeed * Time.fixedDeltaTime; //objeyi Z pozisyonunda sürekli hareket ettir

            if (slide)
                touchPosX = slidePipe.position.x;
            else
            {
                if (isTouching)
                {
                    touchPosX += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime; //Mouse'nin X düzlemindeki yerine göre değeri hesapla ve değişkene at.

                    if (touchPosX > 2.4f) touchPosX = 2.4f; ///Sağa 2 adımdan fazla gitmesin
                    else if (touchPosX < -2.5f) touchPosX = -2.5f; //Sola 2 adımdan fazla gitmesin  
                    if (touchPosX < transform.position.x)
                    {
                        GetComponent<Animator>().SetBool("Left", true);
                    }
                    else if (touchPosX > transform.position.x)
                    {
                        GetComponent<Animator>().SetBool("Right", true);
                    }
                }
                else
                {
                    GetComponent<Animator>().SetBool("Left", false);
                    GetComponent<Animator>().SetBool("Right", false);
                }
            }
            transform.position = new Vector3(touchPosX, transform.position.y, transform.position.z); //pozisyonu ayarla.
        }
        else if (gameManager.state == GameManager.State.Finish)
        {
            transform.position += Vector3.forward * movementSpeed * Time.fixedDeltaTime; //objeyi Z pozisyonunda sürekli hareket ettir
            powerPanelActive = true;
            NegativeTap();
        }
    }
    void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            isTouching = true;
            if (gameManager.state == GameManager.State.Finish)
            {
                PlusTap();
            }
        }
        else
        {
            isTouching = false;
        }
        if (powerPanelActive && gameManager.state != GameManager.State.Finish)
            StartCoroutine(closePowerPanel());
    }
    IEnumerator closePowerPanel()
    {
        powerPanelActive = false;
        yield return new WaitForSeconds(2);
        menuController.powerPanel.SetActive(false);
    }
    public void OnSlide(Transform slideObj)
    {
        slide = true;
        slidePipe = slideObj;
    }
    public void OutSlide()
    {
        slide = false;
    }
    void PlusTap()
    {
        if (tapAmount < 1)
            tapAmount += .085f;
        menuController.setPowerBar(tapAmount);
    }
    void NegativeTap()
    {
        if (tapAmount > 0)
            tapAmount -= .05f;
        menuController.setPowerBar(tapAmount);
    }

    private void OnCollisionEnter(Collision other)
    {
        

    }
}
