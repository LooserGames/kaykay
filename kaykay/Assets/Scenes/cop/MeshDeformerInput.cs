using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformerInput : MonoBehaviour
{
	//Uygulunacak Kuvvet
    public float force = 10f;
	public float forceOffset = 0.1f;


	//Eklenen Kod Kısmı 
	Vector3 vecYon = new Vector3(0, -0.3f, 0);
	//hangi objeden yere yada farklı biryere deforme edilmesi isteniyorsa o objeye verilcek
	public GameObject Circle;

	void Update()
	{   //HandleInput un çalışması için sol tık mouse ile çalıştırma 
		//if (Input.GetMouseButton(0))
		//{
		//	HandleInput();
		//}

		//Sureklı Calismasi icin if den kurtardım
		HandleInput();
		Debug.DrawLine(Circle.transform.position,transform.position,Color.red);
	}
	void HandleInput()
	{  //cameradan farenin bulunduğu noktayı gösterir takip eder ışın oluşturma
	   //burası kaykaydan aşağı doğru olarak değiştirilmeli yada collider eklenerek
	   // (Orjinal Kod) Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		Ray inputRay = new Ray(Circle.transform.position, Vector3.down);
		RaycastHit hit;
		

		if (Physics.Raycast(inputRay, out hit,1f))
		{
			//Işın bir şeyle çarpışırsa, MeshDeformerbileşeni vurulan nesneden alabiliriz.
			MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();

			//Bir şeye çarparsak ve bir şeyin bir MeshDeformerbileşeni varsa, o şeyi deforme edebiliriz!
			if (deformer)
			{
				Vector3 point = hit.point;
				point += hit.normal * forceOffset;

				deformer.AddDeformingForce(point, force);
			}

		}
	}







}
