using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainController : MonoBehaviour
{
    /** Set in Inspector */
    [SerializeField] private MRUK mruk;
    [SerializeField] private GameObject drumPadPrefab;
    [SerializeField] private SurfaceSelection surfaceSelection;

    // A static reference to this script so we can use it in public static methods
    private static MainController instance;

    private DrumPad drumPad;
    private MRUKAnchor drumSurfaceAnchor;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Initial Scene Setup
    public void SceneSetup()
    {
        StartCoroutine(SceneSetupCoroutine());
    }

    // Initial Scene Setup. First requires user to select a surface, then creates a drum pad on selected surface
    public IEnumerator SceneSetupCoroutine()
    {
        surfaceSelection.SetupWithAnchors(mruk.GetCurrentRoom().Anchors);
        while (drumSurfaceAnchor == null)
        {
            yield return null;
        }

        CreateDrumPadOnDrumSurface();
    }

    // Set drum surface anchor and tell surface selection script that this anchor was selected
    public static void SetDrumSurfaceAnchor(MRUKAnchor anchor)
    {
        // Tell surface selection script to select this surface
        instance.surfaceSelection.SelectSurface(anchor);
        instance.drumSurfaceAnchor = anchor;
        
    }

    // Creates a drumpad at the drumSurfaceAnchor's position. This will appear as a flat set of buttons on the user selected surface
    private void CreateDrumPadOnDrumSurface()
    {
        GameObject pad = Instantiate(drumPadPrefab, drumSurfaceAnchor.transform.position + new Vector3(0f, 0.0f, 0f), Quaternion.identity, drumSurfaceAnchor.transform);
        drumPad = pad.GetComponent<DrumPad>();
    }
}
