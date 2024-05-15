using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private MRUK mruk;
    [SerializeField] private GameObject drumPadPrefab;

    private DrumPad drumPad;
    private MRUKAnchor drumSurfaceAnchor;


    // Start is called before the first frame update
    void Start()
    {
        Instants.mainController = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public MRUKAnchor GetDrumSurfaceAnchor()
    {
        return drumSurfaceAnchor;
    }

    public void SetDrumSurfaceAnchorAndProceed(MRUKAnchor anchor)
    {
        drumSurfaceAnchor = anchor;
        GameObject pad = Instantiate(drumPadPrefab, anchor.transform.position + new Vector3(0f, 0.02f, 0f), Quaternion.identity, anchor.transform);
        drumPad = pad.GetComponent<DrumPad>();
    }

    public void SceneSetup()
    {
        Instants.surfaceSelection.SetupWithAnchors(mruk.GetCurrentRoom().Anchors);
    }
}
