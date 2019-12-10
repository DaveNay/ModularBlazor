using System;

namespace ModularBlazor.Shared
{
    public interface IModule
    {
        string Name { get; }
        string Description { get; }
        string NavigationLinkName { get; }
        string URL { get; }
    }
}
