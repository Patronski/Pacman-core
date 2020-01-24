using Pacman.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Data
{
    public class Seeder
    {
        private PacmanContext context { get; set; }

        public Seeder(PacmanContext context)
        {
            this.context = context;
        }

        public void SeedDatabase()
        {
            var extracter = new ExtractAllLevels();
            var levels = extracter.ExctractLevels();

            context.Levels.AddRange(levels);
            context.SaveChanges();
        }
    }
}
