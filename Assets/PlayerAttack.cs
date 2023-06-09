using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float AttackRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float damageAmount = 1f;


    private RaycastHit2D[] hits;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (UserInput.instance.controls.Attack.Attack.WasPressedThisFrame())
        {
            Attack();
            anim.SetTrigger("attack");
        }
    }


    private void Attack()
    {
        hits = Physics2D.CircleCastAll(attackTransform.position, AttackRange, transform.right, 0f, attackableLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            IDamagable iDamageable = hits[i].collider.gameObject.GetComponent<IDamagable>();

            //if we found an iDamageable
            if (iDamageable != null)
            {
                iDamageable.Damage(damageAmount);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTransform.position, AttackRange);
    }
}

