using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunShoot : MonoBehaviour
{
    [SerializeField] public GunScriptable gunScriptable;
    [SerializeField] private GameObject muzzleParticles;
    [SerializeField] private Transform muzzlePosition;
    [SerializeField] private GameObject impactEffect;
    private Text chargeAmmoText;
    private Text bedroomAmmoText;

    public bool isShooting;
    public bool isRealoading;

    public int currentChargerAmmo;
    public int currentBedroomAmmo;
    public float reloadTime;
    private float nextTimeToFire;
    private PlayerMovement playerMovement;
    private Camera cam;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        chargeAmmoText = FindObjectOfType<chargerAmmo>().GetComponent<Text>();
        bedroomAmmoText = FindObjectOfType<bedroomAmmo>().GetComponent<Text>();
        cam = Camera.main;
    }

    private void Start()
    {
        InitializeVariables();
        InitializeAmmo();
    }

    private void InitializeVariables()
    {
        reloadTime = gunScriptable.reloadingtime;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (isRealoading)
        {
            return;
        }
        ReloadChecker();
        ShootGun();
    }

    private void ReloadChecker()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentChargerAmmo <= 0 || currentChargerAmmo < gunScriptable.maxBulletPerCharger)
            {
                if (!playerMovement.isRunning && currentBedroomAmmo > 0)
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
                if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                {
                    isShooting = true;
                    isRealoading = false;
                    nextTimeToFire = Time.time + 1f / gunScriptable.fireRate;
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
        GameObject muzzleClone = Instantiate(muzzleParticles, muzzlePosition.transform.position, transform.rotation);
        muzzleClone.transform.SetParent(muzzlePosition);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, gunScriptable.range))
        {
            if (hit.collider.gameObject.layer == 9)
            {
                EnemyHealth enemy = hit.collider.gameObject.GetComponentInParent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(gunScriptable.damage);
                }
                GameObject impactClone = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                impactClone.transform.SetParent(hit.transform);
            }
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.green, gunScriptable.range);
    }

    private void SpendAmmo()
    {
        currentChargerAmmo--;

        UpdateAmmoTexts();
    }

    public void UpdateAmmoTexts()
    {
        if (currentChargerAmmo <= 0)
        {
            currentChargerAmmo = 0;
        }
        if (currentBedroomAmmo < 0)
        {
            currentBedroomAmmo = 0;
        }
        chargeAmmoText.text = currentChargerAmmo.ToString();
        bedroomAmmoText.text = currentBedroomAmmo.ToString();
    }
}
