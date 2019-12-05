using System.Diagnostics;

namespace MainApplication.Routing
{
    public partial class CustomRouter
    {
        [DebuggerDisplay("{TemplateText}")]
        internal class RouteTemplate
        {
            public RouteTemplate(string templateText, TemplateSegment[] segments)
            {
                TemplateText = templateText;
                Segments = segments;
            }

            public string TemplateText { get; }

            public TemplateSegment[] Segments { get; }
        }

    }
}