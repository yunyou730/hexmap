using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ayy
{

    public class HexGrid : MonoBehaviour
    {
        public int _width = 6;
        public int _height = 6;

        public HexCell      _cellPrefab = null;
        public Text         _cellLabelPrefab = null;

        HexCell[]   _cells;
        Canvas      _gridCanvas = null;
        HexMesh     _hexMesh = null;

        private void Awake()
        {
            _gridCanvas = GetComponentInChildren<Canvas>();
            _hexMesh = GetComponentInChildren<HexMesh>();
            _cells = new HexCell[_height * _width];

            for (int z = 0,i = 0;z < _height;z++)
            {
                for (int x = 0; x < _width;x++)
                {
                    CreateCell(x, z, i++);
                }
            }
        }

        private void Start()
        {
            _hexMesh.Triangulate(_cells);
        }

        private void CreateCell(int x,int z,int i)
        {
            Vector3 position;
            position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
            position.y = 0f;
            position.z = z * (HexMetrics.outerRadius * 1.5f);

            HexCell cell = _cells[i] = Instantiate<HexCell>(_cellPrefab);
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;


            Text label = Instantiate<Text>(_cellLabelPrefab);
            label.rectTransform.SetParent(_gridCanvas.transform,false);
            label.rectTransform.anchoredPosition = new Vector2(position.x,position.z);
            label.text = x.ToString() + "\n" + z.ToString();
        }
    }

}
