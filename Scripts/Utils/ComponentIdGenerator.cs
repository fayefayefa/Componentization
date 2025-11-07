namespace Componentization.Utils
{
    public class ComponentIdGenerator
    {
        private static long s_Id = 0;
        
        public static long Id => s_Id++;
    }
}