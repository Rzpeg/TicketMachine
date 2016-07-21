using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TicketMachine.Application.Tests
{
    /// <summary>
    /// basic unit tests
    /// unit testing done using the xUnit.
    /// </summary>
    public class StationServiceUnitTests
    {
        [Fact]
        public async void SearchStationsStartingWithAsync_InputNamePartial()
        {
            //defining the datasource
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

            // mocking the repo
            var mockedRepo = new Mock<Core.Repositories.IStationRepository>(MockBehavior.Strict);

            mockedRepo.Setup(p => p.GetStationsStartingWithAsync("DART"))
                .ReturnsAsync(dataSource.Where(s => s.Name.StartsWith("DART")));

            // mocking the logger
            var mockedLogger = new Mock<Crosscutting.ILogger>(MockBehavior.Strict);

            mockedLogger.Setup(p => p.LogErrorAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            mockedLogger.Setup(p => p.LogInfoAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            mockedLogger.Setup(p => p.LogExceptionAsync(It.IsAny<Exception>()))
                .Returns(Task.FromResult(true));

            //Init service
            Application.Station.IStationService service = new Application.Station.StationService(mockedRepo.Object, mockedLogger.Object);

            // service invocation
            var actual = await service.SearchStationsStartingWithAsync(new Station.Dto.SearchStationsStartingWithInput()
            {
                StartingWith = "DART"
            });

            // expected result
            var expected = new Station.Dto.SearchStationsStartingWithOutput()
            {
                Stations = new List<Station.Dto.StationDto>()
                {
                    new Station.Dto.StationDto() { Name = "DARTFORD" },
                    new Station.Dto.StationDto() { Name = "DARTMOUTH" },
                },
                NextPossbileCharacters = new List<char>()
                {
                    'F',
                    'M'
                }
            };

            // assert
            Assert.NotNull(actual);
            Assert.Equal(expected.NextPossbileCharacters, actual.NextPossbileCharacters);
            Assert.True(actual.Stations.Count() == 2);
            Assert.Contains(actual.Stations, p => p.Name == "DARTFORD");
            Assert.Contains(actual.Stations, p => p.Name == "DARTMOUTH");
        }

        [Fact]
        public async void SearchStationsStartingWithAsync_InputNameFull()
        {
            //defining the datasource
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

            // mocking the repo
            var mockedRepo = new Mock<Core.Repositories.IStationRepository>(MockBehavior.Strict);

            mockedRepo.Setup(p => p.GetStationsStartingWithAsync("LIVERPOOL"))
                .ReturnsAsync(dataSource.Where(s => s.Name.StartsWith("LIVERPOOL")));

            // mocking the logger
            var mockedLogger = new Mock<Crosscutting.ILogger>(MockBehavior.Strict);

            mockedLogger.Setup(p => p.LogErrorAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            mockedLogger.Setup(p => p.LogInfoAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            mockedLogger.Setup(p => p.LogExceptionAsync(It.IsAny<Exception>()))
                .Returns(Task.FromResult(true));

            //Init service
            Application.Station.IStationService service = new Application.Station.StationService(mockedRepo.Object, mockedLogger.Object);

            // service invocation
            var actual = await service.SearchStationsStartingWithAsync(new Station.Dto.SearchStationsStartingWithInput()
            {
                StartingWith = "LIVERPOOL"
            });

            // expected result
            var expected = new Station.Dto.SearchStationsStartingWithOutput()
            {
                Stations = new List<Station.Dto.StationDto>()
                {
                    new Station.Dto.StationDto() { Name = "LIVERPOOL" }
                },
                NextPossbileCharacters = new List<char>() { ' ' }
            };

            // assert
            Assert.NotNull(actual);
            Assert.Equal(expected.NextPossbileCharacters, actual.NextPossbileCharacters);
            Assert.True(actual.Stations.Count() == 2);
            Assert.Contains(actual.Stations, p => p.Name == "LIVERPOOL");
            Assert.Contains(actual.Stations, p => p.Name == "LIVERPOOL LIME STREET");
        }

        [Fact]
        public async void SearchStationsStartingWithAsync_InputNameNonexisting()
        {
            //defining the datasource
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

            // mocking the repo
            var mockedRepo = new Mock<Core.Repositories.IStationRepository>(MockBehavior.Strict);

            mockedRepo.Setup(p => p.GetStationsStartingWithAsync("KINGS CROSS"))
                .ReturnsAsync(dataSource.Where(s => s.Name.StartsWith("KINGS CROSS")));

            // mocking the logger
            var mockedLogger = new Mock<Crosscutting.ILogger>(MockBehavior.Strict);

            mockedLogger.Setup(p => p.LogErrorAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            mockedLogger.Setup(p => p.LogInfoAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            mockedLogger.Setup(p => p.LogExceptionAsync(It.IsAny<Exception>()))
                .Returns(Task.FromResult(true));

            //Init service
            Application.Station.IStationService service = new Application.Station.StationService(mockedRepo.Object, mockedLogger.Object);

            // service invocation
            var actual = await service.SearchStationsStartingWithAsync(new Station.Dto.SearchStationsStartingWithInput()
            {
                StartingWith = "KINGS CROSS"
            });

            // expected result
            var expected = new Station.Dto.SearchStationsStartingWithOutput()
            {
                Stations = new List<Station.Dto.StationDto>(),
                NextPossbileCharacters = Enumerable.Empty<char>()
            };

            // assert
            Assert.NotNull(actual);
            Assert.Equal(expected.NextPossbileCharacters, actual.NextPossbileCharacters);
            Assert.Empty(actual.Stations);
        }

        [Fact]
        public async void SearchStationsStartingWithAsync_ThrowExceptionInRepository()
        {
            //defining the datasource
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

            // mocking the repo that will throw an exception
            var mockedRepo = new Mock<Core.Repositories.IStationRepository>(MockBehavior.Strict);

            mockedRepo.Setup(p => p.GetStationsStartingWithAsync(It.IsAny<string>()))
                .Throws(new ArgumentNullException("mockedException"));

            // mocking the logger
            var mockedLogger = new Mock<Crosscutting.ILogger>(MockBehavior.Strict);

            mockedLogger.Setup(p => p.LogErrorAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            mockedLogger.Setup(p => p.LogInfoAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            mockedLogger.Setup(p => p.LogExceptionAsync(It.IsAny<Exception>()))
                .Returns(Task.FromResult(true));

            //Init service
            Application.Station.IStationService service = new Application.Station.StationService(mockedRepo.Object, mockedLogger.Object);

            // service invocation
            await Assert.ThrowsAsync<ArgumentNullException>("mockedException",
                async () => await service.SearchStationsStartingWithAsync(
                    new Station.Dto.SearchStationsStartingWithInput()
                    {
                        StartingWith = "EUSTON"
                    }
                ));
        }
    }
}