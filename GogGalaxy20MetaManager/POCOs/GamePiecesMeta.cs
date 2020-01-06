namespace GogGalaxy20MetaManager
{
    //{"criticsScore":null,"developers":["Picorinne Soft"],"genres":["Role-playing (RPG)","Adventure","Indie"],"publishers":[],"releaseDate":null,"themes":[]}
    public class GamePiecesMeta
    {
        public decimal? CriticsScore { get; set; }
        public string[] Developers { get; set; }
        public string[] Genres { get; set; }
        public string[] Publishers { get; set; }
        public ulong? ReleaseDate { get; set; } //unix time
        public string[] Themes { get; set; }
    }
}