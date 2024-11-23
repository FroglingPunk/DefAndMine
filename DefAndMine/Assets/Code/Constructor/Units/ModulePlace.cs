using UnityEngine;

namespace Constructor.Units
{
    public class ModulePlace : MonoBehaviour
    {
        [SerializeField] private EModuleType type;
        [SerializeField] private Module module;

        public Module Module
        {
            get
            {
                return module;
            }
            set
            {
                if (value == null)
                {
                    if (module != null)
                    {
                        Destroy(module.gameObject);
                    }
                }
                else
                {
                    value.transform.SetParent(transform);
                    value.transform.localPosition = Vector3.zero;
                    value.transform.localRotation = Quaternion.identity;
                    value.transform.localScale = Vector3.one;
                }

                module = value;
            }
        }

        public EModuleType Type => type;
    }
}