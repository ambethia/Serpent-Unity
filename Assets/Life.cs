using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Life : MonoBehaviour {

	public bool isRunning = false;
	public bool isEvolving = false;

	private Grid grid;
	
	void Awake () {
		grid = GetComponent<Grid>();
	}
	
	void Update () {
		if (Input.GetKeyDown("space"))
			isRunning = !isRunning;
		if (isRunning && !isEvolving)
			StartCoroutine(Evolve());
	}

	IEnumerator Evolve() {
		List<Cell> activateQueue = new List<Cell>();
		List<Cell> deactivateQueue = new List<Cell>();

		isEvolving = true;
		foreach (Cell[] row in grid.cells)
		{
			foreach (Cell cell in row)
			{
				if (WillLive(cell)) {
					activateQueue.Add(cell);
				} else {
					deactivateQueue.Add(cell);
				}
			}
		}

		foreach (Cell cell in activateQueue) {
			if (!cell.isActive)
				cell.Activate();
		}

		foreach(Cell cell in deactivateQueue) {
			if (cell.isActive)
				cell.Deactivate();
		}

		yield return new WaitForSeconds(0.2f);
		isEvolving = false;
	}

	bool WillLive(Cell cell) {
		int neighbors = SumOfNeighbors(cell);
		if (cell.isActive) {
			return neighbors == 2 || neighbors == 3;
		} else {
			return neighbors == 3;
		}
	}

	int SumOfNeighbors(Cell cell) {
		int neighbors = 0;
		foreach (int u in Enumerable.Range(-1, 3)) {
			foreach (int v in Enumerable.Range(-1, 3)) {
				int x = cell.x + u;
				int y = cell.y + v;

				if (x >= 0 && y >= 0 &&
				    !(u == 0 && v == 0) &&
				    x < grid.cells.Length &&
				    y < grid.cells[x].Length &&
				    grid.cells[x][y].isActive) {
					neighbors += 1;
				}
			}
		}
		return neighbors;
	}
}
