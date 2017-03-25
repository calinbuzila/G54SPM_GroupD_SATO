using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourController : MonoBehaviour {

	public Material[] materialsArray = new Material[0];
    // Default index is 0 for red.
    public const int DefaultBulletColourIndex = 0;
    public Text colourText;

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

        tagArray[0] = "Red";
        tagArray[1] = "Yellow";
        tagArray[2] = "Green";
        tagArray[3] = "Blue";
	}

    public void AssignBulletColour(GameObject myGameObject, int ColourArrayIndex = DefaultBulletColourIndex)
    {
        BulletColourIndex = ColourArrayIndex;
        Renderer rend = myGameObject.GetComponent<Renderer>();
        CheckArrayIndexNotInvalid(ref BulletColourIndex);
        rend.material = materialsArray[BulletColourIndex];
        UpdateColourDisplay();
        myGameObject.tag = tagArray[BulletColourIndex];
    }

    //TODO split method for bullet and enemy into two methods.
    public void AssignRandomColour(GameObject myGameObject, bool isBullet = false)
    {
        Renderer rend = myGameObject.GetComponent<Renderer>();
		randomMaterialSelector = Random.Range(0, materialsArray.Length);
 		rend.material = materialsArray [randomMaterialSelector];
        if (isBullet)
        {
            BulletColourIndex = randomMaterialSelector;
            CheckArrayIndexNotInvalid(ref BulletColourIndex);
            UpdateColourDisplay();
            myGameObject.tag = tagArray[BulletColourIndex];
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
        UpdateColourDisplay();
        myGameObject.tag = tagArray[BulletColourIndex];
    }

    public void CycleToPreviousColour(GameObject myGameObject)
    {
        Renderer rend = myGameObject.GetComponent<Renderer>();
        BulletColourIndex--;
        CheckArrayIndexNotInvalid(ref BulletColourIndex);
        rend.material = materialsArray[BulletColourIndex];
        UpdateColourDisplay();
        myGameObject.tag = tagArray[BulletColourIndex];
    }

    protected void UpdateColourDisplay()
    {
        colourText.text = "<color=white>Colour:</color> " + tagArray[BulletColourIndex];
        colourText.color = GetBulletColour();
    }

    public Color GetBulletColour()
    {
        return materialsArray[BulletColourIndex].color;
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
