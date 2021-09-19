using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainActive : MonoBehaviour
{
    private Animation _animation;
    private PowerOn _powerOn;

    private void Awake()
    {
        _animation = GetComponent<Animation>();
        _powerOn = FindObjectOfType<PowerOn>();
    }

    private void Start()
    {
        _powerOn.PowerOnReleased += ActiveCurtain;
    }

    private void ActiveCurtain()
    {
        _animation.Play();
    }

    public void FinishAnimation()
    {
        _powerOn.PowerOnReleased -= ActiveCurtain;
    }
}
