using System;
using System.Collections.Generic;

namespace Thomas_Saaty
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в программу анализа иерархий Томаса Саати.");
            Console.WriteLine("Введите количество критериев (целое, натуральное число). Их должно быть не менее двух.");

            int amountCrit; //количество критериев
            while (true) //цикл проверки числа
            {
                if ((int.TryParse(Console.ReadLine(), out int x)) && (x >= 2)) //условие проверки
                {
                    amountCrit = x;
                    break; //выход из цикла проверки
                }
                Console.WriteLine("Введено некорректное значение!");    
            }                                                            
                                                                         
                                                                         
            List<List<double>> matrix = new List<List<double>>();         // начало кода создания двумерного динамического массива
            for (int i = 0; i < amountCrit; i++)                          
            {                                                             
                List<double> row = new List<double>();                                  
                for (int j = 0; j < amountCrit; j++)                      
                {                                                         
                    row.Add(0);  //определение длины списка                                         
                }                                                         
                                                                          
                matrix.Add(row); //определение длины списка списков
            }                                                             // конец кода создания двумерного динамического массива

            for (int columns = 0; columns < amountCrit; columns++) //ввод критериев
            {
                for (int lines = columns + 1; lines < amountCrit; lines++)
                {
                    Console.WriteLine($"Насколько критерий под номером {columns + 1} важнее критеря под номером {lines}?");
                    Console.WriteLine($"Введите целое или дробное десятичное число, через enter");
                    while (true) //цикл проверки числа
                    {

                        if (double.TryParse(Console.ReadLine(), out double x)) //условие проверки
                        {
                            matrix[columns][lines] = x;
                            break; //выход из цикла проверки
                        }
                        Console.WriteLine("Введено некорректное значение!");
                    }
                }
            } // конец ввода критериев

            for (int columns = 0; columns < amountCrit; columns++) //заполнение оставшихся ячеек
            {
                for(int lines = 0; lines < amountCrit; lines++)
                {
                    if (columns == lines) 
                    {
                        matrix[columns][lines] = 1;
                    }
                    else
                    {
                        if(lines < columns) //обратные значения для отношений
                        {
                            matrix[columns][lines] = Math.Round(1 / matrix[lines][columns], 2); 
                        }
                    }

                }
            }

            double sumOfMatrix = 0;
            Console.WriteLine("Таблица введенных критериев:");
            for (int columns = 0; columns < amountCrit; columns++) //вывод матрицы в консоль и вычисление общей суммы
            {
                for (int lines = 0; lines < amountCrit; lines++)
                {
                    Console.Write($"{matrix[columns][lines]} | ");

                    sumOfMatrix += matrix[columns][lines];
                }
                Console.WriteLine();
            } 

            List<double> weitcoef = new List<double>(); //список весовых коэффициентов
            List<double> sumOfLines = new List<double>(); //список сумм значений в строках критериев
            for (int columns = 0; columns < amountCrit; columns++) //заполнение weitcoef и sumOfLines
            {
                sumOfLines.Add(0);
                weitcoef.Add(0);
                for (int lines = 0; lines < amountCrit; lines++)
                {
                    sumOfLines[columns] += matrix[columns][lines];
                }
                weitcoef[columns] = Math.Round(sumOfLines[columns] / sumOfMatrix, 2);
            }

            double sumWetCof = 0;
            double maxOfWetCof = 0;
            for(int i =0; i< amountCrit; i++) //поиск максимального весового коэффициента и их суммы 
            {
                sumWetCof += weitcoef[i]; 
                if (weitcoef[i] > maxOfWetCof)
                {
                    maxOfWetCof = weitcoef[i]; 
                }
            }

            if (sumWetCof != 1) //распределение остатка, если сумма неравна единице 
            {
                for (int i = 0; i < amountCrit; i++)
                {
                    if (weitcoef[i] == maxOfWetCof)
                    {
                        weitcoef[i] += Math.Round(1 - sumWetCof, 2);
                    }
                }
            }

            Console.Write($"Весовые коэффициеты: \n"); //вывод результата
            for (int i = 0; i < amountCrit; i++)
            {
                Console.WriteLine($"Под номером {i + 1} = {weitcoef[i]}.");
            }

        }
    }
}
