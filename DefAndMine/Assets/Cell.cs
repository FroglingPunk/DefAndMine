using UnityEngine;
using System.Collections;
using System;

public class Cell : MonoBehaviour
{
    private CellWiring wiring;
    private CellPower power;

    public void SetWiring(bool value)
    {
        wiring.Value = value;
    }

    public void SetPower(int value)
    {
        power.Value = value;
    }


    public Field Field;
    public int IDx;
    public int IDz;

    private bool _hasPower;
    private bool _hasPower_isDirty;

    private Cell[] neighbors;

    public bool HasPower
    {
        get
        {
            return _hasPower;
        }
        set
        {
            if (!_hasPower_isDirty)
            {
                _hasPower_isDirty = true;
                StartCoroutine(HasPowerValueCleaner());

                _hasPower = value;

                for (EDirection direction = EDirection.N; direction <= EDirection.W; direction++)
                {
                    if (HasNeighbor(direction))
                    {
                        GetNeighbor(direction).OnNeighborCellPowerChanged(this);
                    }
                }

                RebuildPowerConductors();
            }
        }
    }

    public int _power;
    public int Power
    {
        get
        {
            return _power;
        }
        set
        {
            int oldValue = _power;

            _power = value;


            if (oldValue == 0 && value > 0)
            {
                HasPower = true;
            }
            else if (value == 0)
            {
                HasPower = false;
            }
        }
    }



    [SerializeField]
    private Transform powerX_trans;
    [SerializeField]
    private Transform powerZ_trans;

    private void RebuildPowerConductors()
    {
        if (_hasPower)
        {
            bool[] neighborsHasPower =
            {
                        NeighborHasPower(EDirection.N),
                        NeighborHasPower(EDirection.E),
                        NeighborHasPower(EDirection.S),
                        NeighborHasPower(EDirection.W)
                    };

            if (neighborsHasPower[(int)EDirection.E] && neighborsHasPower[(int)EDirection.W])
            {
                powerX_trans.localScale = new Vector3(1f, 0.1f, 0.1f);
                powerX_trans.localPosition = Vector3.zero;
            }
            else if (neighborsHasPower[(int)EDirection.E])
            {
                powerX_trans.localScale = new Vector3(0.55f, 0.1f, 0.1f);
                powerX_trans.localPosition = new Vector3(0.225f, 0f, 0f);
            }
            else if (neighborsHasPower[(int)EDirection.W])
            {
                powerX_trans.localScale = new Vector3(0.55f, 0.1f, 0.1f);
                powerX_trans.localPosition = new Vector3(-0.225f, 0f, 0f);
            }
            else
            {
                powerX_trans.localScale = new Vector3(0.3f, 0.1f, 0.3f);
                powerX_trans.localPosition = Vector3.zero;
            }

            if (neighborsHasPower[(int)EDirection.N] && neighborsHasPower[(int)EDirection.S])
            {
                powerZ_trans.localScale = new Vector3(0.1f, 0.1f, 1f);
                powerZ_trans.localPosition = Vector3.zero;
            }
            else if (neighborsHasPower[(int)EDirection.N])
            {
                powerZ_trans.localScale = new Vector3(0.1f, 0.1f, 0.55f);
                powerZ_trans.localPosition = new Vector3(0f, 0f, 0.225f);
            }
            else if (neighborsHasPower[(int)EDirection.S])
            {
                powerZ_trans.localScale = new Vector3(0.1f, 0.1f, 0.55f);
                powerZ_trans.localPosition = new Vector3(0f, 0f, -0.225f);
            }
            else
            {
                powerZ_trans.localScale = new Vector3(0.3f, 0.1f, 0.3f);
                powerZ_trans.localPosition = Vector3.zero;
            }
        }
        else
        {
            powerX_trans.localScale = Vector3.zero;
            powerZ_trans.localScale = Vector3.zero;
        }
    }

    IEnumerator HasPowerValueCleaner()
    {
        yield return null;
        _hasPower_isDirty = false;
    }

    public void Init(Field field, int id_x, int id_z)
    {
        Field = field;
        IDx = id_x;
        IDz = id_z;

        neighbors = new Cell[4];

        Power = 0;
    }

    public void SwitchPower()
    {
        HasPower = !HasPower;
    }

    public void ChainDisablePower()
    {
        if (!_hasPower_isDirty && HasPower)
        {
            HasPower = false;

            for (EDirection direction = EDirection.N; direction <= EDirection.W; direction++)
            {
                if (HasNeighbor(direction))
                {
                    GetNeighbor(direction).ChainDisablePower();
                }
            }
        }
    }

    public void OnNeighborCellPowerChanged(Cell cell)
    {
        RebuildPowerConductors();
    }

    public bool HasNeighbor(EDirection direction)
    {
        return neighbors[(int)direction] != null;
    }

    public Cell GetNeighbor(EDirection direction)
    {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(Cell neighbor, EDirection direction)
    {
        neighbors[(int)direction] = neighbor;
        neighbor.neighbors[(int)direction.Opposite()] = this;
    }

    public bool NeighborHasPower(EDirection direction)
    {
        Cell neighbor = GetNeighbor(direction);
        return neighbor != null && neighbor.HasPower;
    }
}

//public class CellPower
//{


//    public readonly Cell Cell;

//    private bool _hasPower;
//    private bool _hasPower_isDirty;

//    public bool HasPower
//    {
//        get
//        {
//            return _hasPower;
//        }
//        set
//        {
//            if (!_hasPower_isDirty)
//            {
//                _hasPower_isDirty = true;
//                Cell.StartCoroutine(HasPowerValueCleaner());

//                _hasPower = value;

//                for (EDirection direction = EDirection.N; direction <= EDirection.W; direction++)
//                {
//                    if (Cell.HasNeighbor(direction))
//                    {
//                        Cell.GetNeighbor(direction).OnNeighborCellPowerChanged(Cell);
//                    }
//                }

//                RebuildPowerConductors();
//            }
//        }
//    }

//    public int _value;
//    public int Value
//    {
//        get
//        {
//            return _value;
//        }
//        set
//        {
//            int oldValue = _value;

//            _value = value;


//            if (oldValue == 0 && value > 0)
//            {
//                HasPower = true;
//            }
//            else if (value == 0)
//            {
//                HasPower = false;
//            }
//        }
//    }



//    [SerializeField]
//    private Transform powerX_trans;
//    [SerializeField]
//    private Transform powerZ_trans;

//    private void RebuildPowerConductors()
//    {
//        if (_hasPower)
//        {
//            bool[] neighborsHasPower =
//            {
//                        NeighborHasPower(EDirection.N),
//                        NeighborHasPower(EDirection.E),
//                        NeighborHasPower(EDirection.S),
//                        NeighborHasPower(EDirection.W)
//                    };

//            if (neighborsHasPower[(int)EDirection.E] && neighborsHasPower[(int)EDirection.W])
//            {
//                powerX_trans.localScale = new Vector3(1f, 0.1f, 0.1f);
//                powerX_trans.localPosition = Vector3.zero;
//            }
//            else if (neighborsHasPower[(int)EDirection.E])
//            {
//                powerX_trans.localScale = new Vector3(0.55f, 0.1f, 0.1f);
//                powerX_trans.localPosition = new Vector3(0.225f, 0f, 0f);
//            }
//            else if (neighborsHasPower[(int)EDirection.W])
//            {
//                powerX_trans.localScale = new Vector3(0.55f, 0.1f, 0.1f);
//                powerX_trans.localPosition = new Vector3(-0.225f, 0f, 0f);
//            }
//            else
//            {
//                powerX_trans.localScale = new Vector3(0.3f, 0.1f, 0.3f);
//                powerX_trans.localPosition = Vector3.zero;
//            }

//            if (neighborsHasPower[(int)EDirection.N] && neighborsHasPower[(int)EDirection.S])
//            {
//                powerZ_trans.localScale = new Vector3(0.1f, 0.1f, 1f);
//                powerZ_trans.localPosition = Vector3.zero;
//            }
//            else if (neighborsHasPower[(int)EDirection.N])
//            {
//                powerZ_trans.localScale = new Vector3(0.1f, 0.1f, 0.55f);
//                powerZ_trans.localPosition = new Vector3(0f, 0f, 0.225f);
//            }
//            else if (neighborsHasPower[(int)EDirection.S])
//            {
//                powerZ_trans.localScale = new Vector3(0.1f, 0.1f, 0.55f);
//                powerZ_trans.localPosition = new Vector3(0f, 0f, -0.225f);
//            }
//            else
//            {
//                powerZ_trans.localScale = new Vector3(0.3f, 0.1f, 0.3f);
//                powerZ_trans.localPosition = Vector3.zero;
//            }
//        }
//        else
//        {
//            powerX_trans.localScale = Vector3.zero;
//            powerZ_trans.localScale = Vector3.zero;
//        }
//    }

//    IEnumerator HasPowerValueCleaner()
//    {
//        yield return null;
//        _hasPower_isDirty = false;
//    }

//    public CellPower(Cell cell, int startPowerValue = 0)
//    {
//        Cell = cell;
//        Value = startPowerValue;
//    }

//    public void ChainDisablePower()
//    {
//        if (!_hasPower_isDirty && HasPower)
//        {
//            HasPower = false;

//            for (EDirection direction = EDirection.N; direction <= EDirection.W; direction++)
//            {
//                if (Cell.HasNeighbor(direction))
//                {
//                    Cell.GetNeighbor(direction).ChainDisablePower();
//                }
//            }
//        }
//    }

//    private void OnNeighborCellPowerChanged(Cell cell)
//    {
//        RebuildPowerConductors();
//    }

//    public bool NeighborHasPower(EDirection direction)
//    {
//        Cell neighbor = Cell.GetNeighbor(direction);
//        return neighbor != null && neighbor.HasPower;
//    }
//}

public class CellPower : NotifyingVariable<int>
{
}

public class CellWiring : NotifyingVariable<bool>
{
}

public class NotifyingVariable<T>
{
    private T _value;
    public T Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }

    public event Action<T> OnValueChanged;
}