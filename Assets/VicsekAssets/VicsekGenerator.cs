using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VicsekGenerator : MonoBehaviour
{

    [SerializeField] private float startSize;
    [SerializeField] private GameObject square;
    [SerializeField] private GameObject squares;
    private List<GameObject> squareList;
    [SerializeField] private int squareAmt;

    // Start is called before the first frame update
    void Start()
    {
        initializeSquares();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && squareAmt < 15000)
        {
            createNewSquares(false);
        } else if (Input.GetKeyDown(KeyCode.E) && squareAmt < 15000)
        {
            createNewSquares(true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetSquares();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void initializeSquares()
    {
        squareAmt = 0;
        squares = new GameObject("Squares");
        squareList = new List<GameObject>();
        GameObject startSquare = Instantiate(square, Vector3.zero, Quaternion.identity, squares.transform);
        startSquare.transform.localScale = new Vector3(startSize, startSize, 1);
        squareList.Add(startSquare);
    }

    private void createNewSquares(bool diagonal)
    {
        GameObject[] squareArray = squareList.ToArray();
        foreach(GameObject square in squareArray)
        {
            VicsekSquare VS = square.GetComponent<VicsekSquare>();
            Vector3[] pos;
            if (!diagonal)
                pos = VS.split();
            else
                pos = VS.splitDiagonal();
            for (int i = 0; i < pos.Length; i++)
            {
                GameObject newSquare = Instantiate(square, pos[i], Quaternion.identity, squares.transform);
                newSquare.transform.localScale = square.transform.localScale / 3;
                squareList.Add(newSquare);
            }
            squareList.Remove(square);
            Destroy(square);
        }
        squareAmt = squareList.Count;
    }

    private void resetSquares()
    {
        Destroy(squares);
        initializeSquares();
    }
}
