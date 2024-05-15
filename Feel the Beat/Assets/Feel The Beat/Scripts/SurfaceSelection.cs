using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SurfaceSelection : MonoBehaviour
{
    private List<MRUKAnchor> candidateSurfaceAnchors;

    void Start()
    {
        Instants.surfaceSelection = this;
    }
    public void SetupWithAnchors(List<MRUKAnchor> anchors)
    {
        candidateSurfaceAnchors = new List<MRUKAnchor>();
        foreach (MRUKAnchor anchor in anchors)
        {
            if (anchor.AnchorLabels.Contains("TABLE") || anchor.AnchorLabels.Contains("FLOOR"))
            {
                candidateSurfaceAnchors.Add(anchor);
                anchor.AddComponent<CandidateSurface>();
            }
        }
    }

    // Send selected surface to main controller
    // Disable all meshes except selected anchor mesh
    // Disable all candidate surface scripts and colliders
    public void SelectSurface(MRUKAnchor selectedAnchor)
    {
        foreach(MRUKAnchor anchor in candidateSurfaceAnchors)
        {
            //if (anchor != selectedAnchor)
            {
                anchor.GetComponentInChildren<MeshRenderer>().enabled = false;
            }
            anchor.GetComponent<CandidateSurface>().enabled = false;
            anchor.GetComponent<BoxCollider>().enabled = false;
        }
        Instants.mainController.SetDrumSurfaceAnchorAndProceed(selectedAnchor);
    }
}
