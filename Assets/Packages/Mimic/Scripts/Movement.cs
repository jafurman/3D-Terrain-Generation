using System.Collections;
using UnityEngine;

namespace MimicSpace
{
    public class Movement : MonoBehaviour
    {
        [Header("Controls")]
        [Range(0.5f, 5f)]
        public float height = 0.8f;
        public float speed = 5f;
        public float chaseSpeed = 10f; 
        public float detectionRadius = 5f; 
        public float stopDistance = 1f; 
        private Vector3 velocity = Vector3.zero;
        public float velocityLerpCoef = 4f;

        private Mimic myMimic;
        private Transform player;
        private Light specificLight;

        private float changeDirectionTime = 5f; 
        private float timer = 0f;
        private bool isChasing = false;
        private bool isBackingUp = false;
        public bool isStopped = false; 

        private void Start()
        {
            myMimic = GetComponent<Mimic>();
            player = GameObject.FindGameObjectWithTag("Player").transform; 
            specificLight = GameObject.FindGameObjectWithTag("Flashlight").GetComponent<Light>();
            ChangeDirection();
        }

        private void Update()
        {
            DetectLightSource();

            if (isStopped)
            {
                MoveAwayFromPlayer();
                return;
            }

            timer += Time.deltaTime;
            if (timer >= changeDirectionTime && !isChasing && !isBackingUp)
            {
                ChangeDirection();
                timer = 0f;
            }

            float sqrDistanceToPlayer = (transform.position - player.position).sqrMagnitude;

            if (sqrDistanceToPlayer <= detectionRadius * detectionRadius && !isBackingUp)
            {
                isChasing = true;
                velocity = Vector3.Lerp(velocity, (player.position - transform.position).normalized * chaseSpeed, Time.deltaTime * velocityLerpCoef);
            }
            else if (sqrDistanceToPlayer <= stopDistance * stopDistance)
            {
                isChasing = false;
                isBackingUp = true;
                StartCoroutine(BackUp());
            }
            else if (sqrDistanceToPlayer > detectionRadius * detectionRadius)
            {
                isChasing = false;
                isBackingUp = false;
            }

            if (isChasing && sqrDistanceToPlayer <= stopDistance * stopDistance)
            {
                velocity = Vector3.zero;
                isChasing = false;
                isBackingUp = true;
            }

            myMimic.velocity = velocity;
            MoveMimic();
            AdjustHeight();
        }

        private void MoveAwayFromPlayer()
        {
            Vector3 directionAwayFromPlayer = (transform.position - player.position).normalized;
            velocity = directionAwayFromPlayer * speed; 
            myMimic.velocity = velocity;
        }

        private void MoveMimic()
        {
            transform.position += velocity * Time.deltaTime;
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, 5, 60f),
                transform.position.y,
                Mathf.Clamp(transform.position.z, 5, 60f)
            );
        }

        private void DetectLightSource()
        {
            if (specificLight != null && specificLight.enabled)
            {
                Vector3 directionToLight = transform.position - specificLight.transform.position;
                if (Vector3.Angle(specificLight.transform.forward, directionToLight) < specificLight.spotAngle / 2 &&
                    directionToLight.magnitude <= specificLight.range)
                {
                    isStopped = true;
                }
                else
                {
                    isStopped = false;
                }
            }
            else
            {
                isStopped = false;
            }
        }

        private IEnumerator BackUp()
        {
            float backUpTime = 7f; 
            float elapsedTime = 0f;
            Vector3 directionAwayFromPlayer = (transform.position - player.position).normalized;

            while (elapsedTime < backUpTime)
            {
                velocity = directionAwayFromPlayer * speed;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            isBackingUp = false;
            ChangeDirection();
        }

        private void ChangeDirection()
        {
            float randomX = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);
            Vector3 randomDirection = new Vector3(randomX, 0, randomZ).normalized;
            velocity = randomDirection * speed;
        }

        private void AdjustHeight()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 5f, -Vector3.up, out hit))
            {
                Vector3 destHeight = new Vector3(transform.position.x, hit.point.y + height, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, destHeight, velocityLerpCoef * Time.deltaTime);
            }
        }
    }
}
