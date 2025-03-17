using UnityEngine;
using UnityEngine.UI;

public class SetTheBuildPrefabWithButton : MonoBehaviour
{
    private Button buildingSelectButton;
    [SerializeField] private GameObject buildingPrefab;

    private void Awake()
    {
        buildingSelectButton = GetComponent<Button>();
    }
    private void Start()
    {
        buildingSelectButton.onClick.AddListener(() => BuilderManager.instance.SetBuildPrefab(buildingPrefab));
    }
}
