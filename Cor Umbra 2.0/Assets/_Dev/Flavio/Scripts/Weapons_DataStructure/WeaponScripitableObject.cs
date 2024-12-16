using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon", order = 0)]
public class WeaponScripitableObject : ScriptableObject
{
    public WeaponType type;
    public string name;
    public GameObject modelPrefab;
    public Vector3 spawnPoint;
    public Vector3 spawnRotation;
    public Collider hitCollider = null;
    public Transform hitTransform = null;

    public ShootConfigScriptableObject shootConfig;
    public DamageConfigScriptableObject damageConfig;
    //public TrailConfigScriptableObject trailConfig;

    private MonoBehaviour activeMonoBehaviour;
    private GameObject Model;
    private float lastShootTime;
    private ParticleSystem shootSystem;
    //public LineRenderer lineRenderer;

    public void Spawn(Transform parent, MonoBehaviour activeMonoBehaviour)
    {
        this.activeMonoBehaviour = activeMonoBehaviour;
        lastShootTime = 0;

        Model = Instantiate(modelPrefab);
        Model.transform.SetParent(parent, false);
        Model.transform.localPosition = spawnPoint;
        Model.transform.localRotation = Quaternion.Euler(spawnRotation);

        shootSystem = Model.GetComponentInChildren<ParticleSystem>();
        //lineRenderer = Model.GetComponentInChildren<LineRenderer>();
    }

    public void Shoot()
    {
        //Vector3 mouseWorldPosition = Vector3.zero;
        //Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        //Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        //Debug.Log("Atirou");
        //lineRenderer.SetPosition(0, shootSystem.transform.position);


        if (Time.time > shootConfig.fireRate + lastShootTime)
        {
            lastShootTime = Time.time;
            //Debug.Log("Atirou");
            Vector3 shootDirection = -Model.transform.right
                + new Vector3(
                    Random.Range(-shootConfig.Spread.x, shootConfig.Spread.x), 
                    Random.Range(-shootConfig.Spread.y, shootConfig.Spread.y), 
                    Random.Range(-shootConfig.Spread.z, shootConfig.Spread.z)
                    );
            shootDirection.Normalize();

            if (Physics.Raycast(shootSystem.transform.position, shootDirection, out RaycastHit hit, int.MaxValue, shootConfig.hitMask) )
            {
                //Debug.Log(hit.collider);
                hitTransform = hit.transform;
                hitCollider = hit.collider;
                //lineRenderer.SetPosition(1, hit.point);
                if (hitCollider != null)
                {
                    if (hitTransform.TryGetComponent(out IDamageable damageable))
                    {
                        //Debug.Log("Acertou o inimigo");
                        damageable.TakeDamage(damageConfig.damage, hitCollider);
                        //DealDamage(weaponSelector.ActiveWeapon.hitTransform.GetComponentInParent<HumanoidEnemy>(), damage, weaponSelector.ActiveWeapon.hitCollider);
                    }
                    
                }
            }
        }
    }

}
