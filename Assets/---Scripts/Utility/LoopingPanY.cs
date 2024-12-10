using UnityEngine;

public class LoopingPanY : MonoBehaviour
{

    private MeshRenderer background;
    [field: SerializeField] public float PlanetSpeed { get; set; }

    void Awake()
    {
        background = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        background.material.mainTextureOffset -= new Vector2(0, PlanetSpeed * Time.deltaTime);
    }
}