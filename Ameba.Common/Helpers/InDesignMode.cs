using System.ComponentModel;
using System.Windows;

namespace Ameba.Common.Helpers
{
    public static class InDesignMode
    {
        public static bool Check()
        {
#if SILVERLIGHT
            return DesignerProperties.IsInDesignTool;
#else
            var prop = DesignerProperties.IsInDesignModeProperty;
            return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
#endif
        }
    }
}
