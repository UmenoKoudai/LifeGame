using UnityEngine;

public class Test<T> : MonoBehaviour
{
    public int Row { get; }
    public int Column { get; }
    T[,] _values;

    public Test(int row, int column)
    {
        Row = row;
        Column = column;
        _values = new T[row, column];
    }
    public T this[int row, int column] { get => _values[row, column]; set => _values[row, column] = value; }
}
