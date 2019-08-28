namespace Nefarius.ViGEm.Client.Utilities
{
    internal class MathUtil
    {
        /// <summary>
        ///     Transforms a value within a given source range into a matching value of a given target range.
        /// </summary>
        /// <param name="originalStart">Minimum value of the source range.</param>
        /// <param name="originalEnd">Maximum value of the source range.</param>
        /// <param name="newStart">Minimum value of the target range.</param>
        /// <param name="newEnd">Maximum value of the target range.</param>
        /// <param name="value">The source value to transform.</param>
        /// <returns>The transformed value.</returns>
        public static int ConvertRange(
            int originalStart, int originalEnd, // original range
            int newStart, int newEnd, // desired range
            int value) // value to convert
        {
            double scale = (double)(newEnd - newStart) / (originalEnd - originalStart);
            return (int)(newStart + ((value - originalStart) * scale));
        }
    }
}
