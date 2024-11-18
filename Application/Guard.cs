using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Application;
public static class Guard
{
    public static void ThrowIfNull([NotNull] object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
        {
            Throw(paramName);
        }
    }

    internal static void Throw(string? paramName) =>
            throw new ArgumentNullException(paramName);
}
