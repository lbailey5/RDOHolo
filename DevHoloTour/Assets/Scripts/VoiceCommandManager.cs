using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System.Linq;

public class VoiceCommandManager : MonoBehaviour, IDictationHandler {

    public TextMesh TextOutput;
    public GameObject GuidingVoice;
    public string LastVoiceCommand;
    public GameObject[] ObjectsToTrack;

    private GuidingVoiceBehavior guidingVoiceBehavior;
    private PointToObjectBehavior pointToObjectBehavior;
    private bool isRecording = false;
    //private bool hasProcessed = false;

    public void BeginListening()
    {
        StartCoroutine(DelayedRecord());
    }

    public void StopListening()
    {
        if (isRecording)
        {
            isRecording = false;
            TextOutput.color = Color.white;
            StartCoroutine(DictationInputManager.StopRecording());
        }
    }

    public IEnumerator DelayedRecord()
    {
        yield return new WaitForSeconds(.1f);

        if (!isRecording)
        {
            isRecording = true;
            TextOutput.color = Color.green;
            TextOutput.text = "...";
            StartCoroutine(DictationInputManager.StartRecording(
                this.gameObject));
        }
    }

    public void TestLog()
    {
        Debug.Log("Testing");
    }

    public void ProcessInput()
    {
        Debug.Log("ProcessingInput: " + LastVoiceCommand);
        StopListening();
        if(guidingVoiceBehavior == null)
        {
            TextOutput.text = "Can't respond to Voice Command";
            return;
        }

        string[] words = LastVoiceCommand.Split(null);
        
        //clean up words
        for (int i = 0; i < words.Length; i++)
        {
            //remove chars?
            words[i] = words[i].Replace('.', ' ');
            words[i] = words[i].ToUpper();
            words[i] = words[i].Trim();
        }

        if (words.Length > 0)
        {
            if (words.Contains("WHERE"))
            {
                if (words.Contains("SPHERE"))
                {
                    LocateObject(ObjectsToTrack.Where(go => go.name.ToUpper().Contains("SPHERE")).FirstOrDefault());
                }
                else if (words.Contains("CUBE"))
                {
                    LocateObject(ObjectsToTrack.Where(go => go.name.ToUpper().Contains("CUBE")).FirstOrDefault());
                }
                else
                {
                    Apologize();
                }
            }
            else
            {
                Apologize();
            }
        }
        else
        {
            Apologize();
        }

    }

    public void LocateObject(GameObject go)
    {
        Debug.Log("Locating " + go.name);
        GuidingVoice.transform.position = go.transform.position;
        guidingVoiceBehavior.SayText("Over here!");
        pointToObjectBehavior.ObjectToPointTo = go;
        pointToObjectBehavior.StartTracking();
    }

    private void Apologize()
    {
        GuidingVoice.transform.position = this.gameObject.transform.position + new Vector3(0, .6f, 0) ;
        guidingVoiceBehavior.SayText("Fuck if I know");
    }

    public void OnDictationComplete(DictationEventData eventData)
    {
        TextOutput.text = eventData.DictationResult;
        TextOutput.color = Color.blue;
        ProcessInput();
    }

    public void OnDictationError(DictationEventData eventData)
    {
        TextOutput.text = eventData.DictationResult;
        TextOutput.color = Color.red;
        Apologize();
    }

    public void OnDictationHypothesis(DictationEventData eventData)
    {
        TextOutput.text = eventData.DictationResult;
        TextOutput.color = Color.yellow;
    }

    public void OnDictationResult(DictationEventData eventData)
    {
        LastVoiceCommand = eventData.DictationResult;
        TextOutput.text = eventData.DictationResult;
        TextOutput.color = Color.white;
    }
    
    void Awake () {
        guidingVoiceBehavior = GuidingVoice.GetComponent<GuidingVoiceBehavior>();
        pointToObjectBehavior = this.gameObject.transform.parent.GetComponentInChildren<PointToObjectBehavior>();
        if(pointToObjectBehavior == null)
        {
            Debug.LogError("no pointToObjectBehavior found");
        }
        else
        {
            Debug.Log("found pointToObjectBehavior");
        }
    }
	
}
