using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CandidateSurface : MonoBehaviour
{
    private MRUKAnchor anchor;
    private float surfaceHitboxHeight = .02f;

    private bool leftHandPlaced = false;
    private bool rightHandPlaced = false;

    private HashSet<Collider> colliders = new HashSet<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        anchor = GetComponent<MRUKAnchor>();
        Rect? anchorPlane = anchor.PlaneRect;

        // Create a trigger collider that is the same 2d dimensions as the anchor with a given hitbox height
        this.AddComponent<BoxCollider>();
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.center = new Vector3(anchorPlane.Value.center.x, anchorPlane.Value.center.y, surfaceHitboxHeight/2);
        boxCollider.size = new Vector3(anchorPlane.Value.size.x, anchorPlane.Value.size.y, surfaceHitboxHeight);
        boxCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fingertip" && other.GetComponentInParent<OVRHand>().IsTracked)
        {
            colliders.Add(other);

            // Check if left hand and right hand are placed
            foreach (Collider collider in colliders)
            {
                if (collider.GetComponentInParent<OVRSkeleton>().GetSkeletonType() == OVRSkeleton.SkeletonType.HandLeft)
                {
                    leftHandPlaced = true;
                }

                else if (collider.GetComponentInParent<OVRSkeleton>().GetSkeletonType() == OVRSkeleton.SkeletonType.HandRight)
                {
                    rightHandPlaced = true;
                }
            }
            
            if (leftHandPlaced && rightHandPlaced)
            {
                // Show confirmation screen
                Instants.surfaceSelection.SelectSurface(anchor);
            }

            leftHandPlaced = false;
            rightHandPlaced = false;

            // Tell controller this surface was selected
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fingertip")
        {
            colliders.Remove(other);
        }
    }
}
