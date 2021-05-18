using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public Transform[] positions1;
    public Transform[] positions2;
    public static WayPoints Instance;
    public bool isOnlyOneRoad;
    private void Awake()
    {
        Instance = this;
    }
}
