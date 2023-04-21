using TMPro;

namespace Build1.UnityUtils.Extensions
{
    public static class TextMeshProExtensions
    {
        public static TextMeshProUGUI SetAlpha(this TextMeshProUGUI label, float alpha)
        {
            var color = label.color;
            color.a = alpha;
            label.color = color;
            return label;
        }
    }
}