using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIControllerView : MonoControllerBase
{
    public abstract IUIControllerModel CreateModel();
}