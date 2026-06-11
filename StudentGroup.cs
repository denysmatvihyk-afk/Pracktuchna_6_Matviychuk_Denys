public void ShowAllShapesInfo()
{
    Console.WriteLine("\n--- ІНФОРМАЦІЯ ПРО ВСІ ФІГУРИ ГРУПИ ---");
    foreach (var student in Students)
    {
        if (student.StudentShapes.Any())
        {
            Console.WriteLine($"Студент: {student.FullName}");
            foreach (var shape in student.StudentShapes)
            {
                // Поліморфний виклик через інтерфейс IPrintable (пункт 36)
                if (shape is IPrintable printable)
                {
                    Console.WriteLine($"  [Printable]: {printable.GetPrintInfo()}");
                }
                // Поліморфний виклик GetDescription (пункт 32)
                Console.WriteLine($"  [Опис]: {shape.GetDescription()}");
            }
        }
    }
}
