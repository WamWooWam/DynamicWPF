using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DynamicWPF.Scripting
{
    public class ScriptManager : DependencyObject
    {
        public ScriptManager() : base()
        {
            SetValue(ScriptsProperty, new List<Script>());
        }

        public static readonly DependencyProperty ScriptsProperty = 
            DependencyProperty.RegisterAttached("Scripts", typeof(List<Script>), typeof(ScriptManager), new FrameworkPropertyMetadata(new List<Script>()));

        public static void SetScripts(UIElement element, List<Script> value)
        {
            element.SetValue(ScriptsProperty, value);
        }
        
        public static List<Script> GetScripts(UIElement element)
        {
            return (List<Script>)element.GetValue(ScriptsProperty);
        }
    }

    public class Script : DependencyObject
    {
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(Uri), typeof(Script));

        public Uri Source { get => (Uri)GetValue(SourceProperty); set => SetValue(SourceProperty, value); }
    }
}
