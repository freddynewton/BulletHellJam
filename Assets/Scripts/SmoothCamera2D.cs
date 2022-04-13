using UnityEngine;
using System.Collections;

public class SmoothCamera2D : Singleton<SmoothCamera2D>
{
    public GameObject Target;
    public Vector3 Offset;

    public Vector2 ScrollWindow;

    private void OnDrawGizmosSelected()
    {
        
    }
}