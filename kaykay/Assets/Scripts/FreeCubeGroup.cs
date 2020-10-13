using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCubeGroup : MonoBehaviour
{
    public int cubeCount = 1;
    GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        cube = transform.GetChild(0).gameObject; //Grubun içindeki obje küpümüzdür

        for (int i = 1; i < cubeCount; i++) ///1den başlayarak istenilen küp sayısı kadar döndür
        {
            Vector3 newPos = new Vector3(cube.transform.position.x, cube.transform.position.y + i, cube.transform.position.z); //oluşturulacak küpün y değerini ayarla
            Instantiate(cube, newPos, cube.transform.rotation,transform); ///küpü oluştur ve pozisyonunu ayarla
        }
    }
}
