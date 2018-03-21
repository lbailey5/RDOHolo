using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    static int sphereCount;
    int sphereNumber;

    void Start()
    {
        this.gameObject.GetComponent<HandDraggable>().StartedDragging += OnStartedDragging;
        this.gameObject.GetComponent<HandDraggable>().StoppedDragging += OnStoppedDragging;

        this.sphereNumber = ++sphereCount;
    }
    void OnStartedDragging()
    {
        WorldAnchorManager.Instance.RemoveAnchor(this.gameObject);

        var rigidBody = this.gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }
    void OnStoppedDragging()
    {
        Destroy(this.gameObject.GetComponent<Rigidbody>());
        WorldAnchorManager.Instance.AttachAnchor(this.gameObject, this.sphereNumber.ToString());
    }
    void OnCollisionEnter(Collision collision)
    {
        this.gameObject.GetComponent<HandDraggable>().StopDragging();
    }
}