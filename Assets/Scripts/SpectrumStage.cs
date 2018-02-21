using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumStage : MonoBehaviour {

    private Camera camera;
    private Spectrum spectrum;

    private bool elementsLookAtCamera;

	// Use this for initialization
	void Start () {
        camera = Camera.main;
        spectrum = gameObject.GetComponentInChildren<Spectrum>();
        elementsLookAtCamera = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (elementsLookAtCamera) {
            
        }

	}

    public void setElementsLookAtCamera(bool look) {
        elementsLookAtCamera = look;
    }
}
