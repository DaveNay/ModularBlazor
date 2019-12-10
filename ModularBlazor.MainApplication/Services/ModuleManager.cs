using System;
using System.Collections.Generic;

namespace ModularBlazor.MainApplication.Services
{

    public class ModuleManager
    {
        private readonly List<ModuleInfo> _modules = new List<ModuleInfo>();

        public event Action<IEnumerable<ModuleInfo>> OnModulesChanged;

        public bool Loaded { get; private set; }

        public void LoadModules()
        {
            var modules = new List<string>
            {
                @"ModularBlazor.Module1\bin\Debug\netstandard2.0\ModularBlazor.Module1.dll",
                @"ModularBlazor.Module2\bin\Debug\netstandard2.0\ModularBlazor.Module2.dll"
            };

            foreach(var modulePath in modules)
            {
                if(ModuleInfo.TryCreateModule(modulePath, out var moduleInfo))
                {
                    _modules.Add(moduleInfo);
                }
            }

            OnModulesChanged?.Invoke(_modules);
            Loaded = true;
        }

        public void UnloadModules()
        {
            foreach(var module in _modules)
            {
                module.Unload();
            }

            _modules.Clear();

            OnModulesChanged?.Invoke(_modules);
            Loaded = false;
        }
    }
}