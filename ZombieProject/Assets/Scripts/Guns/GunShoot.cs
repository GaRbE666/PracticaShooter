using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunShoot : MonoBehaviour
{
    [Header("GameObjects References")] 
    public GunScriptable gunScriptable;
    [SerializeField] private GameObject muzzleParticles;
    [SerializeField] private Transform muzzlePosition;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject headExplodeEffect;

    [Header("Texts References")]
    [SerializeField] private Text gunNameText;
    [SerializeField] private Text noAmmoText;

    [Header("PAP References")]
    public bool papActived;
    public GameObject gunModel;
    public Material papMaterial;
    [SerializeField] private GameObject papMuzzleParticles;

    [Header("Audio References")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip reload;

    private Text _chargeAmmoText;
    private Text _bedroomAmmoText;


    [HideInInspector] public bool isShooting;
    [HideInInspector] public bool isRealoading;
    [HideInInspector] public int currentChargerAmmo;
    [HideInInspector] public int currentBedroomAmmo;
    [HideInInspector] public int gunDamage;
    [HideInInspector] public float reloadTime;
    [HideInInspector] public float fireRate;

    private float _nextTimeToFire;
    private PlayerMovement _playerMovement;
    private PlayerAudio _playerAudio;
    private Camera _cam;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _chargeAmmoText = FindObjectOfType<chargerAmmo>().GetComponent<Text>();
        _bedroomAmmoText = FindObjectOfType<bedroomAmmo>().GetComponent<Text>();
        _playerAudio = FindObjectOfType<PlayerAudio>();
        _cam = Camera.main;
    }

    private void Start()
    {
        InitializeVariables();
        InitializeAmmo();
    }

    private void InitializeVariables()
    {
        reloadTime = gunScriptable.reloadingtime;
        fireRate = gunScriptable.fireRate;
        gunDamage = gunScriptable.damage;
    }

    private void InitializeAmmo()
    {
        currentBedroomAmmo = gunScriptable.maxBulletsInBedroom;
        currentChargerAmmo = gunScriptable.maxBulletPerCharger;
        UpdateAmmoTexts();
    }

    private void OnEnable()
    {
        UpdateAmmoTexts();
        isRealoading = false;
        gunNameText.text = gunScriptable.name;
    }

    void Update()
    {
        if (!_playerMovement.playerLock)
        {
            if (isRealoading)
            {
                return;
            }
            ReloadChecker();
            ShootGun();
        }

    }

    private void ReloadChecker()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentChargerAmmo <= 0 || currentChargerAmmo < gunScriptable.maxBulletPerCharger)
            {
                if (!_playerMovement.isRunning && currentBedroomAmmo > 0)
                {
                    Reload();
                }

            }
        }
    }

    private void ShootGun()
    {
        if (currentChargerAmmo != 0)
        {
            if (gunScriptable.singleShoot)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    isShooting = true;
                    isRealoading = false;
                    Shoot();
                }
                else
                {
                    isShooting = false;
                }
            }
            else
            {
                if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire)
                {
                    isShooting = true;
                    isRealoading = false;
                    _nextTimeToFire = Time.time + 1f / gunScriptable.fireRate;
                    Shoot();
                }
                else
                {
                    isShooting = false;
                }
            }
        }
        else
        {
            isShooting = false;
        }

    }

    private void Reload()
    {
        isShooting = false;
        audioSource.pitch = .65f;
        audioSource.PlayOneShot(reload);
        StartCoroutine(ReloadingCoroutine());
        return;
    }

    private IEnumerator ReloadingCoroutine()
    {
        isRealoading = true;
        currentBedroomAmmo -= (gunScriptable.maxBulletPerCharger - currentChargerAmmo);
        yield return new WaitForSeconds(reloadTime);
        currentChargerAmmo = gunScriptable.maxBulletPerCharger;
        isRealoading = false;
        UpdateAmmoTexts();
    }

    private void Shoot()
    {
        SpendAmmo();
        ThrowRay();
    }

    private void ThrowRay()
    {
        RaycastHit hit;
        SelectMuzzleIfPapActived();
        
        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, gunScriptable.range))
        {
            if (hit.collider.gameObject.layer == 9)
            {
                CalculateZombieDamage(hit);
            }
        }

    }

    private void SelectMuzzleIfPapActived()
    {
        if (!papActived)
        {
            Instantiate(muzzleParticles, muzzlePosition.transform.position, transform.rotation);
        }
        else
        {
            Instantiate(papMuzzleParticles, muzzlePosition.transform.position, transform.rotation);
        }
    }

    private void CalculateZombieDamage(RaycastHit hit)
    {
        EnemyHealth enemy = hit.collider.gameObject.GetComponentInParent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(gunDamage);
        }
        SelectWhatParticleShow(hit, enemy);

    }

    private void SelectWhatParticleShow(RaycastHit hit, EnemyHealth enemy)
    {
        if (hit.collider.gameObject.name.Equals("HeadCollider") && enemy.currentHealth <= 0)
        {
            _playerAudio.PlayHeadShootAudio();
            GameObject impactHeadClone = Instantiate(headExplodeEffect, hit.collider.gameObject.transform.position, headExplodeEffect.transform.rotation);
            impactHeadClone.transform.SetParent(hit.transform);
            enemy.DisableHead();
            hit.collider.enabled = false;
        }
        else
        {
            GameObject impactClone = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            impactClone.transform.SetParent(hit.transform);
        }
    }

    private void SpendAmmo()
    {
        currentChargerAmmo--;
        ControlOfMunitionAudios();
        UpdateAmmoTexts();
    }

    private void ControlOfMunitionAudios()
    {
        if (currentChargerAmmo > 0 && currentChargerAmmo < 10 && !gunScriptable.singleShoot && !_playerAudio._lowAmmoPlaying)
        {
            Debug.Log("Reproduzco lowAmmo");
            _playerAudio._lowAmmoPlaying = true;
            _playerAudio._noAmmoPlaying = false;
            _playerAudio.PlayLowAmmoAudio();
        }
        else if (currentChargerAmmo == 0 && !_playerAudio._noAmmoPlaying)
        {
            _playerAudio._noAmmoPlaying = true;
            _playerAudio.PlayNoAmmoAudio();

        }
        else if (currentChargerAmmo >= 10 && _playerAudio._lowAmmoPlaying)
        {
            _playerAudio._lowAmmoPlaying = false;
        }
    }

    public void UpdateAmmoTexts()
    {

        if (currentChargerAmmo > 0 && currentChargerAmmo < 10 && !gunScriptable.singleShoot)
        {
            noAmmoText.text = "Recarga...";
            noAmmoText.color = Color.yellow;
        }
        else if (currentChargerAmmo == 0)
        {
            noAmmoText.text = "Sin munición";
            noAmmoText.color = Color.red;
        }
        else
        {
            noAmmoText.text = "";
        }

        if (currentChargerAmmo <= 0)
        {
            currentChargerAmmo = 0;
        }
        if (currentBedroomAmmo < 0)
        {
            currentBedroomAmmo = 0;
        }

        _chargeAmmoText.text = currentChargerAmmo.ToString();
        _bedroomAmmoText.text = currentBedroomAmmo.ToString();
    }
}
