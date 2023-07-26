using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.PlasticSCM.Editor.WebApi;

public class LifeGame : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int _row;
    [SerializeField] int _column;
    [SerializeField] GameObject _cellPrefab;
    [SerializeField, Range(0, 10)] float _interval = 5;
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
    bool _isStart;
    float _timer;

    public bool IsStart => _isStart;
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
                cell.name = $"{r} {c}";
                _cells[r, c] = cell.GetComponent<Cell>();
            }
        }
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_isStart)
        {
            if (_timer > _interval)
            {
                for (int r = 0; r < _row; r++)
                {
                    for (int c = 0; c < _column; c++)
                    {
                        _cells[r, c].GroupCount = 0;
                        ChangeState(r, c);
                    }
                }
                _timer = 0;
            }
        }
    }
    public void StartFlag()
    {
        _isStart = true;
    }
    public void ChangeState(int r, int c)
    {
        foreach (var dir in _direction)
        {
            CheckCell(r + dir.x, c + dir.y, r, c);
        }
    }
    void CheckCell(int r, int c, int baseR, int baseC)
    {
        if (r >= 0 && r < _row && c >= 0 && c < _column)
        {
            if (_cells[r, c].State == Cell.LifeState.Alive)
            {
                _cells[baseR, baseC].GroupCount++;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Cell clickCell = eventData.pointerCurrentRaycast.gameObject.GetComponent<Cell>();
        if(clickCell)
        {
            clickCell.State = Cell.LifeState.Alive;
        }
    }
}
