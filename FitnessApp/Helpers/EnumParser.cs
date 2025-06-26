namespace FitnessApp.Helpers
{
    public static class EnumParser
    {
        public static List<T>? ParseEnums<T>(List<string>? values) where T : struct, Enum
        {
            if (values == null) return null;

            var result = new List<T>();
            foreach (var val in values)
            {
                if (Enum.TryParse<T>(val, ignoreCase: true, out var enumVal))
                    result.Add(enumVal);
            }
            return result.Count > 0 ? result : null;
        }
    }
}
