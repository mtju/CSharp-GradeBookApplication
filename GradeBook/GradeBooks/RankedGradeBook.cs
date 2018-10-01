using GradeBook.Enums;
using System;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            if (PercentOfStudentsWithWorseAverageGrade(averageGrade) >= 80) return 'A';
            if (PercentOfStudentsWithWorseAverageGrade(averageGrade) >= 60) return 'B';
            if (PercentOfStudentsWithWorseAverageGrade(averageGrade) >= 40) return 'C';
            if (PercentOfStudentsWithWorseAverageGrade(averageGrade) >= 20) return 'D';
            // No E
            return 'F';
        }

        private double PercentOfStudentsWithWorseAverageGrade(double averageGrade)
        {
            var countOfStudentsWithWorseAverageGrade = Students.Where(x => x.AverageGrade < averageGrade).Count();
            return (double)countOfStudentsWithWorseAverageGrade / (double)Students.Count * 100;
        }
    }
}
