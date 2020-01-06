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

    //{"background":"https:\/\/images.gog.com\/f96a172a5504f0307aecabbaaa67d7b9c4853bdf12ccc7d0a788e3da92fa6262_glx_bg_top_padding_7.webp?namespace=gamesdb",
    //"squareIcon":"https:\/\/images.gog.com\/fc388a316a774ae1bdbd888baedbc4b12c0b23bc8a3845a84cb929d0e0cd7094_glx_square_icon_v2.webp?namespace=gamesdb",
    //"verticalCover":"https:\/\/images.gog.com\/d75ef3fc45d15a601f92897d3e45b7e9d42e4a6d8756d6398b56aa232c20f45e_glx_vertical_cover.webp?namespace=gamesdb"}
    public class GamePiecesOriginalImages
    {
        public string Background { get; set; }
        public string SquareIcon { get; set; }
        public string VerticalCover { get; set; }
    }
}