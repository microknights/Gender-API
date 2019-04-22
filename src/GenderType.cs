using MicroKnights.Collections;

namespace MicroKnights.Gender_API
{
    public class GenderType : Enumeration<GenderType>
    {
        public GenderType(int value, string displayName) 
            : base(value, displayName)
        {}

        public static readonly GenderType Undetermined = new GenderType(-1, "undetermined");
        public static readonly GenderType Unknown = new GenderType(0,"unknown");
        public static readonly GenderType Male = new GenderType(1,"male");
        public static readonly GenderType Female = new GenderType(2,"female");

        public bool IsGenderKnown => Value > 0;
    }
}