using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Sound;
using UnityEngine;

public class Kite : MonoBehaviour
{
    [SerializeField] private GameObject _target = null;
    [SerializeField] private GameObject objective1 = null;
    [SerializeField] private GameObject objective2 = null;
    [SerializeField] private BoxCollider2D ChildTriggerCollider = null;

    private void Awake()
    {
        ChildTriggerCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            _target.SetActive(true);
            objective1.SetActive(false);
            objective2.SetActive(true);
            ChildTriggerCollider.enabled = true;
            SoundManager.instance.Stop("Memories");
            SoundManager.instance.Play("Adventure");
            Destroy(this.gameObject);
        }
    }
}
