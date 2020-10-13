using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorControl : MonoBehaviour
{
    public SelectedColor selectedColor = new SelectedColor();
    GameManager gameManager;
    private void Start()
    {
       gameManager =  GameObject.Find("GameManager").GetComponent<GameManager>();
        ChangeColor();
    }
    public void ChangeColor()
    {
        switch (selectedColor) ////0 Red 1 Green 2 Blue
        {
            case SelectedColor.Red:
                gameObject.GetComponent<Renderer>().material = gameManager.materials[0];
                break;
            case SelectedColor.Green:
                gameObject.GetComponent<Renderer>().material = gameManager.materials[1];
                break;
            case SelectedColor.Blue:
                gameObject.GetComponent<Renderer>().material = gameManager.materials[2];
                break;
            case SelectedColor.Yellow:
                gameObject.GetComponent<Renderer>().material = gameManager.materials[3];
                break;
        }
    }
    public enum SelectedColor
    { 
        Red,
        Green,
        Blue,
        Yellow
    }
}
