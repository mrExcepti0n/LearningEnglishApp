using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglishMobile.Core.ViewModels.Base
{

    public class MasterMenuItem
    {       
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}