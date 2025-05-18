namespace Game.Damages
{
    public static class HeathExtensions
    {
        public static float Normalized(this IHealth heath) => heath.Max > 0 ? (float)heath.Current / (float)heath.Max : 0;

        public static bool IsDead(this IHealth heath) => heath.Current <= 0;
    }
}