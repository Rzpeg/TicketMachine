namespace TicketMachine.Application.Station.Dto
{
    /// <summary>
    /// Service input DTO for searching stations starting with a specific string
    /// </summary>
    public class SearchStationsStartingWithInput
    {
        /// <summary>
        /// Search pattern.
        /// </summary>
        public string StartingWith { get; set; }
    }
}