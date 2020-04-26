using LearningEnglishMobile.Core.ViewModels.Base;
using LearningEnglishMobile.Core.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningEnglishMobile.Core.ViewModels
{
    public class MasterMainViewModel : ExtendedBindableObject
    {
        public ObservableCollection<MasterMenuItem> MenuItems { get; set; }

        public MasterMainViewModel()
        {
            MenuItems = new ObservableCollection<MasterMenuItem>(new[]
            {
                new MasterMenuItem { Id = 0, Title = "Settings", TargetType = typeof(SettingsView) }
            });
        }   
    }
}
