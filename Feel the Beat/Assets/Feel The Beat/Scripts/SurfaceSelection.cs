using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SurfaceSelection : MonoBehaviour
{
    // List of all possible anchors in room for the drum surface
    private List<MRUKAnchor> candidateSurfaceAnchors;

    void Start()
    {
        
    }

    // Initial Setup. Get all possible surface anchors and add a CandidateSurface script to each anchor
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

    // Disable all meshes except selected anchor mesh
    // Disable all candidate surface scripts and colliders
    public void EndSetup()
    {
        foreach(MRUKAnchor anchor in candidateSurfaceAnchors)
        {
            anchor.GetComponentInChildren<MeshRenderer>().enabled = false;
            anchor.GetComponent<CandidateSurface>().enabled = false;
            anchor.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
