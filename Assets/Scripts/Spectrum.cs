using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour {
    private static readonly int DEFAULT_COUNT = 100; 
    private static GameObject boxElement;

    private List<GameObject> elements;

	// Use this for initialization
	void Start () {
        boxElement = Resources.Load("Prefabs/BoxElement") as GameObject;

        elements = new List<GameObject>();
        for(int i = 0; i < DEFAULT_COUNT; i++)
        {
            GameObject element = Instantiate(boxElement);
            elements.Add(element);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // All of these elements need to be repositioned and handled automatically. Add listeners?
    public void SetElementCount(int count)
    {
        if(elements.Count < count)
        {
            while(elements.Count != count)
            {
                AddElement();
            }
        } else if(elements.Count > count)
        {
            while(elements.Count != count)
            {
                RemoveElement();
            }
        }
    }

    public void RemoveElement()
    {
        GameObject element = elements[elements.Count - 1];
        elements.RemoveAt(elements.Count - 1);
        Destroy(element);
    }

    public void AddElement()
    {
        GameObject element = GameObject.Instantiate(boxElement);
        elements.Add(element);
    }
}
