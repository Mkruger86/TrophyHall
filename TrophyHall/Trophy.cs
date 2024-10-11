using System.Xml.Linq;

namespace TrophyHall
{
    public class Trophy
    {
        public int Id { get; set; }
        public string? Competition { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return "Id: " + Id + ", Competition: " + Competition + ", Year: " + Year;
        }

        public void ValidateCompetition() 
        {
            if (Competition == null)
            {
                throw new ArgumentNullException("Ugyldigt konkurrencenavn");
            }
            else if (Competition.Length < 3)
            {
                throw new ArgumentException("Konkurrencenavnet skal indeholde min. 3 tegn");
            }
            else if (Competition.Length > 70)
            {
                throw new ArgumentException("Konkurrencenavnet må maksimalt indeholde 70 tegn");
            }
        }

        public void ValidateYear()
        {
            if (Year < 1970 || Year > 2024)
            {
                throw new ArgumentOutOfRangeException("Trofæet kan ikke være fra før 1970 eller efter 2024");
            }
        }

        public void ValidateTrophy()
        {
            ValidateCompetition();
            ValidateYear();
        }
    }
}
