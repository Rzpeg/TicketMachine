using System.Threading.Tasks;

namespace TicketMachine.Application.Station
{
    /// <summary>
    /// Defines the "Station Service" Contract
    /// </summary>
    public interface IStationService
    {
        /// <summary>
        /// Searches the stations that satisfy the input condidions.
        /// </summary>
        /// <param name="input">Search conditions.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// <para>
        /// The task result contains a <see cref="Dto.SearchStationsStartingWithOutput"/>.
        /// </para>
        /// </returns>
        Task<Dto.SearchStationsStartingWithOutput> SearchStationsStartingWithAsync(Dto.SearchStationsStartingWithInput input);
    }
}