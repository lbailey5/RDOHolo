using HoloToolkit.Unity;
using UnityEngine;

public class Speech : MonoBehaviour
{
    public SpatialUnderstandingCustomMesh SpatialUnderstandingMesh;

    public void ToggleMesh()
    {
        SpatialUnderstandingMesh.DrawProcessedMesh = !SpatialUnderstandingMesh.DrawProcessedMesh;
    }

    public CommandMenu menu;

    public void ToggleMenu()
    {
        menu.ToggleMenu();
    }

    public HologramManager holo;
    public void CreateCube()
    {
        holo.Create3DCube();
    }

    public void CreateSphere()
    {
        holo.Create3DSphere();
    }
}