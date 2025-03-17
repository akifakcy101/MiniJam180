using System.Collections;
using UnityEngine;

public class BuilderManager : MonoBehaviour
{
    [Header("Singleton")]
    public static BuilderManager instance { get; private set; }


    [Header("Object Prefab")]
    public GameObject buildPrefab;
    [SerializeField] public GameObject buildInstantiate;

    [Header("Object Prefab Position")]
    private Vector3 previewPos;
    private Ray rayToPlane;
    private RaycastHit hit;

    [Header("Placable Materials")]
    public Material placableMaterial;
    public Material unPlacableMaterial;

    [Header("Preview Check")]
    public bool previewCheck;
    public bool isInPreview;

    [Header("Placable Check")]
    public bool isPlacable = true;
    public LayerMask buildingLayerMask;
    public LayerMask previewBuildingMask;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        buildInstantiate = null;

    }

    private void Start()
    {
        PlayerInputManager.instance.playerInput.Player.LeftMouse.performed += i => { isInPreview = false; SetTheBuilding(); };
    }

    private void OnDisable()
    {
        PlayerInputManager.instance.playerInput.Player.LeftMouse.performed -= i =>  SetTheBuilding();
    }

    public void SetBuildPrefab(GameObject buildPrefab)
    {
        if(buildInstantiate != null)
        {
            Destroy(buildInstantiate);
        }
        isPlacable = true;
        this.buildPrefab = buildPrefab;
        isInPreview = true;
        buildInstantiate = Instantiate(buildPrefab);
        buildInstantiate.AddComponent<PreviewCollisionHandler>();
    }


    private void SetTheBuilding()
    {
        if(!isPlacable)
        {
            Debug.Log("This Node Ýs Full");
        }
        
        else if(buildInstantiate != null)
        {
            var building = Instantiate(buildInstantiate, previewPos, Quaternion.identity);
            Destroy(buildInstantiate);
            Destroy(building.GetComponent<PreviewCollisionHandler>());
            Destroy(building.GetComponent<Rigidbody>());
            building.tag = "Building";
            isInPreview = false;
        }
        
    }


    

    

    private void CheckForRayHitPosition()
    {
        rayToPlane = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayToPlane, out hit,500f))
        {
            if (hit.collider.CompareTag("Buildable"))
            {
                previewPos = hit.point;
                if (buildInstantiate != null)
                {
                    Rigidbody rb = buildInstantiate.GetComponent<Rigidbody>();
                    rb.MovePosition(previewPos);
                }
            }
        }
    }

    private void Update()
    {
        CheckForRayHitPosition();
        Debug.Log(isPlacable);
    }

    
}
