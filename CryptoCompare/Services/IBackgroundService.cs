namespace CryptoCompare.Services
{
    /// <summary>
    /// Service that must be exceute in background.
    /// </summary>
    public interface IBackgroundService
    {
        /// <summary>
        /// Code that must be executed in background.
        /// </summary>
        void Run();

        /// <summary>
        /// This methods stops the service.
        /// </summary>
        void Stop();
    }
}