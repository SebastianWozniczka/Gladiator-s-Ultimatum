using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown = 2f;
    public float attackTimer;
    public float attackDistance = 3f;
    public float lookRadius = 15f;
    public float damage = 10f;


    public Transform targetTransform;
    PlayerTarget target;
    NavMeshAgent agent;


    public Transform attackSphereCenter;

    void Update()
    {
        attackTimer = attackTimer + 1f * Time.deltaTime;
        attackTimer = Mathf.Clamp(attackTimer, 0f, attackCooldown);

        float distance = Vector3.Distance(targetTransform.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(targetTransform.position);

            if (distance <= agent.stoppingDistance)
            {
                //attack and face target
                FaceTarget();
            }

            if (distance <= attackDistance && attackTimer >= attackCooldown)
            {
                //Attack Player
                attackTimer = 0f;
                Debug.Log("Enemy Attacking Player!");
                AttackPlayer();

            }

        }
    }

    void FaceTarget()
    {
        Vector3 direction = (targetTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    void Start()
    {
       // targetTransform = PlayerManager.instance.player.transform;
      
        target = targetTransform.GetComponent<PlayerTarget>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void AttackPlayer()
    {
        target.PlayerTakesDamage(damage);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius); //Draw Sphere in Editor(lookRadius)

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackSphereCenter.position, attackDistance); //Draw Sphere in Editor(attack distance)
    }
}
