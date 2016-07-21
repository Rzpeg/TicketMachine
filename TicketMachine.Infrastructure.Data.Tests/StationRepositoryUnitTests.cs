using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TicketMachine.Infrastructure.Data.Tests
{
    /// <summary>
    /// basic unit tests of our repository unit.
    /// Done using xUnit.
    /// </summary>
    public class StationRepositoryUnitTests
    {
        [Fact]
        public async void GetStationsStartingWithAsync_InputPartial()
        {
            // datasoutce
            var dataSource = new List<Core.Entities.Station>();
            dataSource.Add(new Core.Entities.Station()
            {
                Name = "DARTFORD"
            });

            dataSource.Add(new Core.Entities.Station()
            {
                Name = "DARTMOUTH"
            });

            dataSource.Add(new Core.Entities.Station()
            {
                Name = "TOWER HILL"
            });

            dataSource.Add(new Core.Entities.Station()
            {
                Name = "DERBY"
            });

            // repo instance
            Core.Repositories.IStationRepository repo = new TicketMachine.Infrastructure.Data.StationRepository(dataSource);

            // actual data
            var actual = await repo.GetStationsStartingWithAsync("DART");

            // assert
            Assert.True(actual.Count() == 2);
            Assert.Contains(actual, p => p.Name == "DARTFORD");
            Assert.Contains(actual, p => p.Name == "DARTMOUTH");
        }

        [Fact]
        public async void GetStationsStartingWithAsync_InputFull()
        {
            // datasource
            var dataSource = new List<Core.Entities.Station>();
            dataSource.Add(new Core.Entities.Station()
            {
                Name = "LIVERPOOL"
            });

            dataSource.Add(new Core.Entities.Station()
            {
                Name = "LIVERPOOL LIME STREET"
            });

            dataSource.Add(new Core.Entities.Station()
            {
                Name = "PADDINGTON"
            });

            // repo
            Core.Repositories.IStationRepository repo = new TicketMachine.Infrastructure.Data.StationRepository(dataSource);

            // actual data
            var actual = await repo.GetStationsStartingWithAsync("LIVERPOOL");

            // assert
            Assert.True(actual.Count() == 2);
            Assert.Contains(actual, p => p.Name == "LIVERPOOL");
            Assert.Contains(actual, p => p.Name == "LIVERPOOL LIME STREET");
        }

        [Fact]
        public async void GetStationsStartingWithAsync_InputNonexisting()
        {
            // datasource
            var dataSource = new List<Core.Entities.Station>();
            dataSource.Add(new Core.Entities.Station()
            {
                Name = "EUSTON"
            });

            dataSource.Add(new Core.Entities.Station()
            {
                Name = "LONDON BRIDGE"
            });

            dataSource.Add(new Core.Entities.Station()
            {
                Name = "VICTORIA"
            });

            // repo
            Core.Repositories.IStationRepository repo = new TicketMachine.Infrastructure.Data.StationRepository(dataSource);

            // actual data
            var actual = await repo.GetStationsStartingWithAsync("KINGS CROSS");

            // assert
            Assert.Empty(actual);
        }
    }
}