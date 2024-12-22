// using System.Collections.Generic;
//
// namespace Constructor.Units
// {
//     public class UnitBody : Module
//     {
//         public override EModuleType Type => EModuleType.Body;
//
//
//         private ModulePlace[] modulesPlaces;
//         private ModulePlace[] ModulesPlaces
//         {
//             get
//             {
//                 if (modulesPlaces == null)
//                 {
//                     modulesPlaces = GetComponentsInChildren<ModulePlace>();
//                 }
//
//                 return modulesPlaces;
//             }
//         }
//
//
//         public override void Init(Unit unit)
//         {
//             base.Init(unit);
//
//             for (int i = 0; i < modulesPlaces.Length; i++)
//             {
//                 if (modulesPlaces[i].Module != null)
//                 {
//                     modulesPlaces[i].Module.Init(unit);
//                 }
//             }
//         }
//
//
//         public List<Module> GetModules()
//         {
//             List<Module> modules = new List<Module>(ModulesPlaces.Length);
//
//             for (int i = 0; i < ModulesPlaces.Length; i++)
//             {
//                 if (ModulesPlaces[i].Module != null)
//                 {
//                     modules.Add(ModulesPlaces[i].Module);
//                 }
//             }
//
//             return modules;
//         }
//
//         public void SetModule(Module module)
//         {
//             for (int i = 0; i < ModulesPlaces.Length; i++)
//             {
//                 if (ModulesPlaces[i].Type == module.Type)
//                 {
//                     ModulesPlaces[i].Module = module;
//                 }
//             }
//         }
//     }
// }