using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ayy
{
    [RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
    public class HexMesh : MonoBehaviour
    {
        Mesh _hexMesh = null;
        List<Vector3> _verices = null;
        List<int> _triangles = null;

        private void Awake()
        {
            GetComponent<MeshFilter>().mesh = _hexMesh = new Mesh();
            _hexMesh.name = "[Hex Mesh]";
            _verices = new List<Vector3>();
            _triangles = new List<int>();
        }

        public void Triangulate(HexCell[] cells)
        {
            _hexMesh.Clear();
            _verices.Clear();
            _triangles.Clear();
            for (int i = 0;i < cells.Length;i++)
            {
                Triangulate(cells[i]);
            }
            _hexMesh.vertices = _verices.ToArray();
            _hexMesh.triangles = _triangles.ToArray();
            _hexMesh.RecalculateNormals();
        }

        private void Triangulate(HexCell cell)
        {
            Vector3 center = cell.transform.localPosition;
            for(int i = 0;i < 6;i++)
            {
                AddTriangle(center,center + HexMetrics.corners[i],center + HexMetrics.corners[i + 1]);
            }
        }

        private void AddTriangle(Vector3 v1,Vector3 v2,Vector3 v3)
        {
            int vertexIndex = _verices.Count;
            _verices.Add(v1);
            _verices.Add(v2);
            _verices.Add(v3);
            _triangles.Add(vertexIndex);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 2);
        }
    }

}
