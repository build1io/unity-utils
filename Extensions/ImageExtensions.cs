using UnityEngine.UI;

namespace Build1.UnityUtils.Extensions
{
    public static class ImageExtensions
    {
        public static Image SetAlpha(this Image image, float alpha)
        {
            var color = image.color;
            color.a = alpha;
            image.color = color;
            return image;
        }
    }
}