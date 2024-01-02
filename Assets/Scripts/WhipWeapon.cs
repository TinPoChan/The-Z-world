using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : MonoBehaviour
{
    [SerializeField] float timeToAttack = 4f;
    float timer;

    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;

    PlayerMove playerMove;
    [SerializeField] Vector2 whipAttackSize = new Vector2(4f, 2f);

    [SerializeField] int whipDamge = 1;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Attack();
        }
    }

    private void Attack()
    {
        //Debug.Log("Attack");
        timer = timeToAttack;

        if(playerMove.lastHorizontalVector > 0 )
        {
            rightWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamge(colliders);
        }
        else
        {
            leftWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamge(colliders);
        }
    }

    private void ApplyDamge(Collider2D[] colliders)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            IdamgeAble e = colliders[i].GetComponent<IdamgeAble>();
            if (e != null)
            {
                e.TakeDamage(whipDamge);
            }
            
        }
    }
}
