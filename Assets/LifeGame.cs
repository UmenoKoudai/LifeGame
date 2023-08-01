using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LifeGame : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int _row;
    [SerializeField] int _column;
    [SerializeField] GameObject _cellPrefab;
    [SerializeField, Range(0, 10)] float _interval;
    [SerializeField] Slider _slider;
    Test<int> test = new Test<int>(10, 10);
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
    int[,] _changeCount;
    bool _isStart;
    float _timer;

    public bool IsStart => _isStart;
    public int Row => _row;
    public int Column => _column;

    void Start()
    {
        var a = test;
        _slider.maxValue = 10;
        _cells = new Cell[_row, _column];
        _changeCount = new int[_row, _column];
        GetComponent<GridLayoutGroup>().constraintCount = _column;
        for(int r = 0; r < _row; r++)
        {
            for(int c = 0; c < _column; c++)
            {
                var cell = Instantiate(_cellPrefab, transform);
                cell.transform.GetChild(0).GetComponent<Text>().text = r.ToString();
                cell.transform.GetChild(1).GetComponent<Text>().text = c.ToString();
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
            if (_timer > _interval - _slider.value)
            {
                for (int r = 0; r < _row; r++)
                {
                    for (int c = 0; c < _column; c++)
                    {
                        ChangeState(r, c);
                    }
                } 
                for(int r = 0; r < _row; r++)
                {
                    for(int c = 0; c < _column; c++)
                    {
                        _cells[r, c].GroupCount = _changeCount[r, c];
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

    public void StopFlag()
    {
        _isStart = false;
    }

    public void ChangeState(int r, int c)
    {
        int aliveCount = 0;
        foreach (var dir in _direction)
        {
            int nextR = r + dir.x;
            int nextC = c + dir.y;
            if (nextR >= 0 && nextR < _row && nextC >= 0 && nextC < _column)
            {
                if (_cells[nextR, nextC].State == Cell.LifeState.Alive)
                {
                    aliveCount++;
                }
            }
        }
        _changeCount[r, c] = aliveCount;
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
