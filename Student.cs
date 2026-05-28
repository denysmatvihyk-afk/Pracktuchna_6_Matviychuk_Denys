using StudentGroupSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Praktychna1
{
    // 1. Додаємо наслідування від Person
    public class Student : Person, ICloneable
    {
        // Приватні поля залишаємо для валідації
        private string _recordBookNumber;

        public byte[] LabGrades { get; private set; } = new byte[10];
        // Властивість FullName, PersonalEmail та Notes тепер у Person, 
        // тому ми їх тут НЕ дублюємо, вони успадковуються автоматично.
        public string LastName => FullName.Split(' ').Last();
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
        public required string RecordBookNumber
        {
            get => _recordBookNumber;
            set
            {
                if (value?.Length != 8 || !long.TryParse(value, out _))
                    throw new ArgumentException("Номер заліковки має містити рівно 8 цифр");
                _recordBookNumber = value;
            }
        }
        public GradeJournal Journal { get; } = new GradeJournal();
        public double AverageGrade => Grades.Count > 0 ? Grades.Average(g => (double)g) : 0;
        public StudentStatus Status { get; set; }
        public DateTime EnrollmentDate { get; init; } = DateTime.Now;
        public int CourseProgress { get; set; }
        public List<GradePoint> Grades { get; set; } = new List<GradePoint>();
        // 2. ОНОВЛЕНИЙ КОНСТРУКТОР: викликаємо base(...) для Person
        public Student(string fullName, DateTime dateOfBirth, string personalEmail,
                       string recordBookNumber, double averageGrade, StudentStatus status, string notes = "")
            : base(fullName, dateOfBirth, personalEmail, notes)
        {
            RecordBookNumber = recordBookNumber;
            // AverageGrade у тебе розраховується автоматично, тому тут просто ініціалізуємо інші поля
            Status = status;
            CourseProgress = 0;
        }


        // 3. ПЕРЕВИНАЧЕННЯ GetInfo (override замість GetFormattedInfo)
        public override string GetInfo()
        {
            // base.GetInfo() викликає логіку з класу Person
            return base.GetInfo() + $" | Заліковка: #{RecordBookNumber} | Сер. бал: {AverageGrade:F2} | Статус: {Status}";
        }
        // 4. РЕАЛІЗАЦІЯ CalculateScholarship (вимога ПР №5)
        public override decimal CalculateScholarship()
        {
            // Твоя логіка: якщо середній бал > 90 (або 4.0 за старою шкалою)
            return AverageGrade >= 90 ? 2000m : 0m;
        }
        // --- Всі твої методи з ПР №4 залишаються без змін ---
        public void AddLabGrade(int labNumber, byte grade)
        {
            if (labNumber < 0 || labNumber >= LabGrades.Length)
                throw new IndexOutOfRangeException("Номер лабораторної має бути від 0 до 9");
            LabGrades[labNumber] = grade;
        }
        public bool IsExcellent() => AverageGrade >= 90;
        public bool IsFailing() => AverageGrade < 60;
        public object Clone()
        {
            var clone = (Student)this.MemberwiseClone();
            clone.LabGrades = (byte[])this.LabGrades.Clone();
            clone.Grades = new List<GradePoint>(this.Grades);
            return clone;
        }
        // Оператори порівняння та додавання залишаються такими ж
        public static bool operator >(Student s1, Student s2) => s1.AverageGrade > s2.AverageGrade;
        public static bool operator <(Student s1, Student s2) => s1.AverageGrade < s2.AverageGrade;


        public static bool operator ==(Student s1, Student s2) => s1?.AverageGrade == s2?.AverageGrade;
        public static bool operator !=(Student s1, Student s2) => !(s1 == s2);
        public static string operator +(Student s1, Student s2) => $"Команда: {s1.LastName} та {s2.LastName}";

        public override bool Equals(object obj) => obj is Student s && this == s;
        public override int GetHashCode() => (AverageGrade, CourseProgress).GetHashCode();
    }
}
