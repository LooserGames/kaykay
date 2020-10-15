using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    [SerializeField] ColorControl thisColorControl;
    [SerializeField] Transform JumperObj;
    [SerializeField] MenuController menuController;
    [SerializeField] private GameObject snowTrail;
    [SerializeField] private GameObject snowEffect;
    
    CoinController coinController;
    GameManager gameManager;
    Move move;
    Rigidbody rb;
    Animator animator;
    VoiceController voiceController;
    CameraFollow cameraFollow;
    AudioSource audioSource;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        move = GetComponent<Move>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        coinController = gameManager.GetComponent<CoinController>();
        voiceController = gameManager.GetComponent<VoiceController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (animator.GetInteger("RandomJump") == 0 && other.tag == "Box");
        else if (other.tag == "Slide") SlideAnim(other.transform);
        else if (other.tag == "Speed") SpeedUp();
        else if (other.tag == "SpeedExit") animator.SetBool("SpeedUp", false);
        //else if (other.tag == "RedPanel") ChangeColor(ColorControl.SelectedColor.Red);
       // else if (other.tag == "GreenPanel") ChangeColor(ColorControl.SelectedColor.Green);
        //else if (other.tag == "BluePanel") ChangeColor(ColorControl.SelectedColor.Blue);
       // else if (other.tag == "YellowPanel") ChangeColor(ColorControl.SelectedColor.Yellow);
        else if (other.tag == "Ramp"){ Ramp(); snowTrail.SetActive(false); snowEffect.SetActive(false);}
        else if (other.tag == "Diamond") GetDiamond(other.gameObject);
        else if (other.tag == "Barrel") StartCoroutine(BarrelAnim());
        else if (other.tag == "Lava")
        {
            animator.SetBool("Lava", true);
            XControl(other.tag);
        }
        //StartCoroutine(LavaAnim());

        else XControl(other.tag);

    }

    private void Update()
    {
       
    }

    private void OnCollisionEnter(Collision other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "Plane")
        {
            snowTrail.SetActive(true);
            snowEffect.SetActive(false);
            
        }
        else
        {
            snowTrail.SetActive(false);
            snowEffect.SetActive(true);
           
        }
    }

    IEnumerator LavaAnim()
    {
        animator.SetBool("Lava", true);
        yield return new WaitForSeconds(.5f);
        move.movementSpeed = 0;
        yield return new WaitForSeconds(.2f);
        GetComponent<BoxCollider>().isTrigger = true;
    }
    void XControl(string objTag)
    {
        int XCount = 0;
        switch (objTag)
        {
            case "X": XCount = 1; break;
            case "2X": XCount = 2; break;
            case "3X": XCount = 3; break;
            case "4X": XCount = 4; break;
            case "5X": XCount = 5; break;
            case "6X": XCount = 6; break;
            case "7X": XCount = 7; break;
            case "8X": XCount = 8; break;
            case "9X": XCount = 9; break;
            case "10X": XCount = 10; break;
            default: XCount = 0; break;
        }
        animator.SetBool("Ramp", false);
       // animator.SetBool("Finish", true);
        gameManager.GameOver(XCount, coinController.collectedCoin);
        cameraFollow.AroundPlayerCam();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Box") animator.SetInteger("RandomJump", 0);
        else if (other.tag == "Slide")
        {
            move.OutSlide(); animator.SetBool("Slide", false);
            Vibration.Cancel();
        }
        //else if (other.tag == "Speed") animator.SetBool("SpeedUp", false);
    }
    void GetDiamond(GameObject diamond)
    {
        Vibration.Vibrate(29);
        voiceController.playVoice(audioSource, 0);
        diamond.SetActive(false);
        coinController.AddCollectedCoin();
        
    }
    IEnumerator BarrelAnim()
    {
        animator.SetInteger("RandomJump", 3);
        yield return new WaitForSeconds(0.4f);
        voiceController.playVoice(audioSource, 1);
        rb.AddForce(JumperObj.forward * 190);
        StartCoroutine(BarrelAnimFinish());
    }
    IEnumerator BarrelAnimFinish()
    {
        yield return new WaitForSeconds(0.9f);
        animator.SetInteger("RandomJump", 0);
    }
    /*void ChangeColor(ColorControl.SelectedColor color)
    {
        voiceController.playVoice(audioSource, 2);
        Vibration.Vibrate(29);
        thisColorControl.selectedColor = color;
        thisColorControl.ChangeColor();
    }*/
    void SlideAnim(Transform slide)
    {
        voiceController.playVoice(audioSource, 1, .3f);
        StartCoroutine(voiceController.playVoice(audioSource, 4, .4f, 1));
        Vibration.Vibrate();
        move.OnSlide(slide);
        rb.AddForce(JumperObj.forward * 325);
        animator.SetBool("Slide", true);
        Debug.Log( animator.GetBool("Slide"));
    }
   /* IEnumerator RandomJump(GameObject item)
    {
        /*if (item.GetComponent<ColorControl>().selectedColor == thisColorControl.selectedColor)
        {
            animator.SetInteger("RandomJump", 1/*Random.Range(1, 3));
            yield return new WaitForSeconds(.5f);
            voiceController.playVoice(audioSource, 1);
            //  menuController.FeedBackAnim();
            rb.AddForce(JumperObj.forward * 220);
            TrueColor();
        }
        else StartCoroutine(FalseColor(item.tag));
    }*/
    IEnumerator FalseColor(string itemTag)
    {
        yield return new WaitForSeconds(.7f);
        animator.SetBool("Fail", true);
        XControl(itemTag);
        //Time.timeScale -= .5f;
    }
    void TrueColor()
    { 
    
    }
    void SpeedUp()
    {
       // voiceController.playVoice(audioSource, 3);
        Vibration.Vibrate(40);
        cameraFollow.FinishCam();
        gameManager.state = GameManager.State.Finish;
        animator.SetBool("SpeedUp", true);
      //  Time.timeScale += .2f;
    }
    void Ramp()
    {
        gameManager.state = GameManager.State.Stop;
        JumperObj.eulerAngles = new Vector3(-45, 0, 0);
        rb.AddForce(JumperObj.forward * (600+(800*move.tapAmount)) * 1.75f);
        animator.SetBool("Ramp", true);
    }
}
