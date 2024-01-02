using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHp = 1000;
    public int currenthp = 1000;

    [SerializeField] StatusBar hpBar;

    private void Start()
    {
        hpBar.SetState(currenthp, maxHp);
    }

    public void TakeDamge(int damage)
    {
        currenthp -= damage;
        if (currenthp <= 0)
        {
            Debug.Log("Die");
            //Die();
        }

        hpBar.SetState(currenthp, maxHp);
    }

    public void Heal(int heal)
    {
        if (currenthp <= 0 )
        {
            return;
        }

        currenthp += heal;
        if (currenthp > maxHp)
        {
            currenthp = maxHp;
        }

        hpBar.SetState(currenthp, maxHp);

    }
}
