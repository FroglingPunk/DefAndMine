using System.Collections.Generic;
using UnityEngine;

namespace Contstructor
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private BlockModulePlace[] modulesPlaces;

        public Structure Structure { get; private set; }
        public bool IsActive => Structure.IsActive;
        public int PowerConsumed
        {
            get
            {
                int powerConsumed = 0;

                for (int i = 0; i < modulesPlaces.Length; i++)
                {
                    if (modulesPlaces[i].Module != null)
                    {
                        powerConsumed += modulesPlaces[i].Module.PowerConsumed;
                    }
                }

                return powerConsumed;
            }
        }
        public int PowerReceived
        {
            get
            {
                int powerReceived = 0;

                for (int i = 0; i < modulesPlaces.Length; i++)
                {
                    if (modulesPlaces[i].Module != null)
                    {
                        powerReceived += modulesPlaces[i].Module.PowerReceived;
                    }
                }

                return powerReceived;
            }
        }


        public List<Module> GetModules()
        {
            List<Module> modules = new List<Module>(modulesPlaces.Length);

            for (int i = 0; i < modulesPlaces.Length; i++)
            {
                if (modulesPlaces[i].Module != null)
                {
                    modules.Add(modulesPlaces[i].Module);
                }
            }

            return modules;
        }
        public int GetModulePlaceID(Module module)
        {
            for (int i = 0; i < modulesPlaces.Length; i++)
            {
                if (modulesPlaces[i].Module == module)
                {
                    return i;
                }
            }

            return -1;
        }

        public void Init(Structure structure)
        {
            Structure = structure;

            for (int i = 0; i < modulesPlaces.Length; i++)
            {
                modulesPlaces[i].Module?.Init(this);
            }
        }
        public void SetModule(Module module, int modulePlaceID)
        {
            modulesPlaces[modulePlaceID].Module = module;
        }
    }
}