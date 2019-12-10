using ModularBlazor.MainApplication.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModularBlazor.MainApplication.Routing
{
    public class ModuleRouter : Router, IComponent, IDisposable
    {
        [Inject] public ModuleManager ModuleManager { get; set; }

        public new void Attach(RenderHandle renderHandle)
        {
            base.Attach(renderHandle);
            ModuleManager.OnModulesChanged += OnModulesChanged;
        }

        public new void Dispose()
        {
            base.Dispose();
            ModuleManager.OnModulesChanged -= OnModulesChanged;
        }

        private async void OnModulesChanged(IEnumerable<ModuleInfo> modules)
        {
            var assemblies = modules.Select(module => module.Assembly);

            var dict = new Dictionary<string, object>
            {
                { "AdditionalAssemblies", assemblies }
            };

            var pv = ParameterView.FromDictionary(dict);
            await SetParametersAsync(pv);
        }
    }
}