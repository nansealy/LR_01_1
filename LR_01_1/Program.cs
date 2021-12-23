using System;
using System.Collections.Generic;
using System.Linq;


/*
    Лабораторная работа #1. Решение нелинейных уравнений.
    Найти один из корней уравнения  методом половинного деления, методом хорд и методом касательных.
    Вариант #9: x^4 - 8x^3 - 2x^2 - 3
*/

namespace LR_01
{
    public class Program
    {

        // заданная функция f(x)
        public static double Function(double x)
        {
            return Math.Pow(x, 4) - 8 * Math.Pow(x, 3) - 2 * Math.Pow(x, 2) - 3;
        }

        // производная от заданной функции f'(x)
        public static double FunctionDerivative(double x)
        {
            return 4 * Math.Pow(x, 3) - 24 * Math.Pow(x, 2) - 4 * x;
        }

        // метод половинного деления
        // a, b - концы отрезка, eps - точность
        public static double HalfDivisionMethod(double a, double b, double eps)
        {
            if (Function(a) * Function(b) > 0)
            {
                throw new Exception("Метод половинного деления расходится.");
            }
            double middle;
            while (Math.Abs(b - a) > 2 * eps)
            {
                middle = (a + b) / 2;
                if (Function(a) * Function(middle) > 0)
                {
                    a = middle;
                }
                else
                {
                    b = middle;
                }
            }
            middle = (a + b) / 2;
            return middle;
        }

        // метод хорд
        // a, b - концы отрезка, eps - точность
        public static double ChordMethod(double a, double b, double eps)
        {
            if (Function(a) * Function(b) > 0)
            {
                throw new Exception("Метод хорд расходится.");
            }
            double c = a;
            double c0;
            do
            {
                c0 = c;
                c = a - ((b - a) / (Function(b) - Function(a))) * Function(a);
                if (Function(a) * Function(c) > 0)
                {
                    a = c;
                }
                else
                {
                    b = c;
                }
            } while (Math.Abs(c - c0) > eps);
            return c;
        }

        // метод касательных
        // с - начальное приближение, eps - точность
        public static double TangentMethod(double c0, double eps)
        {
            double c, c1;
            do
            {
                if (FunctionDerivative(c0) != 0)
                {
                    c = c0 - Function(c0) / FunctionDerivative(c0);
                    c1 = c0;
                    c0 = c;
                }
                else
                {
                    throw new Exception("Производная равна нулю, метод не может быть выполнен.");
                }
            } while (Math.Abs(c - c1) > eps);
            return c;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Введите данные: ");
            Console.Write("a = ");
            double a = double.Parse(Console.ReadLine());
            Console.Write("b = ");
            double b = double.Parse(Console.ReadLine());
            Console.Write("c = ");
            double c = double.Parse(Console.ReadLine());
            Console.Write("eps = ");
            double eps = double.Parse(Console.ReadLine());
            try
            {
                double halfDivResult = HalfDivisionMethod(a, b, eps);
                Console.WriteLine("Метод половинного деления = " + halfDivResult);
                Console.WriteLine("Test:\nf(x) = " + Function(halfDivResult));

                double chordsResult = ChordMethod(a, b, eps);
                Console.WriteLine("Метод хорд = " + chordsResult);
                Console.WriteLine("Test:\nf(x) = " + Function(chordsResult));

                double tangentResult = TangentMethod(c, eps);
                Console.WriteLine("Метод касательных = " + tangentResult);
                Console.WriteLine("Test:\nf(x) = " + Function(tangentResult));
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}