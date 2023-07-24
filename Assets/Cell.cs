using UnityEngine;

public class Cell : MonoBehaviour
{
    LifeState _lifeState;
    int _groupCount;
    public LifeState State => _lifeState;
    public int GroupCount { get => _groupCount; set => _groupCount = value; }

    private void Update()
    {
        
    }
    public enum LifeState
    {
        Alive,
        Dead,
    }
}
