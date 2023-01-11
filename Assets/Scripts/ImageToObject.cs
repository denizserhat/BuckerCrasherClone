using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ImageToObject : MonoBehaviour
{
    public Texture2D image;
    [SerializeField] private Color[] pixels;
    void Start()
    {
        
        pixels = image.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            GameObject cube = new GameObject();
            MeshFilter meshFilter = cube.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = cube.AddComponent<MeshRenderer>();
            meshFilter.mesh = CreateCubeMesh();
            cube.AddComponent<BoxCollider>();
            Material cubeMaterial = CreateCubeMaterial(pixels[i]);
            meshRenderer.material = cubeMaterial;
            int x = i % image.width;
            int y = i / image.height;
            cube.transform.position = new Vector3(x, y, 0);
        }
    }
    
    

    private Mesh CreateCubeMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[]
        {
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f)
        };
        mesh.triangles = new int[]
        {
            0, 2, 1, 
            0, 3, 2,
            4, 6, 5, 
            4, 7, 6,
            0, 5, 4, 
            0, 1, 5,
            3, 6, 2, 
            3, 7, 6,
            1, 2, 5, 
            2, 6, 5,
            3, 4, 7, 
            0, 4, 3
        };
        mesh.RecalculateNormals();
        return mesh;
    }

    private Material CreateCubeMaterial(Color color)
    {
        Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        //material.SetFloat("_Cull", (float)CullMode.Off);
        material.color = color;
        return material;
    }
}
