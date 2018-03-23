using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;

public class PlaceCowboy : MonoBehaviour {

    public GameObject Cowboy;

    public Vector3 CowboySize;

    public List<GameObject> ActiveHolograms = new List<GameObject>();

    public void CreateCowboy(Vector3 positionCenter, Quaternion rotation)
    {
        // Stay center in the square but move down to the ground
        var position = positionCenter - new Vector3(0, CowboySize.y * .5f, 0);

        GameObject newObject = Instantiate(Cowboy, position, rotation);

        if (newObject != null)
        {
            // Set the parent of the new object the GameObject it was placed on
            newObject.transform.parent = gameObject.transform;

          //  newObject.transform.localScale = RescaleToDesiredSizeProportional(Cowboy, CowboySize);

            newObject.AddComponent<Cowboy>();
            ActiveHolograms.Add(newObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
