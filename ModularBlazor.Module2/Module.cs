using ModularBlazor.Shared;

namespace ModularBlazor.Module1
{
    public class Module : IModule
    {
        public string Description => "The second module";
        public string Name => "Module 2";
        public string NavigationLinkName => "Module dos";
        public string URL => "Module2/Home";
    }
}