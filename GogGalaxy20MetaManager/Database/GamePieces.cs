using System.ComponentModel.DataAnnotations;

namespace GogGalaxy20MetaManager
{
    public class GamePieces
    {
        [Required]
        public string ReleaseKey { get; set; }
        public GamePieceType GamePieceTypeId { get; set; }
        public ulong? UserId { get; set; }
        [Required]
        public string Value { get; set; } //json depending on gamePieceTypeId
    }
}