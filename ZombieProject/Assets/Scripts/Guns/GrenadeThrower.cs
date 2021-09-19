using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeThrower : MonoBehaviour
{
    [Header("GameObjects References")]
    public GunScriptable grenadeScriptable;
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private Transform grenadeSpawn;
    [SerializeField] private GameObject fakeGrenade;
    [Header("Texts References")]
    [SerializeField] private Text grenadeNameText;
    [SerializeField] private Text noAmmoText;

    [HideInInspector] public int _currentChargerAmmo;
    [HideInInspector] public int _currentBedroomAmmo;
    [HideInInspector] public bool _isShooting;
    [HideInInspector] public bool _isReloading;

    private Animator _animator;
    private float _nextTimeToTrhow;
    private PlayerMovement _playerMovement;
    private Text _chargeAmmoText;
    private Text _bedroomAmmoText;

    private int _running = Animator.StringToHash("isRunning");
    private int _throwGrenade = Animator.StringToHash("throw");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _chargeAmmoText = FindObjectOfType<chargerAmmo>().GetComponent<Text>();
        _bedroomAmmoText = FindObjectOfType<bedroomAmmo>().GetComponent<Text>();
    }

    private void OnEnable()
    {
        UpdateAmmoText();
        _isReloading = false;
        grenadeNameText.text = grenadeScriptable.name;
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
        if (_playerMovement.isRunning)
        {
            _animator.SetBool(_running, true);
        }
        else
        {
            _animator.SetBool(_running, false);
        }

        if (_isShooting)
        {
            _animator.SetTrigger(_throwGrenade);
        }
    }

    private IEnumerator ThrowGranade()
    {
        yield return new WaitForSeconds(1f);
        SpendAmmo();
        GameObject grenadeClone = Instantiate(grenadePrefab, grenadeSpawn.position, grenadeSpawn.rotation);
        Rigidbody rb = grenadeClone.GetComponent<Rigidbody>();
        if (_playerMovement.xRotation < -10)
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
        
        if (_currentChargerAmmo == 0)
        {
            noAmmoText.text = "No ammo";
            noAmmoText.color = Color.red;
        }
        else
        {
            noAmmoText.text = "";
        }

        _chargeAmmoText.text = _currentChargerAmmo.ToString();
        _bedroomAmmoText.text = _currentBedroomAmmo.ToString();
        if (_currentBedroomAmmo <= 0)
        {
            _bedroomAmmoText.text = "0";
        }

        if (_currentChargerAmmo <= 0)
        {
            _chargeAmmoText.text = "0";
        }
    }
}
