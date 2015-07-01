using UnityEngine;
using System.Collections;

public class Chessboard : MonoBehaviour
{

	public int m_iSize = 10;
	GameObject[,] m_Grid;

	// Use this for initialization
	void Start ()
	{

		m_Grid = new GameObject[m_iSize, m_iSize];

		for (int i = 0; i <m_iSize; i++)
			for (int j = 0; j <m_iSize; j++) {
				GameObject kachel = GameObject.CreatePrimitive (PrimitiveType.Quad);
				m_Grid [i, j] = kachel;
				kachel.transform.position = new Vector3 (i, j, 0);
				kachel.transform.parent = this.transform;	
	
				kachel.name = "kachel(" + i + "," + j + ")";

				if (Random.value < 0.5) {
					kachel.GetComponent<Renderer> ().material.color = Color.black;
				}

				Camera.main.transform.position = new Vector3 (m_iSize / 2, m_iSize / 2, -10);
				Camera.main .orthographicSize = m_iSize;
		
		
		
			}

		transform.position = new Vector3 (0.5f, 0.5f, 0);
	
		int iCol = 3;
		int iRow = 3;
		print ("Anzahl Nachbarn (" + iCol + "," + iRow + "): " + GetAliveN (iCol, iRow));	
	}
	  
	void KillAll(){
		for (int iCol = 0; iCol < m_iSize; iCol++) {
			for (int iRow = 0; iRow < m_iSize; iRow++) {
				m_Grid[iCol, iRow].GetComponent<Renderer>().material.color = Color.white;
			}
		}
	}


	int GetAliveN (int _iCol, int _iRow)
	{
		int iAliveNeighbours = 0;
		// nachbarn für Feld 3,8 im Radius 1
		for (int iCol = _iCol-1; iCol <= _iCol+1; iCol++) {
			for(int iRow = _iRow -1; iRow <= _iRow+1; iRow++){
				if(iCol >= 0 && iCol < m_iSize && iRow >= 0 && iRow < m_iSize && //check bounds
				   m_Grid[iCol, iRow].GetComponent<Renderer>().material.color == Color.black)
					iAliveNeighbours ++;
			}
		}
		return iAliveNeighbours;
		/* int iAliveNeighbour = 0;

		int rightCol = _iCol + 1;
		int leftCol = _iCol - 1;
		int upperRow = _iRow + 1;
		int lowerRow = _iRow - 1;

		// right
		if (rightCol < m_iSize && m_Grid [rightCol, _iRow].GetComponent<Renderer> ().material.color == Color.black)
			iAliveNeighbour ++;
		// left
		if (leftCol >= 0 && m_Grid [leftCol, _iRow].GetComponent<Renderer> ().material.color == Color.black)
			iAliveNeighbour ++;
		// up
		if (upperRow < m_iSize && m_Grid [_iCol, upperRow].GetComponent<Renderer> ().material.color == Color.black)
			iAliveNeighbour ++;
		// down
		if (lowerRow >= 0 && m_Grid [_iCol, lowerRow].GetComponent<Renderer> ().material.color == Color.black)
			iAliveNeighbour ++;
		// upper left
		if (leftCol >= 0 && upperRow < m_iSize && m_Grid [leftCol, upperRow].GetComponent<Renderer> ().material.color == Color.black)
			iAliveNeighbour ++; 
		// lower left
		if (leftCol >= 0 && lowerRow >= 0 && m_Grid [leftCol, lowerRow].GetComponent<Renderer> ().material.color == Color.black)
			iAliveNeighbour ++;
		// upper right
		if (rightCol < m_iSize && upperRow < m_iSize && m_Grid [rightCol, upperRow].GetComponent<Renderer> ().material.color == Color.black)
			iAliveNeighbour ++;
		// lower right
		if (rightCol < m_iSize && lowerRow >= 0 && m_Grid [rightCol, lowerRow].GetComponent<Renderer> ().material.color == Color.black)
			iAliveNeighbour ++;

		return iAliveNeighbour;

		*/
	}

	void Toggle(int _iCol, int _iRow){
			if (m_Grid [_iCol, _iRow].GetComponent<Renderer> ().material.color == Color.white)
			m_Grid [_iCol, _iRow].GetComponent<Renderer> ().material.color = Color.black;
		else
			m_Grid [_iCol, _iRow].GetComponent<Renderer> ().material.color = Color.white;
	}

	bool GetAlive(int _iCol, int _iRow){
		return (m_Grid[_iCol, _iRow].GetComponent<Renderer>().material.color == Color.black;
	}



	void ToggleMousePos(){
		Vector3 MouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		int iIndexX = (int)(MouseWorldPos.x);
		int iIndexY =  (int)(MouseWorldPos.y);
		Toggle (iIndexX, iIndexY);
	}

	// Update is called once per frame
	void Update ()
	{ 

		if (Input.GetMouseButtonDown(0)) {
			print (Input.mousePosition);
			//Vector3 Camera.main.ScreenToWorldPoint (Input.mousePosition);
			 
		}
		if (Input.GetKeyDown (KeyCode.K))
			KillAll ();
			

		if (Input.GetKeyDown (KeyCode.Space) == false)
			return;

		int[,] tempNeighbours = new int[m_iSize, m_iSize];

		for (int iCol = 0; iCol < m_iSize; iCol++) {
			for (int iRow = 0; iRow < m_iSize; iRow++) {
				tempNeighbours [iCol, iRow] = GetAliveN (iCol, iRow);
			}
		}

		for (int iCol = 0; iCol < m_iSize; iCol++) {
			for (int iRow = 0; iRow < m_iSize; iRow++) {
				if (tempNeighbours [iCol, iRow] == 3) { 
					m_Grid [iCol, iRow].GetComponent<Renderer> ().material.color = Color.black;
				} else if (tempNeighbours [iCol, iRow] != 2)
					m_Grid [iCol, iRow].GetComponent<Renderer> ().material.color = Color.white;
			}
		}
	}
}
