using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IdamgeAble
{
    Transform targetDestination;
    GameObject targetGameObject;
    Character targetCharater;
    [SerializeField] float speed;

    Rigidbody2D Rigidbody2D;

    [SerializeField] int hp = 4;
    [SerializeField] int damge = 1;
    [SerializeField] int exp = 400;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        Rigidbody2D.velocity = direction * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject)
        {
            Attack();
        }
    }

    private void Attack()
    {
        //Debug.Log("Attacking the player");
        if (targetCharater == null)
        {
            targetCharater = targetGameObject.GetComponent<Character>();
        }

        targetCharater.TakeDamge(damge);
    }

    public void TakeDamage(int Dmage)
    {
        hp -= Dmage;

        //It should show some effect here

        if (hp <= 0)
        {
            targetGameObject.GetComponent<Level>().AddExp(exp);
            Destroy(gameObject);
        }
    }
}
