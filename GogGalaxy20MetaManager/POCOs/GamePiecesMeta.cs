namespace GogGalaxy20MetaManager
{
    //{"criticsScore":null,"developers":["Picorinne Soft"],"genres":["Role-playing (RPG)","Adventure","Indie"],"publishers":[],"releaseDate":null,"themes":[]}
    //{"criticsScore":null,"developers":["Computer Recreations, Inc.","Steve Russel"],"genres":["Shooter","Simulator","Pinball"],"publishers":["Computer Recreations, Inc.","Steve Russel"],"releaseDate":-242179200,"themes":["Action","Science fiction"]}
    public class GamePiecesMeta
    {
        public decimal? CriticsScore { get; set; }
        public string[] Developers { get; set; }
        public string[] Genres { get; set; }
        public string[] Publishers { get; set; }
        public long? ReleaseDate { get; set; } //unix time
        public string[] Themes { get; set; }
    }
}