using System.Linq;
using Assets.Projects.Scripts;

namespace Assets.Models
{
    public class MaskAttribute : OptionsAttribute
    {
        public MaskAttribute()
        {
            Name = "Mask";
            Options = MaskImages.AllMasks.Select(x => x.name).ToList();
        }

        public override void SetAttributeValue(AttributeValue value)
        {
            CallBack(value.Value as string);
        }
    }
}
