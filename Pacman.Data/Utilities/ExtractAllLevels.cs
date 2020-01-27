namespace Pacman.Utilities
{
    using Pacman.Models;
    using System.Collections.Generic;
    using System.IO;

    public class ExtractAllLevels
    {
        private const string LevelFolder = @"DataFiles\Levels";

        private List<Level> levels;

        public ExtractAllLevels()
        {
            this.levels = new List<Level>();
        }

        /// <summary>
        /// Reading levels from level folder and returning models
        /// </summary>
        /// <returns></returns>
        public List<Level> ExctractLevels()
        {
            if (!Directory.Exists(LevelFolder))
            {
                throw new DirectoryNotFoundException("Invalid path of level directory!");
            }

            DirectoryInfo directory = new DirectoryInfo(LevelFolder);
            FileInfo[] files = directory.GetFiles("*.txt");

            foreach (FileInfo file in files)
            {
                var levelName = file.Name.Substring(0, file.Name.Length - 4);
                var level = new Level();
                level.Name = levelName;

                var lines = File.ReadAllLines(file.FullName);
                var levelCoordinates = new List<LevelCoordinate>();
                foreach (var line in lines)
                {
                    var coords = line.Split('=')[0].Split(',');
                    var land = line.Split('=')[1].Split(',');

                    levelCoordinates.Add(new LevelCoordinate
                    {
                        QuadrantX = int.Parse(coords[0]),
                        QuadrantY = int.Parse(coords[1]),
                        isWall = land[0] == "1" ? true : false,
                        isPoint = land[1] == "1" ? true : false,
                    });

                }
                level.LevelCoordinates = levelCoordinates;
                levels.Add(level);
            }

            return this.levels;
        }
    }
}
