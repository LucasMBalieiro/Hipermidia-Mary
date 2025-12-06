using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawMesh : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private Material material;
    
    [SerializeField] private float minDistance = .1f;
    [SerializeField] private float lineThickness;
    
    private Mesh mesh;
    private Vector3 lastMousePosition;
    private readonly Vector3 normal2D = new Vector3(0,0,-1);
    
    private List<Vector2> currentGesturePoints = new List<Vector2>();
    
    public static event Action<GestureName> OnGestureRecognized;
    
    private void Start()
    {
        GetComponent<MeshRenderer>().material = material;
    }

    private void OnEnable() => Actions.OnGameOver += ClearMesh;
    private void OnDisable() => Actions.OnGameOver -= ClearMesh;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseDown();
        }

        if (Input.GetMouseButton(0))
        {
            HandleMouseDrag();
        }
        if (Input.GetMouseButtonUp(0))
        {
            HandleMouseUp();
        }
        
    }


    private void HandleMouseDown()
    {
        mesh = new Mesh();
        currentGesturePoints.Clear();
        
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];
            
        Vector3 currentMousePosition = DrawUtils.GetMouseWorldPosition(camera);

        vertices[0] = currentMousePosition;
        vertices[1] = currentMousePosition;
        vertices[2] = currentMousePosition;
        vertices[3] = currentMousePosition;
        
        uv[0] = Vector2.zero;
        uv[2] = Vector2.zero;
        uv[1] = Vector2.zero;
        uv[3] = Vector2.zero;
        
        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 1;
        
        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;
        
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.MarkDynamic();
        
        GetComponent<MeshFilter>().mesh = mesh;
            
        currentGesturePoints.Add(new Vector2(currentMousePosition.x, currentMousePosition.y));
        lastMousePosition = currentMousePosition;
    }

    private void HandleMouseDrag()
    {
        Vector3 currentMousePosition = DrawUtils.GetMouseWorldPosition(camera);
            
            if (Vector3.Distance(currentMousePosition, lastMousePosition) > minDistance)
            {
                currentGesturePoints.Add(new Vector2(currentMousePosition.x, currentMousePosition.y));
                
                Vector3[] newVertices = new Vector3[mesh.vertices.Length + 2];
                Vector2[] newUV = new Vector2[mesh.vertices.Length + 2];
                int[] newTriangles = new int[mesh.triangles.Length + 6];
            
                mesh.vertices.CopyTo(newVertices, 0);
                mesh.uv.CopyTo(newUV, 0);
                mesh.triangles.CopyTo(newTriangles, 0);

                int vertexIndex = newVertices.Length - 4;
            
                int vertexIndex0 = vertexIndex;
                int vertexIndex1 = vertexIndex + 1;
                int vertexIndex2 = vertexIndex + 2;
                int vertexIndex3 = vertexIndex + 3;
            
                Vector3 mouseFowardVector = (currentMousePosition - lastMousePosition).normalized;

                Vector3 newVertexUp = currentMousePosition + Vector3.Cross(mouseFowardVector, normal2D) * lineThickness;
                Vector3 newVertexDown = currentMousePosition + Vector3.Cross(mouseFowardVector, -normal2D) * lineThickness;
            
                newVertices[vertexIndex2] = newVertexUp;
                newVertices[vertexIndex3] = newVertexDown;
            
                newUV[vertexIndex2] = Vector2.zero;
                newUV[vertexIndex3] = Vector2.zero;
            
                int triangleIndex = newTriangles.Length - 6;
            
                newTriangles[triangleIndex] = vertexIndex0;
                newTriangles[triangleIndex + 1] = vertexIndex2;
                newTriangles[triangleIndex + 2] = vertexIndex1;
            
                newTriangles[triangleIndex + 3] = vertexIndex1;
                newTriangles[triangleIndex + 4] = vertexIndex2;
                newTriangles[triangleIndex + 5] = vertexIndex3;
            
                mesh.vertices = newVertices;
                mesh.uv = newUV;
                mesh.triangles = newTriangles;
            
                lastMousePosition = currentMousePosition;   
            }
    }

    private void HandleMouseUp()
    {
        RecognitionResult result = PennyPincher.Recognize(currentGesturePoints);
        OnGestureRecognized?.Invoke(result.gestureName);
        ClearMesh();
    }
    
    private void ClearMesh()
    {
        if (mesh) mesh.Clear();
    }
}
