using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrumHand : MonoBehaviour
{
    private OVRHand hand;
    private OVRSkeleton skeleton;
    private List<Collider> fingertipColliders;
    private float colliderRadiusMultiplier = .009f;
    private float colliderHeightMultiplier = 0.015f;
    private float colliderXOffset = .009f;

    private static List<OVRSkeleton.BoneId> boneIds =
        new List<OVRSkeleton.BoneId>() { OVRSkeleton.BoneId.Hand_IndexTip,
                                         OVRSkeleton.BoneId.Hand_MiddleTip,
                                         OVRSkeleton.BoneId.Hand_RingTip,
                                         OVRSkeleton.BoneId.Hand_PinkyTip};

    // Start is called before the first frame update
    IEnumerator Start()
    {
        hand = GetComponent<OVRHand>();
        skeleton = GetComponent<OVRSkeleton>();
        while (skeleton.Bones.Count == 0)
        {
            yield return null;
        }

        fingertipColliders = new List<Collider>();
        foreach (var bone in skeleton.Bones)
        {
            if (boneIds.Contains(bone.Id))
            {
                bone.Transform.AddComponent<CapsuleCollider>();
                CapsuleCollider currentCollider = bone.Transform.GetComponent<CapsuleCollider>();
                currentCollider.radius = hand.HandScale * colliderRadiusMultiplier;
                currentCollider.height = hand.HandScale * colliderHeightMultiplier;
                currentCollider.center = new Vector3(-hand.HandScale * colliderXOffset, 0f, 0f);
                currentCollider.tag = "Fingertip";
                fingertipColliders.Add(currentCollider);
                bone.Transform.AddComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
