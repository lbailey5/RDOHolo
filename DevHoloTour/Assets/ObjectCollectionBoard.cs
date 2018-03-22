﻿using System;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections.Generic;

public class ObjectCollectionBoard : MonoBehaviour
{

    public TextMesh MainDisplay;
    public TextMesh SubDisplay;
  //  public TextMesh ActiveHolos;


    public bool HideText = false;

    private int activeHolograms = HologramManager.ActiveHolograms.Count;

    List<string> voiceCommands = new List<string>()
    {
        "Toggle Mesh",
        "Toggle Board",
        "Create Cube",
        "Create Sphere",
        "New Command"
    };
    protected string fillTextField(TextMesh txt, string val)
    {
        float rowLimit = 0.20f; //find the sweet spot    

        string[] parts = val.Split(' ');
        string tmp = "";
        txt.text = "";
        for (int i = 0; i < parts.Length; i++)
        {
            tmp = txt.text;

            txt.text += parts[i] + " ";
            if (txt.GetComponent<Renderer>().bounds.extents.x > rowLimit)
            {
                tmp += Environment.NewLine;
                tmp += parts[i] + " ";
                txt.text = tmp;
            }

        }
        return txt.text;
    }

    public string MainDisplayText
    {
        get
        {
            return "Commands: " + "Toggle Mesh " + "Toggle Board  " + "Create Cube " + "Create Sphere"; ;
        }

    }

    public string SubDisplayText
    {
        get
        {
            return "Active Holograms: " + activeHolograms;
        }

    }

    public string ActiveHolosText
    {
        get
        {
            return "Active Holograms: " + activeHolograms;
        }

    }

    // Use this for initialization
    void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
        UpdateBoard();
    }

    public void ToggleBoard()
    {
        HideText = !HideText;
    }



    private void UpdateBoard()
    {
        if (MainDisplay == null || HideText)
        {
            return;
        }
        //  var mainText = "Here is a really long string to test the word wrap property for the main text field";
        var mainText = "Commands:";
        MainDisplay.text = fillTextField(MainDisplay, mainText);
        var subText = "";
        foreach( var s in voiceCommands){
            subText += s + "\n";  
        }
        SubDisplay.text = subText;
        //if( ActiveHolos != null)
        //{
        //    ActiveHolos.text = "Active Holograms: " + activeHolograms;
        //}
       

        MainDisplay.color = Color.blue;
        SubDisplay.color = Color.green;
      //  ActiveHolos.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
      //  UpdateBoard();

    }

}
