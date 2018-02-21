using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour {
    private static int DEFAULT_COUNT = 100;
    private static float DEFAULT_LERP_DELTA = 7f;
    private static GameObject boxElement;

    private List<GameObject> elements;

    private CoreSpectrum coreSpectrum;

	// Use this for initialization
	void Start () {
        boxElement = Resources.Load("Prefabs/BoxElement") as GameObject;

        elements = new List<GameObject>();
        for(int i = 0; i < DEFAULT_COUNT; i++)
        {
            GameObject element = Instantiate(boxElement);
            element.transform.parent = gameObject.transform;
            elements.Add(element);

            //temp
            element.transform.localPosition = new Vector3(i, 0, 0);
        }

        coreSpectrum = new CoreSpectrum();

	}
	
	// Update is called once per frame
	void Update () {
        float[] spectrumData = coreSpectrum.GetSpectrumData();

        for(int i = 0; i < elements.Count; i++)
        {
            GameObject element = elements[i];
            Vector3 newVal = new Vector3(1f, spectrumData[i] * 8000f, 1f);
            Vector3 oldVal = element.transform.localScale;

            element.transform.localScale = Vector3.Lerp(oldVal, newVal, Time.deltaTime * DEFAULT_LERP_DELTA);
        }
        
        coreSpectrum.DataRead();

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
