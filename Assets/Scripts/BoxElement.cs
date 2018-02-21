using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxElement : MonoBehaviour {

    //// Use this for initialization
    //void Start()
    //{
        
    //}

    public void setHeight(float newHeight) {
        float x = gameObject.transform.localScale.x;
        float z = gameObject.transform.localScale.z;
        gameObject.transform.localScale = new Vector3(x, newHeight, z);
    }

    public void setTexture() { // Material?

    }

    public void setTint() { // Color?

    }

	//// Update is called once per frame
	//void Update () {
		
	//}
}
