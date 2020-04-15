﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglishMobile.Core.Validations
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }

        bool Check(T value);
    }
}
