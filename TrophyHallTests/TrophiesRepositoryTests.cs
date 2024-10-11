using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrophyHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrophyHall.Tests
{
    [TestClass()]
    public class TrophiesRepositoryTests
    {
        private readonly TrophiesRepository _repository = new TrophiesRepository();

        [TestMethod()]
        public void GetDifTest()
        {
            IEnumerable<Trophy> trophies = _repository.Get();
            Assert.AreEqual(5, trophies.Count());
            Assert.AreEqual("Copenhagen Marathon", trophies.First().Competition);
            Assert.AreEqual(1987, trophies.Last().Year);

            IEnumerable<Trophy> sortTrophyComp = _repository.Get(orderBy: "competition");
            Assert.AreEqual("ABC Electric Marathon de Paris", sortTrophyComp.First().Competition);
            IEnumerable<Trophy> sortTrophyYear = _repository.Get(orderBy: "year");
            Assert.AreEqual(1987, sortTrophyYear.First().Year);

            //IEnumerable<Trophy> filterTrophy = _repository.Get(filterYear: 2005);          
            //Assert.AreEqual(1, filterTrophy.Count());
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.IsNotNull(_repository.GetById(5));
            Assert.ThrowsException<ArgumentNullException>(() => _repository.GetById(0));
        }

        [TestMethod()]
        public void AddTest()
        {
            Trophy trophyAdd = _repository.Add(new Trophy() { Competition = "Wakawaka Marathon", Year = 2023 });
            Assert.AreEqual(6, trophyAdd.Id);
            Assert.AreEqual(6, _repository.Get().Count());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repository.Add(new Trophy() { Competition = "Wakawaka Marathon", Year = 2025 }));
        }
    }
}