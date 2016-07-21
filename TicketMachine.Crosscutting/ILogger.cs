using System;
using System.Threading.Tasks;

namespace TicketMachine.Crosscutting
{
    /// <summary>
    /// Logger contract definition
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <returns>A task representing asynchonous operation.</returns>
        Task LogInfoAsync(string message);

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>A task representing asynchonous operation.</returns>
        Task LogExceptionAsync(Exception ex);

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <returns>A task representing asynchonous operation.</returns>
        Task LogErrorAsync(string message);
    }
}