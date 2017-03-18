using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourController : MonoBehaviour {

	public Material[] materialsArray = new Material[0];

    protected string[] tagArray;
    protected Shader unlitShader;
    protected Shader standardShader;

    private int randomMaterialSelector;

	void Start(){
        unlitShader = Shader.Find("Unlit/Color");
        standardShader = Shader.Find("Standard");
        tagArray = new string[materialsArray.Length];

        tagArray[0] = "RedMaterial";
        tagArray[1] = "YellowMaterial";
        tagArray[2] = "GreenMaterial";
        tagArray[3] = "BlueMaterial";
	}

    public void AssignColour(Renderer rend, Material colourMaterial)
    {
        rend.material = colourMaterial;
    }

    public void AssignRandomColour(GameObject myGameObject, bool unlitBullet)
    {
        Renderer rend = myGameObject.GetComponent<Renderer>();
		randomMaterialSelector = Random.Range(0, materialsArray.Length);
 		rend.material = materialsArray [randomMaterialSelector];
        if (unlitBullet)
        {
            rend.sharedMaterial.shader = unlitShader;
        }
        else
        {
            rend.material.shader = standardShader;
        }
        myGameObject.tag = tagArray[randomMaterialSelector];
    }

}
