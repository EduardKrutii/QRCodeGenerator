using System;
using System.Windows;
using Microsoft.Win32;

namespace QRCodeGenerator.UI
{
    public enum Theme { Light, Dark }

    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var systemTheme = ThemeManager.GetSystemTheme();
            ThemeManager.ApplyTheme(System.Windows.Application.Current, systemTheme);
        }
    }

    public static class ThemeManager
    {
        private const string LightThemePath = "Themes/Light.xaml";
        private const string DarkThemePath = "Themes/Dark.xaml";

        public static void ApplyTheme(System.Windows.Application app, string theme)
        {
            var themePath = theme == "Dark" ? DarkThemePath : LightThemePath;

            var existingDictionaries = app.Resources.MergedDictionaries
                .Where(d => d.Source?.OriginalString == LightThemePath || d.Source?.OriginalString == DarkThemePath)
                .ToList();

            foreach (var dictionary in existingDictionaries)
                app.Resources.MergedDictionaries.Remove(dictionary);

            var newDict = new ResourceDictionary { Source = new Uri(themePath, UriKind.Relative) };
            app.Resources.MergedDictionaries.Add(newDict);
        }

        public static string GetSystemTheme()
        {
            var isDark = Microsoft.Win32.Registry.GetValue(
                @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
                "AppsUseLightTheme",
                1) is int value && value == 0;

            return isDark ? "Dark" : "Light";
        }
    }
}