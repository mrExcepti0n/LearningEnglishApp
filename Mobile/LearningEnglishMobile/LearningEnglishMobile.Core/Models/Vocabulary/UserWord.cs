using LearningEnglishMobile.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglishMobile.Core.Models.Vocabulary
{
    public class UserWord : ExtendedBindableObject
    {
        public int Id { get; set; }

        public string Word { get; set; }

        public string Translation { get; set; }


        private bool _isSelected { get; set; }

        public bool IsSelected { 
            get => _isSelected;
            set { _isSelected = value; RaisePropertyChanged(() => IsSelected); }
        }

    }
}
