using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private InputAction mouseClick;
    [SerializeField] private float mouseDragPhysicsSpeed = 4f;
    [SerializeField] private float mouseDragSpeed = 0.1f;
    
    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;

    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && (hit.collider.gameObject.CompareTag("Human")))
            {
                hit.collider.gameObject.GetComponent<NavMesh>().isCatched = true;
                hit.collider.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        float firstDistance = Vector3.Distance(clickedObject.transform.position,mainCamera.transform.position);
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
        while (mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (rb != null)
            {
                Vector3 direction = ray.GetPoint(firstDistance) - clickedObject.transform.position;
                rb.linearVelocity = direction * mouseDragPhysicsSpeed; 
                yield return waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(firstDistance), ref velocity, mouseDragSpeed);
                yield return null;
            }
        }
    }
}
