namespace TimeCalculator
{
    using System.ComponentModel;

    /// <summary>
    /// Types of time units.
    /// </summary>
    public enum ETimeUnit
    {
        /// <summary>
        /// Represents a 마이크로초.
        /// </summary>
        [Description("마이크로초")]
        MicroSecond,

        /// <summary>
        /// Represents a 밀리초.
        /// </summary>
        [Description("밀리초")]
        MilliSecond,

        /// <summary>
        /// Represents a 초.
        /// </summary>
        [Description("초")]
        Second,

        /// <summary>
        /// Represents a 분.
        /// </summary>
        [Description("분")]
        Minute,

        /// <summary>
        /// Represents a 시간.
        /// </summary>
        [Description("시간")]
        Hour,

        /// <summary>
        /// Represents a 일.
        /// </summary>
        [Description("일")]
        Day,

        /// <summary>
        /// Represents a 주.
        /// </summary>
        [Description("주")]
        Week,

        /// <summary>
        /// Represents a 년.
        /// </summary>
        [Description("년")]
        Year
    }
}
