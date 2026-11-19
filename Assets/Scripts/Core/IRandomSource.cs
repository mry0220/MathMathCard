
namespace MathMathCard.Core
{
    public interface IRandomSource
    {
        /// <summary>
        /// 0以上 maxExclusive未満の整数を返す
        /// </summary>
        int Next(int maxExclusive);
    }
}
