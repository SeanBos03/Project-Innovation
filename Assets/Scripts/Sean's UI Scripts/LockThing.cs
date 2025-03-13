using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockThing : MonoBehaviour
{
    public void LockSwipe()
    {
        GameData.swipeLock = true;
    }

    public void UnlockSwipe()
    {
        GameData.swipeLock = false;
    }
}
