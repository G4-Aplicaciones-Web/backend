using Humanizer;

namespace backendNetCore.Shared.Infrastructure.Persistence.Configuration.Extensions;

/// <summary>
/// String extensions for converting to snake case and pluralization
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     Convert a string to snake case
    /// </summary>
    /// <param name="text">The string to convert</param>
    /// <returns>The string converted to snake case</returns>
    public static string ToSnakeCase(this string text)
    {
        return new string(Convert(text.GetEnumerator()).ToArray());

        static IEnumerable<char> Convert(CharEnumerator e)
        {
            if (!e.MoveNext()) yield break;
            yield return char.ToLower(e.Current);

            while (e.MoveNext())
            {
                if(char.IsUpper(e.Current))
                {
                    yield return '-';
                    yield return char.ToLower(e.Current);
                }
                else
                {
                    yield return e.Current;
                }
            }
        }
    }

    /// <summary>
    ///     Pluralizes the given text
    /// </summary>
    /// <param name="text">The string to convert</param>
    /// <returns>The string converted to plural</returns>
    public static string ToPlural(this string text)
    {
        return text.Pluralize(false);
    }
}