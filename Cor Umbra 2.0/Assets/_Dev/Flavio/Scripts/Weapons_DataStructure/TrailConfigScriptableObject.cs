using UnityEngine;

[CreateAssetMenu(fileName = "Trail Config", menuName = "Weapons/Weapon Trail Config", order = 4)]
public class TrailConfigScriptableObject : ScriptableObject
{
    public Material material;
    public AnimationCurve widthCurve;
    public float duration = 0.5f;
    public float minVertexDistance = 0.1f;
    public Gradient color;

    public float MissDistance = 100f;
    public float SimulationSpeed = 100f;
}
