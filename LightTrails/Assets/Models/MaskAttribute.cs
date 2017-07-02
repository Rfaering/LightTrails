using System.Linq;

namespace Assets.Models
{
    public class MaskAttribute : OptionsAttribute
    {
        public MaskAttribute()
        {
            Options = MaskImages.Masks.Select(x => x.name).ToList();
        }
    }
}
