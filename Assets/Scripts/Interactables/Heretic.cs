using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heretic : Enemy
{
    private void OnDestroy()
    {
        Debug.Log("HERESY ENDS");
    }
}
