﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyApi.Dtos
{
    public class UserWordSetSaveDto
    {
        public int WordSetId { get; set; }

        public ICollection<int> WordSetItemIds { get; set; }
    }
}
