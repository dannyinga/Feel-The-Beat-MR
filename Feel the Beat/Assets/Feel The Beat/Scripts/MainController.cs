using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainController : MonoBehaviour
{
    #region Constants
    #endregion

    #region Static Variables
    
    // A reference to the script so we can use it in public static methods
    private static MainController instance;

    #endregion

    #region Public Variables
    #endregion

    #region Private Variables
    
    [SerializeField] private MRUK mruk;
    [SerializeField] private GameObject drumPadPrefab;
    
    private SurfaceSelection surfaceSelection;
    private DrumPad drumPad;
    private MRUKAnchor drumSurfaceAnchor;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        surfaceSelection = GetComponentInChildren<SurfaceSelection>();
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

        surfaceSelection.EndSetup();

        CreateDrumPadOnDrumSurface();
    }

    // Set drum surface anchor
    public static void SetDrumSurfaceAnchor(MRUKAnchor anchor)
    {
        instance.drumSurfaceAnchor = anchor;
        
    }

    // Creates a drumpad at the drumSurfaceAnchor's position. This will appear as a flat set of buttons on the user selected surface
    private void CreateDrumPadOnDrumSurface()
    {
        GameObject pad = Instantiate(drumPadPrefab, drumSurfaceAnchor.transform.position + new Vector3(0f, 0.0f, 0f), Quaternion.identity, drumSurfaceAnchor.transform);
        drumPad = pad.GetComponent<DrumPad>();
    }
}
