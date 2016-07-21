using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace TicketMachine.Application.Tests
{
    /// <summary>
    /// Basic hevavior testing
    /// <para>
    /// P.S. The current xUnit-Specflow connector does not support xUnit 2.x (IUseFixture has beed removed)
    /// After updating the specflow Objectives.feature file, the Objectives.feature.cs will be regenerated and will become uncompilable.
    /// To fix it change the referenced interface for class "AssignmentObjectivesFeature" from "Xunit.IUseFixture" to "Xunit.IClassFixture".
    /// </para>
    /// </summary>
    [Binding]
    public class StationServiceBehaviorTests
    {
        // data source
        private List<Core.Entities.Station> dataSource;

        // search result
        private Application.Station.Dto.SearchStationsStartingWithOutput searchResult;

        // repo
        private Core.Repositories.IStationRepository stationRepository;

        // logger
        private Crosscutting.ILogger logger;

        // service
        private Application.Station.IStationService stationService;

        [Given(@"a list of four stations ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)""")]
        public void GivenAListOfFourStations(string p0, string p1, string p2, string p3)
        {
            // populating teh datasource
            dataSource = new List<Core.Entities.Station>();

            dataSource.Add(new Core.Entities.Station()
            {
                Name = p0
            });

            dataSource.Add(new Core.Entities.Station()
            {
                Name = p1
            });

            dataSource.Add(new Core.Entities.Station()
            {
                Name = p2
            });

            dataSource.Add(new Core.Entities.Station()
            {
                Name = p3
            });
        }

        [Given(@"a list of three stations ""(.*)"", ""(.*)"", ""(.*)""")]
        public void GivenAListOfThreeStations(string p0, string p1, string p2)
        {
            // populating teh datasource
            dataSource = new List<Core.Entities.Station>();

            dataSource.Add(new Core.Entities.Station()
            {
                Name = p0
            });

            dataSource.Add(new Core.Entities.Station()
            {
                Name = p1
            });

            dataSource.Add(new Core.Entities.Station()
            {
                Name = p2
            });
        }

        [When(@"input ""(.*?)""")]
        public async void WhenInput(string p0)
        {
            // mocking the repository
            var mockedRepo = new Mock<Core.Repositories.IStationRepository>(MockBehavior.Strict);

            mockedRepo.Setup(p => p.GetStationsStartingWithAsync(p0))
                .Returns((string input) => Task.FromResult(dataSource.Where(s => s.Name.StartsWith(input))));

            this.stationRepository = mockedRepo.Object;

            // mocking the logger
            var mockedLogger = new Mock<Crosscutting.ILogger>(MockBehavior.Strict);

            mockedLogger.Setup(p => p.LogErrorAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            mockedLogger.Setup(p => p.LogInfoAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            mockedLogger.Setup(p => p.LogExceptionAsync(It.IsAny<Exception>()))
                .Returns(Task.FromResult(true));

            this.logger = mockedLogger.Object;

            this.stationService = new Application.Station.StationService(this.stationRepository, this.logger);

            //searching
            this.searchResult = await this.stationService.SearchStationsStartingWithAsync(new Station.Dto.SearchStationsStartingWithInput()
             {
                 StartingWith = p0
             });
        }

        [Then(@"should return: The characters of ""(.*?)"", ""(.*?)"" and the stations ""(.*?)"", ""(.*?)""")]
        public void ThenShouldReturnTheCharactersOfAndTheStations(char p0, char p1, string p2, string p3)
        {
            // asserts
            Assert.Equal(p0, searchResult.NextPossbileCharacters.ElementAt(0));
            Assert.Equal(p1, searchResult.NextPossbileCharacters.ElementAt(1));
            Assert.Equal(p2, searchResult.Stations.ElementAt(0).Name);
            Assert.Equal(p3, searchResult.Stations.ElementAt(1).Name);
        }

        [Then(@"should return: The character of ""(.*?)"" and the stations ""(.*?)"", ""(.*?)""")]
        public void ThenShouldReturnTheCharacterOfAndTheStations(char p0, string p1, string p2)
        {
            // asserts
            Assert.Equal(p0, searchResult.NextPossbileCharacters.ElementAt(0));
            Assert.Equal(p1, searchResult.Stations.ElementAt(0).Name);
            Assert.Equal(p2, searchResult.Stations.ElementAt(1).Name);
        }

        [Then(@"should return no possible stations and no possible characters")]
        public void ThenShouldReturnNoPossibleStationsAndNoPossibleCharacters()
        {
            // asserts
            Assert.Empty(searchResult.NextPossbileCharacters);
            Assert.Empty(searchResult.Stations);
        }
    }
}