using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketMachine.Infrastructure.Data
{
    /// <summary>
    /// Station Repository implementation
    /// </summary>
    public class StationRepository : Core.Repositories.IStationRepository
    {
        /// <summary>
        /// The stations storage
        /// </summary>
        private IEnumerable<Core.Entities.Station> _stations;

        /// <summary>
        /// Initializes a new instance of the <see cref="StationRepository"/> class.
        /// </summary>
        /// <param name="stationsDataSource">The stations data source.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public StationRepository(IEnumerable<Core.Entities.Station> stationsDataSource)
        {
            this._stations = stationsDataSource;
        }

        /// <summary>
        /// Searches for the stations that start with a specific string
        /// </summary>
        /// <param name="startsWith">The string to search for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// <para>
        /// The task result contains a collection of <see cref="Entities.Station" /></para>
        /// </returns>
        public Task<IEnumerable<Core.Entities.Station>> GetStationsStartingWithAsync(string startsWith)
        {
            // find needed stations
            var stations = this._stations
                .Where(p => p.Name.StartsWith(startsWith));

            // return the result
            return Task.FromResult(stations);
        }
    }
}