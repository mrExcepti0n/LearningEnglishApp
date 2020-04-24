using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.Extensions
{
    public static class PageExtensions
    {
        public static void RemovePrevPageFromStack(this INavigation navigation)
        {
            navigation.RemovePage(navigation.NavigationStack[navigation.NavigationStack.Count - 2]);
        }
    }
}
