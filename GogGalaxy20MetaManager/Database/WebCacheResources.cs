using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GogGalaxy20MetaManager
{
    public class WebCacheResources
    {
        [Required]
        public int WebCacheId { get; set; }
        public WebCacheResourceType WebCacheResourceTypeId { get; set; }
        [Required]
        public string Filename { get; set; }

        [Required, ForeignKey("webCacheId")]
        public WebCache WebCache { get; set; }
    }
}