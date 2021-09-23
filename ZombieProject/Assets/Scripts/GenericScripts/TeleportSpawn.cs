using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportSpawn : MonoBehaviour
{
    [SerializeField] private Text teleportSpawnText;
    [SerializeField] private GameObject cableLink;
    [SerializeField] private Material cableLinkMaterial;
    public bool link2;
    private Teleport _teleport;

    private void Awake()
    {
        _teleport = FindObjectOfType<Teleport>();
    }

    private void Start()
    {
        teleportSpawnText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_teleport.link1 && !link2)
            {
                SetTeleportSpawnText("Press F to link");
                ActiveText();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_teleport.link1)
            {
                SetTeleportSpawnText("Press F to link");
                ActiveText();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (_teleport.link1)
                    {
                        LinkUp();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DesactiveText();
        }
    }

    private void ActiveText()
    {
        teleportSpawnText.enabled = true;
    }

    private void DesactiveText()
    {
        teleportSpawnText.enabled = false;
    }

    private void SetTeleportSpawnText(string text)
    {
        teleportSpawnText.text = text;
    }

    private void LinkUp()
    {
        link2 = true;
        cableLink.GetComponent<MeshRenderer>().material = cableLinkMaterial;
    }
}
