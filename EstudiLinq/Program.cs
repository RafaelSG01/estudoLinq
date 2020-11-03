using EstudoLinq.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace EstudoLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Entre com o caminho do arquivo: ");
            string path = Console.ReadLine();
            Console.Write("Entre com um Salário: ");
            double avgSalary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            try
            {
                List<Employee> employees = new List<Employee>();
                using(StreamReader sr = File.OpenText(path)){
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        string name = line[0];
                        string email = line[1];
                        double salary = double.Parse(line[2], CultureInfo.InvariantCulture);
                        employees.Add(new Employee(name, email, salary));
                    }
                }

                Console.WriteLine("Email dos funcionários com o salário maior que " + avgSalary.ToString("F2", CultureInfo.InvariantCulture) + ":");
                var emails = employees.Where(e => e.Salary > avgSalary).OrderBy(e => e.Email).Select(e => e.Email);
                foreach(string e in emails)
                {
                    Console.WriteLine(e);
                }

                var sumSalary = employees.Where(e => e.Name[0] == 'M').Sum(e => e.Salary);

                Console.WriteLine("A soma dos salários dos funcionários em que o nome começa com a letra 'M': " + sumSalary.ToString("F2", CultureInfo.InvariantCulture));
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
