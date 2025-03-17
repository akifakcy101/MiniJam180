using System.Collections;
using UnityEngine;

public class ProductBuilding : MonoBehaviour
{
    [Header("Product Building Data")]
    public ProductBuildingScriptableObject buildingData;

    [Header("Work Condition Check")]
    private bool isWorking = true;


    

    

    

    private IEnumerator AddResourceAccordingToTime()
    {
        if(!isWorking)
        {
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(buildingData.productiontDuration);
            ResourceManager.instance.AddResource(buildingData.productionAmount, buildingData.productType);
            StartCoroutine(AddResourceAccordingToTime());
        }
    }

}
