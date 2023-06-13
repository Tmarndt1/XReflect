using System.Linq.Expressions;

namespace XReflect
{
    public static partial class Extensions
    {
        internal static string? GetBody(this LambdaExpression expression)
        {
            if (expression?.Body is MemberExpression memberExp) return memberExp.Member.Name;

            return default;
        }
    }
}
