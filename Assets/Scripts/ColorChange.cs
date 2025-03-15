using Unity.VisualScripting;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Renderer rend;
    [SerializeField] private Material transparentGreen;
    [SerializeField] private Material transparentRed;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material= transparentGreen;
    }

    void OnTriggerEnter(Collider other)
    {
        rend.material = transparentRed;
    }

    void OnTriggerExit(Collider other)
    {
        rend.material = transparentGreen;
    }
}
