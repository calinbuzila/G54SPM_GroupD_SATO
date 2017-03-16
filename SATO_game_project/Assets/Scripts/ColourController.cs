using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourController : MonoBehaviour {

    public Material redMaterial;
    public Material yellowMaterial;
    public Material greenMaterial;

	public Material[] materialsArray = new Material[0];
	private int randomMaterialSelector;


	void Start(){


	}

    public void AssignColour(Renderer rend, Material colourMaterial)
    {
        rend.material = colourMaterial;
	
    }

    public void AssignRandomColour(Renderer rend)
    {
//		print ("show me the money");

		randomMaterialSelector = Random.Range(0, materialsArray.Length);
//		Debug.Log ("random range ", randomMaterialSelector);


 		rend.material = materialsArray [randomMaterialSelector];

//		rend.material = materialsArray [0];

        //TODO Make the random colour generator
    }

}
