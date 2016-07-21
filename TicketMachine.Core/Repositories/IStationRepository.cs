using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketMachine.Core.Repositories
{
    /// <summary>
    /// Defines the "Station Repository" contract.
    /// </summary>
    public interface IStationRepository
    {
        /// <summary>
        /// Searches for the stations that start with a specific string
        /// </summary>
        /// <param name="startsWith">The string to search for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// <para>
        /// The task result contains a collection of <see cref="Entities.Station"/>
        /// </para>
        /// </returns>
        Task<IEnumerable<Entities.Station>> GetStationsStartingWithAsync(string startsWith);
    }
}