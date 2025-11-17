using UnityEngine;

public class DrawMeshController : MonoBehaviour
{
    [SerializeField] DrawMesh drawMesh;

    private void OnEnable()
    {
        Actions.OnStartGame += EnableMesh;
        Actions.OnGameOver += DisableMesh;
    }

    private void OnDisable()
    {
        Actions.OnStartGame -= EnableMesh;
        Actions.OnGameOver -= DisableMesh;
    }
    
    private void EnableMesh()
    {
        drawMesh.enabled = true;
    }

    private void DisableMesh()
    {
        drawMesh.enabled = false;
    }
}
