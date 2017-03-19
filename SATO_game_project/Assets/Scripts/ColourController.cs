using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourController : MonoBehaviour {

	public Material[] materialsArray = new Material[0];
    // Default index is 0 for red.
    public const int DefaultBulletColourIndex = 0;
    protected string[] tagArray;
    protected Shader unlitShader;
    protected Shader standardShader;
    protected int BulletColourIndex;

    private int randomMaterialSelector;

	void Start(){
        BulletColourIndex = DefaultBulletColourIndex;
        unlitShader = Shader.Find("Unlit/Color");
        standardShader = Shader.Find("Standard");
        tagArray = new string[materialsArray.Length];

        tagArray[0] = "RedMaterial";
        tagArray[1] = "YellowMaterial";
        tagArray[2] = "GreenMaterial";
        tagArray[3] = "BlueMaterial";
	}

    public void AssignColour(GameObject myGameObject, int ColourArrayIndex = DefaultBulletColourIndex)
    {
        Renderer rend = myGameObject.GetComponent<Renderer>();
        CheckArrayIndexNotInvalid(ref ColourArrayIndex);
        rend.material = materialsArray[ColourArrayIndex];
        myGameObject.tag = tagArray[ColourArrayIndex];
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

    public void CycleToNextColour(GameObject myGameObject)
    {
        Renderer rend = myGameObject.GetComponent<Renderer>();
        BulletColourIndex++;
        CheckArrayIndexNotInvalid(ref BulletColourIndex);
        rend.material = materialsArray[BulletColourIndex];
        myGameObject.tag = tagArray[BulletColourIndex];
    }

    public void CycleToPreviousColour(GameObject myGameObject)
    {
        Renderer rend = myGameObject.GetComponent<Renderer>();
        BulletColourIndex--;
        CheckArrayIndexNotInvalid(ref BulletColourIndex);
        rend.material = materialsArray[BulletColourIndex];
        myGameObject.tag = tagArray[BulletColourIndex];
    }

    protected void CheckArrayIndexNotInvalid(ref int value)
    {
        if (value < 0)
        {
            value = materialsArray.Length-1;
        }
        if (value > materialsArray.Length-1)
        {
            value = 0;
        }
    }

}
