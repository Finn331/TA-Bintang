using System.Globalization;
using System.Text;

public static class AnswerUtils
{
    // normalisasi: lower, trim, hilangkan spasi berlebih, tanda baca, dan diakritik
    public static string Normalize(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return "";
        s = s.Trim().ToLowerInvariant();

        // hapus diakritik (é -> e)
        var norm = s.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in norm)
        {
            var uc = CharUnicodeInfo.GetUnicodeCategory(c);
            if (uc != UnicodeCategory.NonSpacingMark) sb.Append(c);
        }
        s = sb.ToString().Normalize(NormalizationForm.FormC);

        // hapus tanda baca & spasi ganda
        var cleaned = new StringBuilder(s.Length);
        foreach (var c in s)
        {
            if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)) cleaned.Append(c);
        }
        s = System.Text.RegularExpressions.Regex.Replace(cleaned.ToString(), @"\s+", " ");
        return s;
    }

    public static bool Matches(string user, string golden)
        => Normalize(user) == Normalize(golden);
}
