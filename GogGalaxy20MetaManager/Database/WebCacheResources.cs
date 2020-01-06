using System.ComponentModel.DataAnnotations;

namespace GogGalaxy20MetaManager
{
    public class WebCacheResources
    {
        [Required]
        public string ReleaseKey { get; set; }
        public WebCacheResourceType WebCacheResourceTypeId { get; set; }
        public ulong UserId { get; set; }
        [Required]
        public string Filename { get; set; }
    }
}