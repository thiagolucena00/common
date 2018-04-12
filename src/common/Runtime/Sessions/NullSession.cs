namespace common.Runtime.Sessions
{
    /// <summary>
    /// Implements null object pattern for <see cref="ISession"/>.
    /// </summary>
    public class NullSession : SessionBase
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullSession Instance { get; } = new NullSession();

        /// <inheritdoc />
        public override long? UserId => null;

        /// <inheritdoc />
        public override long? ImpersonatorUserId => null;

        private NullSession()
        {
            
        }
    }
}