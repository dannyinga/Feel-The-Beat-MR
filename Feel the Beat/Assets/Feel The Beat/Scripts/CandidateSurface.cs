using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class CandidateSurface : MonoBehaviour
{
    // Anchor that the surface is attached to
    private MRUKAnchor anchor;

    // Surface hitbox reaches +- surfaceHitboxHeight/2 above and below it and across entire surface plane
    private float surfaceHitboxHeight = .02f;

    // Flags that keep track of each hand and if it is placed
    private bool leftHandPlaced = false;
    private bool rightHandPlaced = false;

    // Set of colliders currently on the candidate surface
    private HashSet<Collider> fingertipColliders = new HashSet<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        // Get this surface's MRUKAnchor and it's PlaneRect. This anchor is guaranteed to have a PlaneRect because this script is only attached to Floors and Tables
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

    // Select surface if both hands are placed on it
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fingertip" && other.GetComponentInParent<OVRHand>().IsTracked)
        {
            fingertipColliders.Add(other);

            // Check if left hand and right hand are placed
            foreach (Collider collider in fingertipColliders)
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
            
            // Check if both hands are placed
            if (leftHandPlaced && rightHandPlaced)
            {
                // Show confirmation screen

                // Tell main controller to set the anchor
                MainController.SetDrumSurfaceAnchor(anchor);
            }

            // Reset booleans
            leftHandPlaced = false;
            rightHandPlaced = false;
        }
    }

    // Remove the fingertip from the list on exit
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fingertip")
        {
            fingertipColliders.Remove(other);
        }
    }
}
