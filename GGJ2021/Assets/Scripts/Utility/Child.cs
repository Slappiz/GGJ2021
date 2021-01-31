using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    private Animator _animator;
    public GameObject Flag;
    public GameObject Objective1;
    public GameObject Objective2;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        Flag.SetActive(false);
    }

    public void SetPanicAnimation()
    {
        _animator.Play("Panic");
        StartCoroutine(TimeToIdle());

    }

    public void SetIdleAnimation()
    {
        _animator.Play("Idle");
    }

    IEnumerator TimeToIdle()
    {
        yield return new WaitForSeconds(15f);
        SetIdleAnimation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SetPanicAnimation();
            Flag.SetActive(true);
            Objective2.SetActive(false);
        }
    }
}
