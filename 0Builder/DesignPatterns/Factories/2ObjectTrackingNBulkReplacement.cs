﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Factories
{
    public class ObjectTrackingNBulkReplacement
    {
        public interface ITheme
        {
            string TextColor { get; }
            string BgrColor { get; }
        }

        private class LightTheme : ITheme
        {
            public string TextColor => "Black";
            public string BgrColor => "White";
        }        

        private class DarkTheme : ITheme
        {
            public string TextColor => "White";
            public string BgrColor => "Black";
        }

        public class TrackingThemeFactory
        {
            private readonly List<WeakReference<ITheme>> themes = new();
            
            public ITheme CreateTheme(bool dark)
            {
                ITheme theme = dark ? new DarkTheme() : new LightTheme();
                themes.Add(new WeakReference<ITheme>(theme));
                return theme;
            }

            public string Info
            {
                get
                {
                    var sb = new StringBuilder();
                    foreach (var weakReference in themes)
                    {
                        if (weakReference.TryGetTarget(out var theme))
                        {
                            bool dark = theme is DarkTheme;
                            sb.Append(dark ? "Dark" : "Light").AppendLine(" theme");
                        }
                    }

                    return sb.ToString();
                }
            }
        }

        public class ReplaceableThemeFactory
        {
            private readonly List<WeakReference<Ref<ITheme>>> themes = new();

            private ITheme createThemeImpl(bool dark)
            {
                return dark ? new DarkTheme() : new LightTheme();
            }

            public Ref<ITheme> CreateTheme(bool dark)
            {
                var r = new Ref<ITheme>(createThemeImpl(dark));
                themes.Add(new WeakReference<Ref<ITheme>>(r));
                return r;
            }

            public void ReplaceTheme(bool dark)
            {
                foreach (var weakReference in themes)
                {
                    if (weakReference.TryGetTarget(out Ref<ITheme> reference))
                    {
                        reference.Value = createThemeImpl(dark);
                    }
                }
            }
        }

        public class Ref<T> where T : class
        {
            public T Value;

            public Ref(T value)
            {
                Value = value;
            }
        }
        
        public static void Main(string[] args)
        {
            var factory = new TrackingThemeFactory();
            var theme1 = factory.CreateTheme(false);
            var theme2 = factory.CreateTheme(true);
            Console.WriteLine(factory.Info);

            var factory2 = new ReplaceableThemeFactory();
            var magicTheme = factory2.CreateTheme(true);
            Console.WriteLine(magicTheme.Value.BgrColor);
            factory2.ReplaceTheme(false);
            Console.WriteLine(magicTheme.Value.BgrColor);
        }
    }
}