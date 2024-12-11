using UnityEngine;
using System;

public class QuadByCode : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private Material _quadMaterial;
	#endregion

	#region Unity Callbacks
	// Start is called before the first frame update
	void Start()
    {
		MeshFilter myMeshFilter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer myRender = gameObject.AddComponent<MeshRenderer>();
		myRender.material = _quadMaterial;

		Mesh myMesh = new Mesh();

		myMesh.vertices = new Vector3[] {
			new Vector3(0,0,0), //0
			new Vector3(1, 0, 0),//1
			new Vector3(1, 1, 0),//2
			new Vector3(0, 1, 0),//3
		};

		myMesh.triangles = new int[] {
			0,3,1, //Triangulo 1
			3,2,1  //Triangulo 2
		};

		myMeshFilter.mesh = myMesh;



	}

    // Update is called once per frame
    void Update()
    {
        
    }
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion   
}
