public static class ArrayExtension
{
    public static int GetNearestAvailablePos<T>(this T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
}