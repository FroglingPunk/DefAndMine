using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class WiringView
{
    public WiringView(LineRenderer wiringLineTemplate)
    {

    }
}

public class WiringSource
{
    public IReadOnlyReactiveProperty<Cell> Cells => _cells;

    private readonly ReactiveProperty<Cell> _cells = new();
}


public class PowerSource
{

}