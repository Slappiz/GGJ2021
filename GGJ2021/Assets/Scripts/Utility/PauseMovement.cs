using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PauseMovement : MonoBehaviour
{
    public float PauseDuration = 5f;
    public PlayableDirector PlayableDirector;
    public Child Child;
    public GameObject Objective;

    private void Awake()
    {
        Objective.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            var controller = other.GetComponent<Controller.CharacterController>();
            controller.LockMovement(true);
            PlayableDirector.Play();
            StartCoroutine(PausePlayer(controller));
        }
    }

    IEnumerator PausePlayer(Controller.CharacterController player)
    {
        yield return new WaitForSeconds(PauseDuration * .6f);
        Child.SetPanicAnimation();
        yield return new WaitForSeconds(PauseDuration * .4f);
        player.LockMovement(false);
        Objective.SetActive(true);
        Destroy(gameObject);
    } 
}
