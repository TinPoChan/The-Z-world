using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int level = 1;
    int exp = 0;

    [SerializeField] ExpBar expBar;

    int TO_NEXT_LEVEL
    {
        get { return level * 1000; }
    }

    private void Start()
    {
        expBar.UpdateExpSlider(exp, TO_NEXT_LEVEL);
        expBar.SetLevelText(level);
    }

    public void AddExp(int amount)
    {
        exp += amount;
        CheckLevelUp();
        expBar.UpdateExpSlider(exp, TO_NEXT_LEVEL);
    }

    void CheckLevelUp()
    {
        if (exp >= TO_NEXT_LEVEL)
        {
            exp -= TO_NEXT_LEVEL;
            level++;
            Debug.Log("Level up!");
            expBar.SetLevelText(level);
        }
    }
}
