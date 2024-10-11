using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TrophyHall
{
    public class TrophiesRepository
    {
        private int _nextId = 1;
        private readonly List<Trophy> _trophies = new List<Trophy>();

        public TrophiesRepository()
        {
            _trophies.Add(new Trophy() { Id = _nextId++, Competition = "Copenhagen Marathon", Year = 2005 });
            _trophies.Add(new Trophy() { Id = _nextId++, Competition = "KBC Dublic Marathon", Year = 2001 });
            _trophies.Add(new Trophy() { Id = _nextId++, Competition = "Baspa Marathon Hamburg", Year = 2016 });
            _trophies.Add(new Trophy() { Id = _nextId++, Competition = "Zurich Marathon de Sevilla", Year = 1995 });
            _trophies.Add(new Trophy() { Id = _nextId++, Competition = "ABC Electric Marathon de Paris", Year = 1987 });
        }

        public IEnumerable<Trophy> Get(string? orderBy = null, int? filterYear = null)
        {
            IEnumerable<Trophy> result = new List<Trophy>(_trophies);
            //sorter objekter i listen efter year eller competition
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "competition":
                        result = result.OrderBy(c => c.Competition);
                        break;
                    case "competition_desc":
                        result = result.OrderByDescending(c => c.Competition);
                        break;
                    case "year":
                        result = result.OrderBy(y => y.Year);
                        break;
                    case "year_desc":
                        result = result.OrderByDescending(y => y.Year);
                        break;
                    default:
                        break;
                }
                //where returnerer trofæer med givne year. Fortsætter eksvering efter match modsat find.
                if (filterYear != null)
                {               
                    result = result.Where(y => y.Year < filterYear);
                }
                //if (filterYear != null)
                //    {
                //    switch (filterYear)
                //    {
                //        case ("equal"):

                //            result = result.Where(trophy => trophy.Year == resultYear);
                //            break;
                //        case "above":
                //            result = result.Where(trophy => trophy.Year > resultYear);
                //            break;
                //        case "below":
                //            result = result.Where(trophy => trophy.Year < resultYear);
                //            break;
                //        default:
                //            break;
                //        }
                //    }
            }
            return result;
        }
        public Trophy GetById(int? id)
        {
            //find returnerer første tilfælde af id og stopper så eksekvering, modsat where.
            var result = _trophies.Find(trophies => trophies.Id == id);
            if (result == null)
            {
                throw new ArgumentNullException("Id findes ikke");
            }
            return result;
        }

        public Trophy Add(Trophy trophy)
        {
            trophy.ValidateTrophy();
            trophy.Id = _nextId++;
            _trophies.Add(trophy);
            return trophy;
        }

        public Trophy? Remove(int id)
        {
            Trophy? trophy = GetById(id);
            if (trophy == null)
            {
                return null;
            }
            _trophies.Remove(trophy);
            return trophy;
        }
    }
}
