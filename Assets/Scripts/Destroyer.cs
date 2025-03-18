using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject Manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Human"))
        {
            Manager.GetComponent<HumanSpawner>().humanCount--;
            Destroy(other.gameObject);
        }
        
    }
}
