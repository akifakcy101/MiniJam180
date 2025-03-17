using System.Collections;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [Header("Singleton")]
    public static ResourceManager instance { get; private set; }

    [Header("Current Resources")]
    public int food;
    public int water;
    public int wood;

    [Header("Maximum Resource Amount")]
    [SerializeField] private int maxFood;
    [SerializeField] private int maxWater;
    [SerializeField] private int maxWood;

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
    }

    private void Start()
    {
        ClampTheResources();
    }

    private void ClampTheResources()
    {
        food = Mathf.Clamp(food,0,maxFood);
        water = Mathf.Clamp(water,0,maxWater);
        wood = Mathf.Clamp(wood,0,maxWood);
    }

    public void AddResource(int amount,ProductTypes productType)
    {
        switch(productType)
        {
            case ProductTypes.Food:
                food += amount;
                break;

            case ProductTypes.Water:
                water += amount;
                break;

            case ProductTypes.Wood:
                wood += amount;
                break;
        } 
    }

    public void DeleteResource(int amount,ProductTypes productType)
    {
        switch (productType)
        {
            case ProductTypes.Food:
                food -= amount;
                break;

            case ProductTypes.Water:
                water -= amount;
                break;

            case ProductTypes.Wood:
                wood -= amount;
                break;
        }
    }
}
