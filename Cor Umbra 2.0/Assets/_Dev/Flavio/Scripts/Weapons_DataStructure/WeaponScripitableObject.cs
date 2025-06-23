using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon", order = 0)]
public class WeaponScripitableObject : ScriptableObject
{
    public WeaponType type;
    public string name;
    public GameObject modelPrefab;
    public int maxWeaponAmmo;
    public int weaponAmmo;
    public int reloadbleAmmo;
    public Vector3 spawnPoint;
    public Vector3 spawnRotation;
    public VisualEffect shootEffect;
    public AudioSource shootAudioSource;
    public AudioClip[] shootAudios;
    public Item[] ammotype;
    public Collider hitCollider = null;
    public Vector3 hitPoint = Vector3.zero;
    public Transform hitTransform = null;
    

    public ShootConfigScriptableObject shootConfig;
    public DamageConfigScriptableObject damageConfig;
    //public TrailConfigScriptableObject trailConfig;

    private MonoBehaviour activeMonoBehaviour;
    private GameObject Model;
    private float lastShootTime;
    [SerializeField] private PlayerInventory playerInventory;
    //public LineRenderer lineRenderer;

    public void Spawn(Transform parent, MonoBehaviour activeMonoBehaviour)
    {
        this.activeMonoBehaviour = activeMonoBehaviour;
        lastShootTime = 0;
        weaponAmmo = maxWeaponAmmo;
        reloadbleAmmo = 0;
        Model = Instantiate(modelPrefab);
        Model.transform.SetParent(parent, false);
        Model.transform.localPosition = spawnPoint;
        Model.transform.localRotation = Quaternion.Euler(spawnRotation);

        shootEffect = Model.GetComponentInChildren<VisualEffect>();
        shootAudioSource = Model.GetComponent<AudioSource>();
        shootAudioSource.volume = 0.3f;
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        //lineRenderer = Model.GetComponentInChildren<LineRenderer>();
        AddAmmo();
    }

    public void AddAmmo()
    {
        foreach (Item item in playerInventory.items)
        {
            if (item != null)
            {
                if (item.Id == "#001")
                {
                    reloadbleAmmo += item.Amount;
                    playerInventory.items.Remove(item);
                    break;
                }
            }

        }
    }

    public void Reload()
    {

        int ammoRequired = maxWeaponAmmo - weaponAmmo;
        
        if (ammoRequired > 0)
        {
            if(reloadbleAmmo > 0)
            {
                int reloadingAmmo = Mathf.Min(ammoRequired, reloadbleAmmo);
                weaponAmmo += reloadingAmmo;
                reloadbleAmmo -= reloadingAmmo;
            }
            else
            {
                Debug.Log("Sem munição");
            }
        }
        else
        {
            Debug.Log("Carregador cheio");
        }

    }

    public void Shoot()
    {
        if(weaponAmmo > 0)
        {
            shootAudioSource.clip = shootAudios[Random.Range(0, 2)];
        }
        else
        {
            shootAudioSource.clip = shootAudios[3] ;
        }
        //Vector3 mouseWorldPosition = Vector3.zero;
        //Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        //Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        //Debug.Log("Atirou");
        //lineRenderer.SetPosition(0, shootSystem.transform.position);

    
        if (Time.time > shootConfig.fireRate + lastShootTime)
        {
            shootAudioSource.Play();
            lastShootTime = Time.time;
            Debug.Log("Atirou");
            Vector3 shootDirection = -Model.transform.right
                + new Vector3(
                    Random.Range(-shootConfig.Spread.x, shootConfig.Spread.x), 
                    Random.Range(-shootConfig.Spread.y, shootConfig.Spread.y), 
                    Random.Range(-shootConfig.Spread.z, shootConfig.Spread.z)
                    );
            shootDirection.Normalize();

            if (Physics.Raycast(shootEffect.transform.position, shootDirection, out RaycastHit hit, int.MaxValue, shootConfig.hitMask) && weaponAmmo > 0)
            {
                weaponAmmo--;
                shootEffect.Play();
                //Debug.Log(hit.collider);
                hitPoint = hit.point;
                hitTransform = hit.transform;
                hitCollider = hit.collider;
                //lineRenderer.SetPosition(1, hit.point);
                if (hitCollider != null)
                {
                    if (hitTransform.TryGetComponent(out IDamageable damageable))
                    {
                        //Debug.Log("Acertou o inimigo");
                        damageable.TakeDamage(damageConfig.damage, hitCollider, hitPoint);
                        //DealDamage(weaponSelector.ActiveWeapon.hitTransform.GetComponentInParent<HumanoidEnemy>(), damage, weaponSelector.ActiveWeapon.hitCollider);
                    }
                    
                }
            }

        }

    }

}
