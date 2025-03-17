using UnityEngine;

public class PreviewCollisionHandler : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.CompareTag("Building"))
            {
                BuilderManager.instance.isPlacable = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Building"))
        {
            BuilderManager.instance.isPlacable = true;
        }
    }
}
