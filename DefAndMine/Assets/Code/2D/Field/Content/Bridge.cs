using UnityEngine;

public class Bridge : MonoCellContentBase
{
    public override ECellContent Type => ECellContent.Bridge;
    public override bool IsSolid => false;


    public override void Init(Cell cell, EDirection direction)
    {
        base.Init(cell, direction);

        transform.position += new Vector3(0, 0.2f, 0f);
    }
}