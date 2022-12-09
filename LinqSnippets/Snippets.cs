using LinqSnippets.Models;

namespace LinqSnippets;
public class Snippets
{
    public static void BasicLinQ()
    {
        List<string> cars = new List<string>()
        {
            "VW Golf",
            "VW California",
            "Audi A3",
            "Fiat Punto",
            "Seat Ibiza",
            "Seat León",
        };

        // select * from cars
        IEnumerable<string> carList = from car in cars select car;

        foreach (string car in carList)
        {
            System.Console.WriteLine(car);
        }

        // select * from cars where car == "Audi"
        IEnumerable<string> carsAudi = from car in cars where car.Contains("Audi") select car;

        foreach (string carAudi in carsAudi)
        {
            System.Console.WriteLine(carAudi);
        }
    }

    static public void LinqNumbers()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // Each number multiplied by 3
        // take all numbers, but 9
        // Order number by ascending value
        IEnumerable<int> processNumberList =
        numbers
        .Select(num => num * 3) // 3, 6, 9, 12, 15 etc.
        .Where(num => num != 9) // all (3, 6, 9, 12, 15 etc.) but the 9
        .OrderBy(num => num); // at the end, we order ascending
    }

    public static void SearchExamples()
    {
        List<string> textList = new List<string>()
        {
            "a",
            "bx",
            "c",
            "d",
            "e",
            "cj",
            "f",
            "c"
        };

        // First of all elements
        string first = textList.First();
        // First element that is "f"
        string f = textList.First(text => text.Equals("f"));
        // First element that contains "j"
        string j = textList.First(text => text.Contains("j"));
        // First element that contains z or default
        string? firstOrDefault = textList.FirstOrDefault(text => text.Contains("z"));
        // Last element that contains z or default
        string? lastOrDefault = textList.LastOrDefault(text => text.Contains("z"));
        // Single value
        string unique = textList.Single();
        string? uniqueOrDefault = textList.SingleOrDefault();

        int[] evenNumbers = { 0, 2, 4, 6, 8 };
        int[] othersEvenNumbers = { 0, 2, 6 };
        // Obtain 4 and 8
        IEnumerable<int> myEvenNumbers = evenNumbers.Except(othersEvenNumbers);
    }

    public static void MultipleSelects()
    {
        // select many
        string[] myOpinions = {
            "Opinion 1, text 1",
            "Opinion 2, text 2",
            "Opinion 3, text 3",
            "Opinion 4, text 4",
        };

        IEnumerable<string> myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

        List<Enterprise> enterprises = new List<Enterprise>()
        {
            new Enterprise()
            {
                Id = 1,
                Name = "Empresa 1",
                Employees = new List<Employee>()
                {
                    new Employee(){ Id = 1, Name = "Nombre 1", Salary = 100, Email = "correo1@gmail.com" },
                    new Employee(){ Id = 2, Name = "Nombre 2", Salary = 200, Email = "correo2@gmail.com" },
                    new Employee(){ Id = 3, Name = "Nombre 3", Salary = 300, Email = "correo3@gmail.com" },
                }
            },
            new Enterprise()
            {
                Id = 2,
                Name = "Empresa 2",
                Employees = new List<Employee>()
                {
                    new Employee(){ Id = 4, Name = "Nombre 4", Salary = 400, Email = "correo4@gmail.com" },
                    new Employee(){ Id = 5, Name = "Nombre 5", Salary = 500, Email = "correo5@gmail.com" },
                    new Employee(){ Id = 6, Name = "Nombre 6", Salary = 600, Email = "correo6@gmail.com" },
                }
            },
            new Enterprise()
            {
                Id = 3,
                Name = "Empresa 3",
                Employees = new List<Employee>()
                {
                    new Employee(){ Id = 7, Name = "Nombre 7", Salary = 700, Email = "correo7@gmail.com" },
                    new Employee(){ Id = 8, Name = "Nombre 8", Salary = 800, Email = "correo8@gmail.com" },
                    new Employee(){ Id = 9, Name = "Nombre 9", Salary = 900, Email = "correo9@gmail.com" },
                }
            },
        };

        // Get all employees of all enterprise
        IEnumerable<Employee> employees = enterprises.SelectMany(enterprises => enterprises.Employees);

        // Know if an a list is empty
        bool hasEnterprise = enterprises.Any();

        // Know if an a list contains employee
        bool hasEmployee = enterprises.Any(enterprises => enterprises.Employees.Any());

        // All enterprise at least has an employees with more than 200 of salary
        bool hasEmployeeWithSalaryMore100 = enterprises.Any(enterprises => enterprises.Employees.Any(employees => employees.Salary > 200));

    }

    public static void LinqCollections()
    {
        List<string> FirstLists = new List<string>() { "a", "b", "c" };
        List<string> SecondLists = new List<string>() { "a", "c", "d" };

        // Inner join
        IEnumerable<Object> commonResult = from firstElement in FirstLists join secondElement in SecondLists on firstElement equals secondElement select new { firstElement, secondElement };
        // or
        IEnumerable<Object> commonResult2 = FirstLists.Join(
            SecondLists, // Lista con la que se va a unir/relacionarla => Inner
            element => element, // element será element => OuterKeySelector 
            secondElement => secondElement, // secondElement será secondElement => InnerKeySelector
            (element, secondElement) => new { element, secondElement }); // ResultSelector

        // Outer join - left
        IEnumerable<Object> lefOuterJoin = from firstElement in FirstLists // se obtiene la primer lista
                                           join secondElement in SecondLists // se une con la segunda lista
                                           on firstElement equals secondElement // se obtienen todos los elementos que son igual entre FirstList y SecondList
                                           into temporalList // Los elements iguales, se obtienen y se guardan en una lista temporal
                                           from temporalElement in temporalList.DefaultIfEmpty() // se itera la lista temporal 
                                           where firstElement != temporalElement // donde el firstElement sea diferente de temporalElement 
                                           select new { Element = firstElement }; // se obtiene ese elemento que no sea igual al de la lista de la derecha

        IEnumerable<Object> lefOuterJoin2 = from firstElement in FirstLists
                                            from secondElement in SecondLists.Where(element => element == firstElement).DefaultIfEmpty()
                                            select new { FirstElement = firstElement, Secondelement = secondElement };

        // Outer join - right
        IEnumerable<Object> rightOuterJoin = from secondElement in SecondLists // se obtiene la segunda lista
                                             join firstElement in FirstLists // se une con la primer lista
                                             on secondElement equals firstElement // se obtienen todos los elementos que son igual entre FirstList y SecondList
                                             into temporalList // Los elements iguales, se obtienen y se guardan en una lista temporal
                                             from temporalElement in temporalList.DefaultIfEmpty() // se itera la lista temporal 
                                             where secondElement != temporalElement // donde el secondElement sea diferente de temporalElement 
                                             select new { Element = secondElement }; // se obtiene ese elemento que no sea igual al de la lista de la derecha

        // Union
        IEnumerable<Object> unionList = lefOuterJoin.Union(rightOuterJoin);
    }

    public static void SkipTakeLinq()
    {
        List<int> myList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // SKIP
        IEnumerable<int> skipTwoFirstValues = myList.Skip(2); // { 3, 4, 5, 6, 7, 8, 9, 10 }
        IEnumerable<int> skipTwoLastValues = myList.SkipLast(2); // { 1, 2, 3, 4, 5, 6, 7, 8 }
        IEnumerable<int> skipWhile = myList.SkipWhile(number => number < 4); // { 4, 5, 6, 7, 8, 9, 10 }

        // TAKE
        IEnumerable<int> takeTwoFirstValues = myList.Take(2); // { 1, 2 }
        IEnumerable<int> takeTwoLastValues = myList.TakeLast(2); // { 9, 10 }
        IEnumerable<int> takeWhile = myList.TakeWhile(number => number < 4); // { 1, 2, 3  }
    }

    // Paging with skip & take
    public static IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int page, int resultPage)
    {
        int startIndex = (page - 1) * resultPage;
        return collection.Skip(startIndex).Take(resultPage);
    }

    // Variables
    public static void LinqVariables()
    {
        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        IEnumerable<int> aboveAverage = from number in numbers
                                        let average = numbers.Average()// Guardar variables 
                                        let nSquared = Math.Pow(number, 2)
                                        where nSquared > average
                                        select number;

        foreach (int number in aboveAverage)
        {
            System.Console.WriteLine($"Query: Number => {number} Square => {Math.Pow(number, 2)}");
        }
    }

    // ZIP
    public static void ZipLinq()
    {
        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5 };
        List<string> stringNumbers = new List<string>() { "one", "two", "three", "four", "five" };

        IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => $"{number} = {word}");
    }

    // Repeat & Range
    public static void RepeatRangeLinq()
    {
        // Generate collection from 1-1000
        var first1000 = Enumerable.Range(0, 1000); // => Range

        IEnumerable<int> aboveAverage = from number in first1000
                                        select number;

        // Repeat a value N items
        IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5); // { "X", "X", "X", "X", "X" }
    }

    public static void StudentsLinq()
    {
        List<Student> students = new List<Student>()
        {
            new Student()
            {
                Id = 0,
                Name = "Nombre 0",
                Grade = 90,
                Certified = false
            },
            new Student()
            {
                Id = 1,
                Name = "Nombre 1",
                Grade = 80,
                Certified = true
            },
            new Student()
            {
                Id = 2,
                Name = "Nombre 2",
                Grade = 30,
                Certified = false
            },
            new Student()
            {
                Id = 3,
                Name = "Nombre 3",
                Grade = 65,
                Certified = true
            },
            new Student()
            {
                Id = 4,
                Name = "Nombre 4",
                Grade = 10,
                Certified = true
            },
            new Student()
            {
                Id = 5,
                Name = "Nombre 5",
                Grade = 45,
                Certified = false
            }
        };

        IEnumerable<Student> certifiedStudents = from student in students
                                                 where student.Certified
                                                 select student;

        IEnumerable<Student> notCertifiedStudents = from student in students
                                                    where !student.Certified
                                                    select student;

        IEnumerable<string> approveStudents = from student in students
                                              where student.Grade >= 60 && student.Certified
                                              select student.Name;
    }

    // All
    public static void AllLinq()
    {
        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5 };

        bool allAreSmallerThan10 = numbers.All(x => x < 10); // true
        bool allAreBiggerOrEqualThan2 = numbers.All(x => x > 2); // false

        List<int> emptyList = new List<int>();
        bool allNumbersAreGreaterThan0 = emptyList.All(x => x >= 0); // true
    }

    // Aggregate
    public static void AggregateQueries()
    {
        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // Sum all numbers
        int sum = numbers.Aggregate((prevSum, current) => prevSum + current);

        List<string> words = new List<string>() { "hello", "my", "name", "is", "Erick" };

        string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);
    }

    // Distinct
    public static void DistinctValues()
    {
        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 5, 4, 3 };
        IEnumerable<int> distinct = numbers.Distinct(); // 1, 2, 3, 4, 5
    }

    // GroupBy
    public static void GroupBy()
    {
        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        // Get only even numbers and generate two groups
        IEnumerable<IGrouping<bool, int>> grouped = numbers.GroupBy(x => x % 2 == 0); // Devuelve dos grupos: un grupo que no cumplen la condición y otro grupo que si cumple

        foreach (IGrouping<bool, int> group in grouped)
        {
            foreach (int value in group)
            {
                System.Console.WriteLine(value); // 1, 2, 5, 7, 9 ... 2, 4, 6, 8
            }
        }

        List<Student> students = new List<Student>()
        {
            new Student()
            {
                Id = 0,
                Name = "Nombre 0",
                Grade = 90,
                Certified = false
            },
            new Student()
            {
                Id = 1,
                Name = "Nombre 1",
                Grade = 80,
                Certified = true
            },
            new Student()
            {
                Id = 2,
                Name = "Nombre 2",
                Grade = 30,
                Certified = false
            },
            new Student()
            {
                Id = 3,
                Name = "Nombre 3",
                Grade = 65,
                Certified = true
            },
            new Student()
            {
                Id = 4,
                Name = "Nombre 4",
                Grade = 10,
                Certified = true
            },
            new Student()
            {
                Id = 5,
                Name = "Nombre 5",
                Grade = 45,
                Certified = false
            }
        };

        IEnumerable<IGrouping<bool, Student>> approvedQuery = students.GroupBy(student => student.Certified && student.Grade >= 60);

        // We obtain two groups
        // 1. Not certified students
        // 2. Certified & grade >= 60 students

        foreach (IGrouping<bool, Student> studentApproved in approvedQuery)
        {
            System.Console.WriteLine($"----- {studentApproved.Key} -----");
            foreach (Student student in studentApproved)
            {
                System.Console.WriteLine($"Student approved: {student.Name}");
            }
        }
    }

    public static void RelationsLinq()
    {
        List<Post> posts = new List<Post>()
        {
            new Post()
            {
                Id = 1,
                Title = "My first post",
                Content = "My first content",
                Created = DateTime.Now,
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Id = 1,
                        Title = "My first comment",
                        Content = "My first content",
                        Created = DateTime.Now
                    },
                    new Comment()
                    {
                        Id = 2,
                        Title = "My second comment",
                        Content = "My second content",
                        Created = DateTime.Now
                    }
                }
            },
            new Post()
            {
                Id = 2,
                Title = "My second post",
                Content = "My second content",
                Created = DateTime.Now,
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Id = 3,
                        Title = "My 3th comment",
                        Content = "My 3th content",
                        Created = DateTime.Now
                    },
                    new Comment()
                    {
                        Id = 4,
                        Title = "My 4th comment",
                        Content = "My 4th content",
                        Created = DateTime.Now
                    }
                }
            }
        };

        IEnumerable<Object> commentsWithContent = posts.SelectMany(post => post.Comments, (post, comment) => new { PostId = post.Id, CommentContent = post.Content });
    }
}
