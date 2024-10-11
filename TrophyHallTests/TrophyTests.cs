using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrophyHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TrophyHall.Tests
{
    [TestClass]
    public class TrophyTests
    {
        public Trophy _goodTrophy = new Trophy { Id = 1, Competition = "Copenhagen Marathon", Year = 2014 };
        public Trophy _badCompNull = new Trophy { Competition = null };
        public Trophy _badCompMin = new Trophy { Competition = "CM" };
        public Trophy _badCompMax = new Trophy { Competition = "Lorem ipsum dolor sit amet, " +
            "consectetur adipiscing elit. Proin laoreet." };
        public Trophy _badYearLow = new Trophy { Year = 1969 };
        public Trophy _badYearHigh = new Trophy { Year = 2025};

        [TestMethod]
        public void ToStringTest()
        {
            Assert.AreEqual("Id: 1, Competition: Copenhagen Marathon, Year: 2014", _goodTrophy.ToString());
        }

        [TestMethod]
        public void ValidateCompetitionTest()
        {
            _goodTrophy.ValidateCompetition();
            Assert.ThrowsException<ArgumentNullException>(() => _badCompNull.ValidateCompetition());
            Assert.ThrowsException<ArgumentException>(() => _badCompMin.ValidateCompetition());
            Assert.ThrowsException<ArgumentException>(() => _badCompMax.ValidateCompetition());
        }

        [TestMethod]
        public void ValidateYearTest()
        {
            _goodTrophy.ValidateYear();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _badYearLow.ValidateYear());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _badYearHigh.ValidateYear());
        }

        [TestMethod]
        public void ValidateTrophyTest()
        {
            _goodTrophy.ValidateTrophy();
        }

        //forsøg på boundary/equivalance parametertests (mindre kode fremfor enkeltvis objekt-initialisering som ovenfor) med min, min+1, nominel værdi, max-1, max
        [TestMethod]
        [DataRow("CPM", 1970)]
        [DataRow("CPHM", 1971)]
        [DataRow("Copenhagen Marathon", 2000)]
        [DataRow("Copenhagen Marathon anno 2025–A World Athletics Road Race Label event", 2023)]
        [DataRow("Copenhagen Marathon anno 2025– A World Athletics Road Race Label event", 2024)]
        public void ValidateCompetitionParamTest(string competition, int year)
        {
            _goodTrophy.Competition = competition;
            _goodTrophy.Year = year;
            _goodTrophy.ValidateTrophy();
        }
    }
}