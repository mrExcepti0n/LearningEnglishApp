using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VocabularyApi.Models;

namespace VocabularyApi.Infrastructure.DataAccess
{
    public class VocabularyContext : DbContext
    {
        public DbSet<UserVocabulary> UserVocabularies { get; set; }
        public DbSet<VocabularyWord> VocabularyWords { get; set; }
        public DbSet<WordTranslation> WordTranslations { get; set; }


        public VocabularyContext(DbContextOptions<VocabularyContext> options)
         : base(options)
        {
          
        }
    }
}
