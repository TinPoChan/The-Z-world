using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour, IdamgeAble
{
    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }
}
