namespace Uow.Data.Extensions;

internal static class ArrayExtensions
{
    public static int IndexOf(this string[] value, string find)
    {
        for (var i = 0; i < value.Length; i++)
        {
            if (value[i].Equals(find))
            {
                return i;
            }
        }

        return -1;
    }
}