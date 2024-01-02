using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestory : MonoBehaviour
{
    [SerializeField] GameObject heart;
    [SerializeField] [Range(0,1)] float chance = 0.5f;

    private void OnDestroy()
    {
        if (Random.value < chance)
        {
            Transform t = Instantiate(heart).transform;
            t.position = transform.position;
        }

    }
}
