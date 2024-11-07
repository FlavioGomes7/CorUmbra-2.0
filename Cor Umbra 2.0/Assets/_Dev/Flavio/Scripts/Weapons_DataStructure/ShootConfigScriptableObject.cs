using UnityEngine;

[CreateAssetMenu(fileName = "Shoot Config", menuName = "Weapons/Shoot Configuration", order = 2)] 
public class ShootConfigScriptableObject : ScriptableObject
{
    public LayerMask hitMask;
    public Vector3 Spread = new Vector3();
    public float fireRate;
}
