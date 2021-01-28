using System;
using System.Collections;
using UnityEngine;

namespace Controller.Effects
{
    public class GhostController : MonoBehaviour
    {
        public GameObject Ghost;
        public SpriteRenderer GFX;
        private Direction _direction;
        
        public void StartGhost(float spawnTime, float duration, Direction direction)
        {
            _direction = direction;
            
            InvokeRepeating("SpawnGhost", spawnTime, spawnTime);
            StartCoroutine(WaitAndStop(duration));
        }

        private void SpawnGhost()
        {
            var currentGhost = Instantiate(Ghost, transform.position, transform.rotation);
            var sprite = currentGhost.GetComponent<SpriteRenderer>();
            var dissolve = currentGhost.GetComponent<Dissolve>();
            
            if (dissolve != null)
            {
                dissolve.StartDissolve(2);
            }
            
            sprite.sprite = GFX.sprite;

            switch (_direction)
            {
                case Direction.Left:
                    sprite.flipX = true;
                    break;
                case Direction.Right:
                    sprite.flipX = false;
                    break;
                default:
                    break;
            }
            Destroy(currentGhost, 1f);
        }
        IEnumerator WaitAndStop(float time)
        {
            yield return new WaitForSeconds(time);
            CancelInvoke("SpawnGhost");
        }
    }
}