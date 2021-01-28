using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Controller.Effects
{
    public class Dissolve : MonoBehaviour
    {
        private Material _material;
        private bool _isDissolving;
        private float _duration;

        private void Start()
        {
            _material = GetComponent<SpriteRenderer>().material;
        }

        public bool IsDissolving
        {
            get => _isDissolving;
        }

        public void StartDissolveCycle(float duration)
        {
            _duration = duration * 0.5f;
            StartCoroutine(RunDissolveCycle());
        }

        public void StartDissolve(float duration)
        {
            _duration = duration;
            DOVirtual.Float(1, 0, _duration, Fade);
        }

        private IEnumerator RunDissolveCycle()
        {
            _isDissolving = true;
            DOVirtual.Float(1, 0, _duration, Fade);
            yield return new WaitForSeconds(_duration);
            StartCoroutine(RevereseDissolve());
        }

        private IEnumerator RevereseDissolve()
        {
            DOVirtual.Float(0, 1, _duration, Fade);
            yield return new WaitForSeconds(_duration);
            _isDissolving = false;
        }
        

        private void Fade(float value)
        {
            _material.SetFloat("_Fade", value);
        }
    }
}