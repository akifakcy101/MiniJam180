using UnityEngine;

public enum ProductTypes  {Food , Water , Wood};

[CreateAssetMenu(fileName = "ProductBuilding",menuName = "ScriptableObjects/BuildingTypes/Product Building")]
public class ProductBuildingScriptableObject : BaseBuildingScriptableObject
{
     public ProductTypes productType;
     public int productionAmount;
    public int productiontDuration;
}

