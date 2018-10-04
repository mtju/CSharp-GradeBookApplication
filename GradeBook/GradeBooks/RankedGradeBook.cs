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
            ValidateStudentCountException();

            if (PercentOfStudentsWithWorseAverageGrade(averageGrade) >= 80) return 'A';
            if (PercentOfStudentsWithWorseAverageGrade(averageGrade) >= 60) return 'B';
            if (PercentOfStudentsWithWorseAverageGrade(averageGrade) >= 40) return 'C';
            if (PercentOfStudentsWithWorseAverageGrade(averageGrade) >= 20) return 'D';
            // No E
            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (!ValidateStudentCountConsole()) return;
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (!ValidateStudentCountConsole()) return;
            base.CalculateStudentStatistics(name);
        }

        private void ValidateStudentCountException()
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }
        }

        private bool ValidateStudentCountConsole()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return false;
            }

            return true;
        }

        private double PercentOfStudentsWithWorseAverageGrade(double averageGrade)
        {
            var countOfStudentsWithWorseAverageGrade = Students.Where(x => x.AverageGrade < averageGrade).Count();
            return (double)countOfStudentsWithWorseAverageGrade / (double)Students.Count * 100;
        }
    }
}
