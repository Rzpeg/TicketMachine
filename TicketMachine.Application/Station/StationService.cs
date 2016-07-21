using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace TicketMachine.Application.Station
{
    /// <summary>
    /// Station Service
    /// </summary>
    public class StationService : IStationService
    {
        /// <summary>
        /// The stations data repository.
        /// </summary>
        private Core.Repositories.IStationRepository _stationRepository;

        /// <summary>
        /// The logger.
        /// </summary>
        private Crosscutting.ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StationService"/> class.
        /// </summary>
        /// <param name="stationRepository">The station repository.</param>
        public StationService(Core.Repositories.IStationRepository stationRepository, Crosscutting.ILogger logger)
        {
            this._stationRepository = stationRepository;
            this._logger = logger;
        }

        /// <summary>
        /// Searches the stations that satisfy the input condidions.
        /// </summary>
        /// <param name="input">Search conditions.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// <para>
        /// The task result contains a <see cref="Dto.SearchStationsStartingWithOutput" />.
        /// </para>
        /// </returns>
        public async Task<Dto.SearchStationsStartingWithOutput> SearchStationsStartingWithAsync(Dto.SearchStationsStartingWithInput input)
        {
            await this._logger.LogInfoAsync("entered SearchStationsStartingWithAsync").ConfigureAwait(false);

            Dto.SearchStationsStartingWithOutput output = null;

            // logger cannot be awaited in catch / finally blocks, so using ExceptionDispatchInfo as a workarround
            // The support for this feature is coming in the Roslyn
            ExceptionDispatchInfo capturedException = null;

            try
            {
                await this._logger.LogInfoAsync("fetching stations").ConfigureAwait(false);

                // fetch stations
                var dataRep = await this._stationRepository.GetStationsStartingWithAsync(input.StartingWith).ConfigureAwait(false);

                await this._logger.LogInfoAsync("selecting possible characters").ConfigureAwait(false);

                // select possible next characters
                var nextPossibleCharacters = dataRep
                    .Where(p => p.Name.Length > input.StartingWith.Length)
                    .Select(p => p.Name[input.StartingWith.Length]);

                await this._logger.LogInfoAsync("mapping results").ConfigureAwait(false);

                // map to DTOs
                var mappedResult = dataRep.Select(p => new Dto.StationDto()
                {
                    Name = p.Name
                });

                // prepare output
                output = new Dto.SearchStationsStartingWithOutput()
                {
                    Stations = mappedResult,
                    NextPossbileCharacters = nextPossibleCharacters
                };
            }
            catch (Exception ex)
            {
                //capture the exception
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }

            // if there's any captured exception, log it.
            if (capturedException != null)
            {
                await this._logger.LogExceptionAsync(capturedException.SourceException).ConfigureAwait(false);

                //rethrow the captured exception preserving the stack details
                capturedException.Throw();
            }

            await this._logger.LogInfoAsync("returning").ConfigureAwait(false);

            // return
            return output;
        }
    }
}