using System.Collections.Generic;
using UnityEngine;

public abstract class InstallerBase : MonoBehaviour
{
    public abstract List<IController> RegisterControllers();
}