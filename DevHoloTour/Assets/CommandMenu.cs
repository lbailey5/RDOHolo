using System;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections.Generic;

public class CommandMenu : MonoBehaviour
{

    public TextMesh MainDisplay;
    public TextMesh SubDisplay;
    public TextMesh HoloCountDisplay;
    public GameObject Background;
    public Display MenuDisplay;
    public bool HideText = false;

    public int ActiveHoloCount = 0;

    List<string> voiceCommands = new List<string>()
    {
        "Toggle Mesh",
        "Toggle Board",
        "Create Cube",
        "Create Sphere",
        "New Command",
        "Where is...?",
        "What does [name] do?"
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

    // Use this for initialization
    void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
        UpdateBoard();
    }

    public void ToggleMenu()
    {
        HideText = !HideText;
        if(MainDisplay.text == "" || SubDisplay.text == "")
        {
            UpdateBoard();
            Background.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            MainDisplay.text = "";
            SubDisplay.text = "";
            HoloCountDisplay.text = "";
            Background.GetComponent<Renderer>().enabled = false;
        }
    }



    public void UpdateBoard(int activeholos = 0)
    {
        ActiveHoloCount = activeholos;

        if (MainDisplay == null || HideText)
        {
            return;
        }
        //  var mainText = "Here is a really long string to test the word wrap property for the main text field";
        var mainText = "Commands:";
        MainDisplay.text = fillTextField(MainDisplay, mainText);
        var subText = "";
        foreach (var s in voiceCommands)
        {
            subText += s + "\n";
        }
        SubDisplay.text = subText;


            HoloCountDisplay.text = "Spawned Holograms: " + ActiveHoloCount;


        MainDisplay.color = Color.blue;
        SubDisplay.color = Color.green;
        HoloCountDisplay.color = Color.white;
        
    }

    // Update is called once per frame
    void Update()
    {
        //  UpdateBoard();

    }

}
