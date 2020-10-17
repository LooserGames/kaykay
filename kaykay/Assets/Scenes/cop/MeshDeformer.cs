using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://catlikecoding.com/unity/tutorials/mesh-deformation/ Kaynak site 

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour
{
	Mesh deformingMesh;
	Vector3[] originalVertices, displacedVertices;
	Vector3[] vertexVelocities;
	public float springForce = 20f;
	public float damping = 5f;
	float uniformScale = 1f;


	//Herhangi bir deformasyonu gerçekleştirmek için ağa erişmemiz gerekir. 
	//Ağa sahip olduğumuzda, orijinal köşe konumlarını çıkarabiliriz. 
	//Deformasyon sırasında yer değiştiren köşeleri de takip etmeliyiz.
	void Start()
	{
		deformingMesh = GetComponent<MeshFilter>().mesh;
		originalVertices = deformingMesh.vertices;
		displacedVertices = new Vector3[originalVertices.Length];
		for (int i = 0; i < originalVertices.Length; i++)
		{
			displacedVertices[i] = originalVertices[i];
		}

		//Ağ deforme olurken tepe noktaları hareket eder. 
		//Bu yüzden her bir tepe noktasının hızını da saklamalıyız.
		vertexVelocities = new Vector3[originalVertices.Length];

	}

	//Artık köşelerin hızları olduğuna göre onları hareket ettirebiliriz. 
	//UpdateHer bir tepe noktasını işlemek için bir yöntem ekleyin .
	//Daha sonra, gerçekten değişmesi için yer değiştiren köşeleri ağa atayın.
	//Ağın şekli artık sabit olmadığından, normallerini de yeniden hesaplamamız gerekir.
	void Update()
	{
		uniformScale = transform.localScale.x;

		for (int i = 0; i < displacedVertices.Length; i++)
		{
			UpdateVertex(i);
		}
		deformingMesh.vertices = displacedVertices;
		deformingMesh.RecalculateNormals();
	}

	void UpdateVertex(int i)
	{
		Vector3 velocity = vertexVelocities[i];
		Vector3 displacement = displacedVertices[i] - originalVertices[i];
		displacement *= uniformScale;
		velocity -= displacement * springForce * Time.deltaTime;
		velocity *= 1f - damping * Time.deltaTime;
		vertexVelocities[i] = velocity;
		displacedVertices[i] += velocity * (Time.deltaTime / uniformScale);
	}

	//MeshDeformer.AddDeformingForceHalihazırda yer değiştirmiş tüm köşelerden geçmeli 
	//ve deforme edici kuvveti her bir köşeye ayrı ayrı uygulamalıdır.
	public void AddDeformingForce(Vector3 point, float force)
	{
		point = transform.InverseTransformPoint(point);

		for (int i = 0; i < displacedVertices.Length; i++)
		{
			AddForceToVertex(i, point, force);
		}
		//Kameranın bulunduğu noktadan objeye değdiğimiz noktaya çizgi çeker
		//silinebilir...
		Debug.DrawLine(Camera.main.transform.position, point);
	}
	void AddForceToVertex(int i, Vector3 point, float force)
	{
		Vector3 pointToVertex = displacedVertices[i] - point;
		pointToVertex *= uniformScale;
		float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
		float velocity = attenuatedForce * Time.deltaTime;
		vertexVelocities[i] += pointToVertex.normalized * velocity;

	}







}
