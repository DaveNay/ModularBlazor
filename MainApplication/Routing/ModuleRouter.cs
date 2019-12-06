using MainApplication.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MainApplication.Routing
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

        private async void OnModulesChanged(IEnumerable<Assembly> modules)
        {
            var dict = new Dictionary<string, object>
            {
                { "AdditionalAssemblies", modules }
            };

            var pv = ParameterView.FromDictionary(dict);
            await SetParametersAsync(pv);
        }
    }
}