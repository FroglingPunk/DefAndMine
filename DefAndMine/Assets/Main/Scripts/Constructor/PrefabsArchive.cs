using System.Collections.Generic;
using UnityEngine;

namespace Contstructor
{
    public class PrefabsArchive : MonoBehaviour
    {
        private static PrefabsArchive instance;
        public static PrefabsArchive Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PrefabsArchive>();

                    if (instance == null)
                    {
                        instance = Instantiate(Resources.Load<PrefabsArchive>("PrefabsArchive"));
                    }

                    DontDestroyOnLoad(instance.gameObject);
                }

                return instance;
            }
        }


        [SerializeField] private Module[] modules;
        [SerializeField] private Block[] blocks;


        public Module GetModule(int id)
        {
            return id < modules.Length ? modules[id] : null;
        }

        public Block GetBlock(int id)
        {
            return id < blocks.Length ? blocks[id] : null;
        }

        public int GetIndex(Module module)
        {
            string moduleName = module.gameObject.name;
            moduleName = moduleName.Remove(moduleName.IndexOf(("(Clone)")));

            for (int i = 0; i < modules.Length; i++)
            {
                if (modules[i].gameObject.name == moduleName)
                {
                    return i;
                }
            }

            return -1;
        }

        public int GetIndex(Block block)
        {
            string blockName = block.gameObject.name;
            blockName = blockName.Remove(blockName.IndexOf(("(Clone)")));

            for(int i = 0; i < blocks.Length; i++)
            {
                if(blocks[i].gameObject.name == blockName)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}