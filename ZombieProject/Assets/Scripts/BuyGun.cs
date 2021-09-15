using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyGun : MonoBehaviour
{
    [SerializeField] private string gunName;
    [SerializeField] private int cost;
    [SerializeField] private GunShoot gunPrefab;
    [SerializeField] private GrenadeThrower grenadePrefab;
    [SerializeField] private Transform gunSpawn;
    [SerializeField] private Text gunText;
    [SerializeField] private bool isGranede;

    private WeaponSwitching weaponSwitching;
    private Vector3 newGunPos;

    private void Awake()
    {
        weaponSwitching = FindObjectOfType<WeaponSwitching>();
    }

    private void Start()
    {
        ChangeGunText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gunText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gunText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                PlayerScore playerScore = other.GetComponent<PlayerScore>();
                if (playerScore.score >= cost)
                {
                    playerScore.QuitScore(cost);
                    if (!isGranede)
                    {
                        newGunPos = new Vector3(other.transform.position.x, other.transform.position.y + .5f, other.transform.position.z);
                        GameObject gunClone = Instantiate(gunPrefab.gameObject, newGunPos, gunPrefab.gameObject.transform.rotation);
                        PlayerItemManager pim = other.GetComponent<PlayerItemManager>();
                        pim.gunShoots.Add(gunClone.GetComponent<GunShoot>());
                        gunClone.transform.SetParent(gunSpawn);
                    }
                    else
                    {
                        GameObject gunClone = Instantiate(grenadePrefab.gameObject, other.transform.position, Quaternion.identity);
                        PlayerItemManager pim = other.GetComponent<PlayerItemManager>();
                        pim.grenades.Add(gunClone.GetComponent<GrenadeThrower>());
                        gunClone.transform.SetParent(gunSpawn);
                    }
                    weaponSwitching.selectedWeapon = 1;
                    weaponSwitching.SelectedWeapon();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gunText.gameObject.SetActive(false);
        }
    }

    private void ChangeGunText()
    {
        gunText.text = "Press F to " + gunName + "(" + cost + ")";
    }
}
