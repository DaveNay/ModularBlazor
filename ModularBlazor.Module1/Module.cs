using ModularBlazor.Shared;

namespace ModularBlazor.Module1
{
    public class Module : IModule
    {
        public string Description => "The first module";
        public string Name => "Module 1";
        public string NavigationLinkName => "Mod 1";
        public string URL => "Module1/Home";
    }
}