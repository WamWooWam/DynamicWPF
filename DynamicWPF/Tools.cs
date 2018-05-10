using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DynamicWPF
{
    internal static class Tools
    {
        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    if (child is FlowDocumentScrollViewer v) // Why the fuck is there no generic "FlowDocumentControl" or smth
                    {
                        foreach (T childOfChild in FindDocumentChildren<T>(v.Document.Blocks))
                        {
                            yield return childOfChild;
                        }
                    }
                    else
                    {
                        foreach (T childOfChild in child.FindVisualChildren<T>())
                        {
                            yield return childOfChild;
                        }
                    }
                }
            }
        }

        public static IEnumerable<T> FindDocumentChildren<T>(BlockCollection blocks)
        {
            foreach (var childOfChild in blocks)
            {
                if (childOfChild is T t)
                {
                    yield return t;
                }
                else if (childOfChild is Paragraph p)
                {
                    foreach (var childOfChildOfChild in p.Inlines)
                    {
                        if (childOfChildOfChild is T t2)
                        {
                            yield return t2;
                        }
                    }
                }
                else if(childOfChild is Section s)
                {
                    foreach(var thing in FindDocumentChildren<T>(s.Blocks))
                    {
                        yield return thing;
                    }
                }
            }
        }

        public static T FindChild<T>(this DependencyObject parent) where T : UIElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    return child as T;
                }
            }

            return null;
        }

        public static void SetBrowserEmulationMode()
        {
            string fileName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
            if (String.Compare(fileName, "devenv.exe", true) != 0 && String.Compare(fileName, "XDesProc.exe", true) != 0)
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (uint.Parse(key.GetValue(fileName, 0u).ToString()) != 11000u)
                        key.SetValue(fileName, 11000u, RegistryValueKind.DWord);
                }
            }
        }
    }
}
