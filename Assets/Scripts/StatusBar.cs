using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Transform bar;

    public void SetState(int current, int max)
    {
        float state = (float)current / max;
        if (state < 0)
        {
            state = 0;
        }
            
        bar.transform.localScale = new Vector3(state, 1f, 1f);
    }
}
