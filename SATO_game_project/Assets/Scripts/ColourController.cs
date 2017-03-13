using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourController : MonoBehaviour {

    public Material redMaterial;
    public Material yellowMaterial;
    public Material greenMaterial;
	
    public void AssignColour(Renderer rend, Material colourMaterial)
    {
        rend.material = colourMaterial;
    }

    public void AssignRandomColour(Renderer rend)
    {
        //TODO Make the random colour generator
    }

}
