using System;


namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] str =
            {
                {'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж'},
                {'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О'},
                {'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц'},
                {'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю'},
                {'Я', 'а', 'б', 'в', 'г', 'д', 'е', 'ё'},
                {'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н'},
                {'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х'},
                {'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э'},
                {'ю', 'я', ' ', '.', ':', '!', '?',','},
            };

            Console.WriteLine("Зашифровать строку введите 1");
            Console.WriteLine("Расшифровать строку введите 2");
            Console.WriteLine("Закончить введите 0");

            string run = "";
            run = Console.ReadLine();
            while ( run != "0")
            {
                if(run == "1")
                {
                    Console.WriteLine("Введите строку");
                    string input = Console.ReadLine();
                    string[] code = new string[input.Length];
                    //провеку на валидность добавить
                    bool sucsessFlag = true;

                    for (int m = 0; m < input.Length; m++)
                    {
                        bool flag = true;

                        for (int i = 0; i < 9; i++)
                        {
                            if (!flag)
                            {
                                break;
                            }

                            for (int j = 0; j < 8; j++)
                            {        
                                if (input[m] == str[i, j])
                                {
                                    code[m] = i.ToString() + j.ToString();
                                    flag = false;
                                    break;
                                }
                            }
                        }

                        if (flag)
                        {
                            sucsessFlag = false;
                            Console.WriteLine("В ведённой строке присутствует недопустимый символ\n");
                            break;
                        }
                    }

                    if (sucsessFlag)
                    {
                        Console.WriteLine("Зашифрованная строка:\n");
                        for (int i = 0; i < code.Length; i++)
                        {
                            Console.Write(code[i] + " ");
                        }
                        Console.WriteLine();
                    }

                }

                if (run == "2")
                {
                    Console.WriteLine("Введите зашифрованную строку");
                    string [] code = Console.ReadLine().Split(' ');
                    string[] output = new string[code.Length];

                    for (int m = 0; m < code.Length; m++)
                    {
                        output[m] = str[int.Parse(code[m].Substring(0, 1)), int.Parse(code[m].Substring(1, 1))].ToString();
                        
                    }
                    Console.WriteLine("Исходная строка:\n");
                    for (int i = 0; i < output.Length; i++)
                    {
                        Console.Write(output[i]);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("Зашифровать строку введите 1");
                Console.WriteLine("Расшифровать строку введите 2");
                Console.WriteLine("Закончить введите 0");
                run = Console.ReadLine();
            }
        }
    }
}
