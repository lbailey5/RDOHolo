using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using DigitalRuby.RainMaker;

public class TouchTestScript : MonoBehaviour, IInputClickHandler{

    public GameObject thing;
    private RainScript script;
    private bool rainOn = true;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("clicky", this);
        if (rainOn)
        {
            script.RainIntensity = 0;
            script.EnableWind = false;
            rainOn = false;
        }
        else
        {
            script.RainIntensity = 1;
            script.EnableWind = true;
            rainOn = true;
        }

    }

    // Use this for initialization
    void Start () {
        script = thing.GetComponent<RainScript>();
        if (script)
        {
            Debug.Log("Got the thing", this);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
