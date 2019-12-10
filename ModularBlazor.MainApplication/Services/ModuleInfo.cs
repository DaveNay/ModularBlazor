using ModularBlazor.Shared;
using System;
using System.IO;
using System.Reflection;

namespace ModularBlazor.MainApplication.Services
{
    public class ModuleInfo
    {
        private IModule _module;
        private PluginLoadContext _context;

        public string Name => _module?.Name;

        public string Description => _module?.Description;

        public string NavigationLinkName => _module?.NavigationLinkName;

        public string URL => _module?.URL;

        public Assembly Assembly { get; private set; }

        private ModuleInfo(IModule module, Assembly assembly, PluginLoadContext context)
        {
            _module = module;
            Assembly = assembly;
            _context = context;
        }

        public static bool TryCreateModule(string modulePath, out ModuleInfo moduleInfo)
        {
            string root = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

            string pluginLocation = Path.GetFullPath(Path.Combine(root, modulePath.Replace('\\', Path.DirectorySeparatorChar)));

            var context = new PluginLoadContext(pluginLocation);
            var assembly = context.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IModule).IsAssignableFrom(type))
                {
                    if (Activator.CreateInstance(type) is IModule module)
                    {
                        moduleInfo = new ModuleInfo(module, assembly, context);
                        return true;
                    }
                }
            }

            // The .dll does not contain a class implementing IModule so it must not be one of our modules.
            moduleInfo = null;
            return false;
        }

        internal void Unload()
        {
            _context.Unload();
        }
    }
}