using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Waves : MonoBehaviour
{
    [Header("Referenced GameObjects")]
    [SerializeField] private Transform updateLocation = null;

    [Header("Settings")]
    [SerializeField] private int width = 10;
    [SerializeField] private int depth = 1500;
    [SerializeField] private int updateDepth = 10;
    [SerializeField] private float UVScale = 2f;
    [SerializeField] private Octave[] Octaves = null;

    protected Vector3[] verts;

    //Mesh
    protected MeshFilter MeshFilter;
    protected Mesh Mesh;

    // Start is called before the first frame update
    void Start()
    {
        ConstructMesh();
    }

#if UNITY_EDITOR
    [ContextMenu("Construct wave mesh")]
    public void ConstructMeshInEditor()
    {
        //Mesh Setup
        Mesh = new Mesh();
        Mesh.name = gameObject.name;
        verts = GenerateVerts();
        Mesh.vertices = GenerateVerts();
        Mesh.triangles = GenerateTries();
        Mesh.uv = GenerateUVs();
        UpdateWaves(Vector3.zero, depth);
        Mesh.RecalculateNormals();
        Mesh.RecalculateBounds();
        MeshFilter = gameObject.GetComponent<MeshFilter>();
        MeshFilter.mesh = Mesh;
    }

#endif

    public void ConstructMesh()
    {
        //Mesh Setup
        Mesh = new Mesh();
        Mesh.name = gameObject.name;
        verts = GenerateVerts();
        Mesh.vertices = GenerateVerts();
        Mesh.triangles = GenerateTries();
        Mesh.uv = GenerateUVs();
        UpdateWaves(Vector3.zero, depth);
        Mesh.RecalculateNormals();
        Mesh.RecalculateBounds();
        MeshFilter = gameObject.GetComponent<MeshFilter>();
        MeshFilter.mesh = Mesh;
    }


    public float GetHeight(Vector3 position)
    {
        //scale factor and position in local space
        var scale = new Vector3(1 / transform.lossyScale.x, 0, 1 / transform.lossyScale.z);
        var localPos = Vector3.Scale((position - transform.position), scale);

        //get edge points
        var p1 = new Vector3(Mathf.Floor(localPos.x), 0, Mathf.Floor(localPos.z));
        var p2 = new Vector3(Mathf.Floor(localPos.x), 0, Mathf.Ceil(localPos.z));
        var p3 = new Vector3(Mathf.Ceil(localPos.x), 0, Mathf.Floor(localPos.z));
        var p4 = new Vector3(Mathf.Ceil(localPos.x), 0, Mathf.Ceil(localPos.z));

        //clamp if the position is outside the plane
        p1.x = Mathf.Clamp(p1.x, 0, width);
        p1.z = Mathf.Clamp(p1.z, 0, depth);
        p2.x = Mathf.Clamp(p2.x, 0, width);
        p2.z = Mathf.Clamp(p2.z, 0, depth);
        p3.x = Mathf.Clamp(p3.x, 0, width);
        p3.z = Mathf.Clamp(p3.z, 0, depth);
        p4.x = Mathf.Clamp(p4.x, 0, width);
        p4.z = Mathf.Clamp(p4.z, 0, depth);

        //get the max distance to one of the edges and take that to compute max - dist
        var max = Mathf.Max(Vector3.Distance(p1, localPos), Vector3.Distance(p2, localPos), Vector3.Distance(p3, localPos), Vector3.Distance(p4, localPos) + Mathf.Epsilon);
        var dist = (max - Vector3.Distance(p1, localPos))
                 + (max - Vector3.Distance(p2, localPos))
                 + (max - Vector3.Distance(p3, localPos))
                 + (max - Vector3.Distance(p4, localPos) + Mathf.Epsilon);
        //weighted sum
        var height = verts[index(p1.x, p1.z)].y * (max - Vector3.Distance(p1, localPos))
                   + verts[index(p2.x, p2.z)].y * (max - Vector3.Distance(p2, localPos))
                   + verts[index(p3.x, p3.z)].y * (max - Vector3.Distance(p3, localPos))
                   + verts[index(p4.x, p4.z)].y * (max - Vector3.Distance(p4, localPos));

        //scale
        return height * transform.lossyScale.y / dist;
    }

    private Vector3[] GenerateVerts()
    {
        var verts = new Vector3[(width + 1) * (depth + 1)];

        //equaly distributed verts
        for (int x = 0; x <= width; x++)
            for (int z = 0; z <= depth; z++)
                verts[index(x, z)] = new Vector3(x, 0, z);

        return verts;
    }

    private int[] GenerateTries()
    {
        var tries = new int[verts.Length * 6];

        //two triangles are one tile
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                tries[index(x, z) * 6 + 0] = index(x, z);
                tries[index(x, z) * 6 + 1] = index(x + 1, z + 1);
                tries[index(x, z) * 6 + 2] = index(x + 1, z);
                tries[index(x, z) * 6 + 3] = index(x, z);
                tries[index(x, z) * 6 + 4] = index(x, z + 1);
                tries[index(x, z) * 6 + 5] = index(x + 1, z + 1);
            }
        }

        return tries;
    }

    private Vector2[] GenerateUVs()
    {
        var uvs = new Vector2[verts.Length];

        //always set one uv over n tiles than flip the uv and set it again
        for (int x = 0; x <= width; x++)
        {
            for (int z = 0; z <= depth; z++)
            {
                var vec = new Vector2((x / UVScale) % 2, (z / UVScale) % 2);
                uvs[index(x, z)] = new Vector2(vec.x <= 1 ? vec.x : 2 - vec.x, vec.y <= 1 ? vec.y : 2 - vec.y);
            }
        }

        return uvs;
    }

    private int index(int x, int z)
    {
        return x * (depth + 1) + z;
    }

    private int index(float x, float z)
    {
        return index((int)x, (int)z);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWaves(updateLocation.position, updateDepth);
    }

    void UpdateWaves(Vector3 position, int updateDepth)
    {
        var scale = new Vector3(1 / transform.lossyScale.x, 0, 1 / transform.lossyScale.z);
        var localPosStart = Vector3.Scale((position - transform.position), scale);

        // Handle scaling
        int zStart = Mathf.RoundToInt(localPosStart.z);
        int zEnd = Mathf.RoundToInt(Mathf.Min(localPosStart.z + updateDepth, depth));


        //var verts = Mesh.vertices;
        for (int x = 0; x <= width; x++)
        {
            for (int z = zStart; z <= zEnd; z++)
            {
                var y = 0f;
                for (int o = 0; o < Octaves.Length; o++)
                {

                    var perl = Mathf.PerlinNoise((x * Octaves[o].scale.x + Time.time * Octaves[o].speed.x) / width, (z * Octaves[o].scale.y + Time.time * Octaves[o].speed.y) / depth) - 0.5f;
                    y += perl * Octaves[o].height;

                }

                verts[index(x, z)] = new Vector3(x, y, z);
            }
        }
        Mesh.vertices = verts;
        //Mesh.RecalculateNormals();

    }

    [System.Serializable]
    public struct Octave
    {
        public Vector2 speed;
        public Vector2 scale;
        public float height;
        public bool alternate;
    }
}
