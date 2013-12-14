using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public GameObject cellPrefab;
	public Vector2 size = new Vector2(5, 5);
	public Cell[][] cells;

	void Start () {
		cells = new Cell[(int)size.x][];
		transform.position = new Vector3(Mathf.Floor(size.x / 2) * -1, 0, Mathf.Floor(size.y / 2) * -1);
		for (int x = 0; x < size.x; x++) {
			cells[x] = new Cell[(int)size.y];
			for (int y = 0; y < size.y; y++) {
				GameObject cell = (GameObject)Instantiate(cellPrefab);
				cell.transform.parent = transform;
				cell.transform.localPosition = new Vector3(x, 0, y);
				cells[x][y] = cell.GetComponent<Cell>();
				cells[x][y].x = x;
				cells[x][y].y = y;
			}
		}
	}
}
