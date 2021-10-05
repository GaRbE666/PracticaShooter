using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTravelling : MonoBehaviour
{
    [SerializeField] private AnimationClip travEscenario;
    [SerializeField] private AnimationClip travBar;
    [SerializeField] private Animation animationTravelling;
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        cam.enabled = false;
    }

    public void PlayTravEsce()
    {
        cam.enabled = true;
        animationTravelling.clip = travEscenario;
        animationTravelling.Play();
    }

    public void PlayTravBar()
    {
        cam.enabled = true;
        animationTravelling.clip = travBar;
        animationTravelling.Play();
    }
}
