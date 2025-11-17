using UnityEngine;

public static class DrawUtils
{
    public static Vector3 GetMouseWorldPosition(Camera camera)
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
