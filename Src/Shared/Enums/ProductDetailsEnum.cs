
using System.ComponentModel;

namespace Shared.Enums;

public enum ProductDetailsEnum : byte
{
    [Description("قیمت")]
    Price = 1,
    [Description("رنگ")]
    Color = 2,
    [Description("سایز")]
    Size = 3,
    [Description("وزن")]
    Weight = 4,
    [Description("برند")]
    Brand = 5,
    [Description("مدل")]
    Model = 6,
    [Description("توضیحات")]
    Description = 7,
}
