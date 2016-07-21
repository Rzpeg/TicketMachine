using System.Collections.Generic;

namespace TicketMachine.Application.Station.Dto
{
    /// <summary>
    /// Service resulting (output) DTO for searching stations starting with a specific string
    /// </summary>
    public class SearchStationsStartingWithOutput
    {
        /// <summary>
        /// A collection of next possible characters.
        /// </summary>
        public IEnumerable<char> NextPossbileCharacters { get; set; }

        /// <summary>
        /// A collection of stations.
        /// </summary>
        public IEnumerable<StationDto> Stations { get; set; }
    }
}