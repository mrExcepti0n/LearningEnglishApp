﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Dtos
{
    public class TranslationDto
    {
        public int UserVocabularyWordId { get; set; }

        public string Name { get; set; }
        public string Translation { get; set; }
    }
}
