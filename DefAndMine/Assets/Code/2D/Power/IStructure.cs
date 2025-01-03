using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStructure
{
    Cell Cell { get; }

    void Init(Cell cell);
    void Demolish();
    void Rotate(bool clockwise);
}