using UnityEngine;

namespace Constructor.Structures
{
    public class BlockModulePlace : MonoBehaviour
    {
        [SerializeField] private Module module;

        public Module Module
        {
            get
            {
                return module;
            }
            set
            {
                if(value == null)
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
                }

                module = value;
            }
        }
    }
}