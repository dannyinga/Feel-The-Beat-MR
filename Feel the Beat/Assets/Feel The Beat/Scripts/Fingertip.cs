using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fingertip : MonoBehaviour
{
    /** Static Variables */
    private static float colliderRadiusMultiplier = .008f;
    private static float colliderHeightMultiplier = 0.003f;

    /** Variables */
    private OVRBone bone;
    private OVRHand hand;
    private CapsuleCollider capsuleCollider;
    private Rigidbody rb;
    private Vector3 colliderOffset = new Vector3(.01f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateFingertipHitbox(OVRBone currBone, OVRHand currHand, float currFingerMultiplier)
    {
        bone = currBone;
        hand = currHand;

        // If right hand, reverse offset
        if (hand.GetComponent<OVRSkeleton>().GetSkeletonType() == OVRSkeleton.SkeletonType.HandRight)
        {
            colliderOffset *= -1;
        }

        // Add a capsule collider to this bone and change the tag
        capsuleCollider = this.AddComponent<CapsuleCollider>();
        capsuleCollider.tag = "Fingertip";

        // Modify properties of collider to reflect the finger's dimensions
        capsuleCollider.radius = hand.HandScale * colliderRadiusMultiplier * currFingerMultiplier;
        capsuleCollider.height = hand.HandScale * colliderHeightMultiplier * currFingerMultiplier;
        capsuleCollider.center = hand.HandScale * colliderOffset * currFingerMultiplier;

        // Add a rigidbody to enable trigger activations
        rb = this.AddComponent<Rigidbody>();
        rb.isKinematic = true;
    }
}
