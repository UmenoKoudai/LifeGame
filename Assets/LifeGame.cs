using UnityEngine;
using UnityEngine.UI;
using static Cell;

public class LifeGame : MonoBehaviour
{
    [SerializeField] int _row;
    [SerializeField] int _column;
    [SerializeField] GameObject _cellPrefab;
    Position[] _direction = {
        new Position( 1, 0 ),
        new Position( -1, 0 ),
        new Position( 0, 1 ),
        new Position( 0, -1 ),
        new Position( 1, 1 ),
        new Position( -1, -1 ),
        new Position( 1, -1 ),
        new Position( -1, 1 ),
    };
    Cell[,] _cells;

    public int Row => _row;
    public int Column => _column;

    void Start()
    {
        _cells = new Cell[_row, _column];
        GetComponent<GridLayoutGroup>().constraintCount = _column;
        for(int r = 0; r < _row; r++)
        {
            for(int c = 0; c < _column; c++)
            {
                var cell = Instantiate(_cellPrefab, transform);
                _cells[r, c] = cell.GetComponent<Cell>();
            }
        }
    }

    void Update()
    {
        
    }

    public void ChangeState(int r, int c, Cell[,] cells)
    {
        int count = 0;
        foreach (var dir in _direction)
        {
            CheckCell(r + dir.x, c + dir.y, cells);
        }
    }
    void CheckCell(int r, int c, Cell[,] cells)
    {
        if (r >= 0 && r < _row && c >= 0 && c < _column)
        {
            if (_cells[r, c].State == LifeState.Dead)
            {
                _cells[r, c].GroupCount++;
            }
        }
    }
}
