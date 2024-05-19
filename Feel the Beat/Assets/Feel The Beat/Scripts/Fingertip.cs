using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fingertip : MonoBehaviour
{
    #region Constants
    private static float xZMultiplier = .018f;
    private static float yMultiplier = 0.015f;

    private static float colliderXOffsetLeft = .012f;
    private static float colliderXOffsetRight = -colliderXOffsetLeft;
    #endregion

    #region Static Variables

    #endregion

    #region Public Variables
    #endregion

    #region Private Variables

    private OVRBone bone;
    private OVRHand hand;
    private OVRSkeleton.SkeletonType handType;

    private CapsuleCollider coll;
    private Rigidbody rb;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CalibrateFingertip(OVRBone currBone, OVRHand currHand, float currFingerMultiplier)
    {
        bone = currBone;
        hand = currHand;
        handType = hand.GetComponent<OVRSkeleton>().GetSkeletonType();

        float colliderXOffset = IsOnLeftHand() ? colliderXOffsetLeft : colliderXOffsetRight;

        // Modify properties of collider to reflect the finger's dimensions
        float x = hand.HandScale * xZMultiplier * currFingerMultiplier;
        float y = hand.HandScale * yMultiplier * currFingerMultiplier;
        float z = hand.HandScale * xZMultiplier * currFingerMultiplier;
        
        Vector3 localScale = new Vector3(x, y, z);
        Vector3 offset = new Vector3(hand.HandScale * colliderXOffset * currFingerMultiplier, 0f, 0f);

        transform.localScale = localScale;
        transform.localPosition = offset;
    }

    public bool IsOnLeftHand()
    {
        return handType == OVRSkeleton.SkeletonType.HandLeft;
;    }

    public bool IsOnRightHand()
    {
        return handType == OVRSkeleton.SkeletonType.HandRight;
    }
}
