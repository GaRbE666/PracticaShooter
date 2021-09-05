using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeThrower : MonoBehaviour
{
    public GunScriptable grenadeScriptable;
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private Transform grenadeSpawn;
    [SerializeField] private GameObject fakeGrenade;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Text chargeAmmoText;
    [SerializeField] private Text bedroomAmmoText;

    public int _currentChargerAmmo;
    public int _currentBedroomAmmo;
    public bool _isShooting;
    public bool _isReloading;
    private Animator _animator;
    private float _nextTimeToTrhow;

    private int running = Animator.StringToHash("isRunning");
    private int throwGrenade = Animator.StringToHash("throw");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        UpdateAmmoText();
        _isReloading = false;
    }

    private void Start()
    {
        _currentBedroomAmmo = grenadeScriptable.maxBulletsInBedroom;
        _currentChargerAmmo = grenadeScriptable.maxBulletPerCharger;
        UpdateAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isReloading)
        {
            return;
        }
        if (_currentBedroomAmmo >= 0)
        {
            Reload();
        }

        CheckThrow();
        ChangeAnimations();
    }

    private void CheckThrow()
    {
        if (gameObject.activeSelf)
        {
            if (_currentChargerAmmo != 0)
            {
                if (Input.GetButtonDown("Fire1") && Time.time >= _nextTimeToTrhow)
                {
                    _isShooting = true;
                    _nextTimeToTrhow = Time.time + 1f / grenadeScriptable.fireRate;
                    StartCoroutine(ThrowGranade());
                }
                else
                {
                    _isShooting = false;
                }
            }
            else
            {
                _isShooting = false;
            }

        }
    }

    private void Reload()
    {
        if (_currentChargerAmmo <= 0)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;
        yield return new WaitForSeconds(1f);
        _currentChargerAmmo++;
        UpdateAmmoText();
        _isReloading = false;
    }

    private void ChangeAnimations()
    {
        if (playerMovement.isRunning)
        {
            _animator.SetBool(running, true);
        }
        else
        {
            _animator.SetBool(running, false);
        }

        if (_isShooting)
        {
            _animator.SetTrigger(throwGrenade);
        }
    }

    private IEnumerator ThrowGranade()
    {
        yield return new WaitForSeconds(0.7f);
        SpendAmmo();
        GameObject grenadeClone = Instantiate(grenadePrefab, grenadeSpawn.position, grenadeSpawn.rotation);
        Rigidbody rb = grenadeClone.GetComponent<Rigidbody>();
        if (playerMovement.xRotation < -10)
        {
            rb.AddForce(grenadeSpawn.forward * (grenadeScriptable.range + 5), ForceMode.VelocityChange);
        }
        else
        {
            rb.AddForce(grenadeSpawn.forward * grenadeScriptable.range, ForceMode.VelocityChange);
        }
        
        _isShooting = false;
    }

    private void SpendAmmo()
    {
        _currentChargerAmmo--;
        _currentBedroomAmmo--;

        if (_currentBedroomAmmo < 0)
        {
            fakeGrenade.SetActive(false);
            _currentBedroomAmmo = -1;
        }
        UpdateAmmoText();
    }

    public void UpdateAmmoText()
    {
        chargeAmmoText.text = _currentChargerAmmo.ToString();
        bedroomAmmoText.text = _currentBedroomAmmo.ToString();
        if (_currentBedroomAmmo <= 0)
        {
            bedroomAmmoText.text = "0";
        }

        if (_currentChargerAmmo <= 0)
        {
            chargeAmmoText.text = "0";
        }
    }
}
