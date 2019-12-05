using MainApplication.Serrvices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MainApplication.Services
{
    public class ModuleManager
    {
        public event Action OnModulesLoaded;

        public IEnumerable<Assembly> Modules { get; private set; }
        public bool Loaded { get; private set; }
        private PluginLoadContext loadContext;

        public void LoadModules()
        {
            string relativePath = @"Module1\bin\debug\netstandard2.0\Module1.dll";

            string root = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

            string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            loadContext = new PluginLoadContext(pluginLocation);
            var module1Assembly = loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));

            Modules = new[] { module1Assembly };

            Loaded = true;
            OnModulesLoaded?.Invoke();
        }

        public void UnloadModules()
        {
            loadContext.Unload();

            Modules = null;

            Loaded = false;
            OnModulesLoaded?.Invoke();
        }
    }
}