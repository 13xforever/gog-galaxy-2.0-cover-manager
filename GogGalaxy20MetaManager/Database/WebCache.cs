using System.ComponentModel.DataAnnotations;

namespace GogGalaxy20MetaManager
{
	public class WebCache
	{
		public int Id { get; set; }
		[Required]
		public string ReleaseKey { get; set; }
		public ulong UserId { get; set; }
	}
}