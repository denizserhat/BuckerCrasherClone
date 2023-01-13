using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Draw
{
    public class DrawMesh : MonoBehaviour
    {
        public DrawSo drawSo;
        private Camera _cam;
        private CancellationTokenSource _drawingCancellationTokenSource;
        private GameObject _drawing;
        private Vector3 _lastMousePosition;

        private void Start()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _drawingCancellationTokenSource = new CancellationTokenSource();
                Draw();
            }
            if (Input.GetMouseButtonUp(0))
            {
                _drawingCancellationTokenSource.Cancel();
            }
        }


        private async void Draw()
        {
            if (_drawing != null)
            {
                DestroyImmediate(_drawing);
            }
            _drawing = new GameObject("Drawing");
            _drawing.AddComponent<MeshFilter>();
            _drawing.AddComponent<MeshRenderer>();

            
            Vector3 startPosition =
                _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,  -_cam.transform.position.z));
            Vector3 temp = new Vector3(startPosition.x, startPosition.y, 0.5f);

            
            Mesh mesh = new Mesh();
            List<Vector3> vertices =  new List<Vector3>(new Vector3[8]);
            
            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i] = temp;
            }

            var triangles = SetTriangles();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            
            while (!_drawingCancellationTokenSource.IsCancellationRequested)
            {
                

                #region Distance

                var position = _cam.transform.position;
                _lastMousePosition = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,  -position.z));

                float distance =
                    Vector3.Distance(_cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -position.z)),
                        _lastMousePosition);

                while (distance<drawSo.shapeDistance)
                {
                    distance =
                        Vector3.Distance(_cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,  -position.z)),
                            _lastMousePosition);
                    await UniTask.Yield();
                }

                #endregion
                #region Set Faces

                vertices.AddRange(new Vector3[4]);
                triangles.AddRange(new int[30]);

                SetFaces(triangles, vertices);
                
                mesh.vertices = vertices.ToArray();
                mesh.triangles = triangles.ToArray();

                #endregion
                
                _drawing.GetComponent<MeshFilter>().mesh = mesh;
                _drawing.GetComponent<Renderer>().material = drawSo.shapeMaterial;
                
                await UniTask.Yield(cancellationToken: _drawingCancellationTokenSource.Token).SuppressCancellationThrow();
            }
            _drawing.AddComponent<MeshCollider>();
            _drawing.gameObject.layer = 8; // Platform Layer
            _drawing.transform.tag = "Platform";
        }

        private List<int> SetTriangles()
        {
            List<int> triangles = new List<int>(new int[36])
            {
                // Front Face
                [0] = 0,
                [1] = 2,
                [2] = 1,
                [3] = 0,
                [4] = 3,
                [5] = 2,
                // Top Face
                [6] = 2,
                [7] = 3,
                [8] = 4,
                [9] = 2,
                [10] = 4,
                [11] = 5,
                // Right Face
                [12] = 1,
                [13] = 2,
                [14] = 5,
                [15] = 1,
                [16] = 5,
                [17] = 6,
                // Left Face
                [18] = 0,
                [19] = 7,
                [20] = 4,
                [21] = 0,
                [22] = 4,
                [23] = 3,
                // Back Face
                [24] = 5,
                [25] = 4,
                [26] = 7,
                [27] = 5,
                [28] = 7,
                [29] = 6,
                // Bottom Face
                [30] = 0,
                [31] = 6,
                [32] = 7,
                [33] = 0,
                [34] = 1,
                [35] = 6
            };
            
            return triangles;
        }

        private void SetFaces(List<int> triangles, List<Vector3> vertices)
        {

            int vIndex = vertices.Count - 8;
            int vIndex0 = vIndex + 3;
            int vIndex1 =  vIndex + 2;
            int vIndex2 =  vIndex + 1;
            int vIndex3 =  vIndex + 0;
                
            int vIndex4 =  vIndex + 4;
            int vIndex5 =  vIndex + 5;
            int vIndex6 =  vIndex + 6;
            int vIndex7 =  vIndex + 7;
            
            int tIndex = triangles.Count - 30;

            // New Top Face
            triangles[tIndex + 0] = vIndex2;
            triangles[tIndex + 1] = vIndex3;
            triangles[tIndex + 2] = vIndex4;
            triangles[tIndex + 3] = vIndex2;
            triangles[tIndex + 4] = vIndex4;
            triangles[tIndex + 5] = vIndex5;
                
            // New Right Face
            triangles[tIndex + 6] = vIndex1;
            triangles[tIndex + 7] = vIndex2;
            triangles[tIndex + 8] = vIndex5;
            triangles[tIndex + 9] = vIndex1;
            triangles[tIndex + 10] = vIndex5;
            triangles[tIndex + 11] = vIndex6;
                
            // New Left Face
            triangles[tIndex + 12] = vIndex0;
            triangles[tIndex + 13] = vIndex7;
            triangles[tIndex + 14] = vIndex4;
            triangles[tIndex + 15] = vIndex0;
            triangles[tIndex + 16] = vIndex4;
            triangles[tIndex + 17] = vIndex3;
                
            // New Back Face

            triangles[tIndex + 18] = vIndex5;
            triangles[tIndex + 19] = vIndex4;
            triangles[tIndex + 20] = vIndex7;
            triangles[tIndex + 21] = vIndex0;
            triangles[tIndex + 22] = vIndex4;
            triangles[tIndex + 23] = vIndex3;
                
            // New Bottom Face
            triangles[tIndex + 24] = vIndex0;
            triangles[tIndex + 25] = vIndex6;
            triangles[tIndex + 26] = vIndex7;
            triangles[tIndex + 27] = vIndex0;
            triangles[tIndex + 28] = vIndex1;
            triangles[tIndex + 29] = vIndex6;
            
            Vector3 currentMousePosition =
                _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -_cam.transform.position.z));
            Vector3 mouseForwardVector = (currentMousePosition - _lastMousePosition).normalized;
            
            Vector3 topRightVertex = currentMousePosition + Vector3.Cross(mouseForwardVector, Vector3.back) * drawSo.lineThickness;
            Vector3 bottomRightVertex = currentMousePosition + Vector3.Cross(mouseForwardVector, Vector3.forward) * drawSo.lineThickness;
            Vector3 topLeftVertex = new Vector3(topRightVertex.x, topRightVertex.y, 1);
            Vector3 bottomLeftVertex = new Vector3(bottomRightVertex.x, bottomRightVertex.y, 1);
            
            vertices[vIndex4] = topLeftVertex; 
            vertices[vIndex5] = topRightVertex; 
            vertices[vIndex6] = bottomRightVertex; 
            vertices[vIndex7] = bottomLeftVertex;

        }

    }
}
