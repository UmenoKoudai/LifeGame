using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    LifeState _lifeState = LifeState.Dead;
    int _groupCount = 0;
    public LifeState State
    {
        get => _lifeState;
        set
        {
            _lifeState = value;
            ColorChange();
        }
    }

    public int GroupCount 
    { 
        get => _groupCount; 
        set
        {
            _groupCount = value;
            StateChange();
        }
    }

    private void Start()
    {
        ColorChange();
    }

    void ColorChange()
    {
        if(_lifeState == LifeState.Alive)
        {
            GetComponent<Image>().color = Color.yellow;
        }
        else if(_lifeState == LifeState.Dead)
        {
            GetComponent<Image>().color = Color.gray;
        }
    }

    void StateChange()
    {
        Debug.Log(GroupCount);
        if(_groupCount <= 1)
        {
            State = LifeState.Dead;
        }
        else if(_groupCount <= 3)
        {
            State = LifeState.Alive;
        }
        else
        {
            State = LifeState.Dead;
        }
    }
    public enum LifeState
    {
        Alive,
        Dead,
    }
}
