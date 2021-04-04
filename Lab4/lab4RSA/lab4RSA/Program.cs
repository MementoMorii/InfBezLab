using System;
using System.Collections.Generic;
using System.Numerics;

namespace lab4RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] alphavit = new char[]
            {
                'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з',
                'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р',
                'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ',
                'ъ', 'ы', 'ь', 'э', 'ю', 'я', 'А', 'Б', 'В',
                'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К',
                'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У',
                'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь',
                'Э', 'Ю', 'Я', ' ', '.', ':', '!', '?', ','
            };
            int p, q;
            Console.Write("Введите p: ");
            while (!int.TryParse(Console.ReadLine(), out p))
            {
                Console.Write("p должно быть числом, введите р заново");
            }
            
            Console.Write("Введите q: ");
            while (!int.TryParse(Console.ReadLine(), out q))
            {
                Console.Write("q должно быть числом, введите q заново");
            }

            Console.WriteLine("Зашифровать строку введите 1");
            Console.WriteLine("Расшифровать строку введите 2");
            Console.WriteLine("Ввести новые р и q введите 3");
            Console.WriteLine("Закончить введите 0");
            string run = Console.ReadLine();
            
            while (run!="0")
            {
                if (run == "3")
                {
                    Console.Write("Введите p: ");
                    while (!int.TryParse(Console.ReadLine(), out p))
                    {
                        Console.Write("p должно быть числом, введите р заново");
                    }

                    Console.Write("Введите q: ");
                    while (!int.TryParse(Console.ReadLine(), out q))
                    {
                        Console.Write("q должно быть числом, введите q заново");
                    }
                }

                int n = 0;
                int f = 0;
                int d = 0;
                int e_ = 0;
                

                //зашифровка строки
                if (run == "1")
                {
                    if (SimpleNumber(p) && SimpleNumber(q)) //если p и q простые
                    {
                        Console.Write("Введите строку: ");
                        string s = Console.ReadLine();

                        n = p * q;
                        f = (p - 1) * (q - 1);
                        d = Calculate_d(f);
                        e_ = Calculate_e(d, f);

                        Console.WriteLine("\nn =  " + n);
                        Console.WriteLine("f =  " + f);
                        Console.WriteLine("d =  " + d);
                        Console.WriteLine("e =  " + e_);
                        Console.WriteLine("\nОткрытый ключ: " + "{" + e_ + ", " + n + "}");
                        Console.WriteLine("Закрытый ключ: " + "{" + d + ", " + n + "}");

                        ///// ШИФРОВАНИЕ /////
                        bool flag = true;
                        List<object> obj = Encode(s, e_, n, flag); //Вызов метода Encode
                        if (!(bool)obj[1]) //проверка на недопустимые символы
                        {
                            Console.WriteLine("В ведённой строке есть недопустимые символы");
                            continue;
                        }
                        List<string> result = (List<string>)obj[0];
                        List<string> input = new List<string>();

                        Console.WriteLine("\nЗашифрованный текст: \n");
                        foreach (string item in result)
                        {
                            Console.Write(item + " ");
                            input.Add(item);
                        }
                        Console.WriteLine();
                        Console.WriteLine();

                    }
                    else
                    {
                        Console.Write("p и q должны быть простыми числами");
                    }
                }

                //расшифровка строки
                if (run == "2")
                {
                    if (SimpleNumber(p) && SimpleNumber(q)) //если p и q простые
                    {
                        Console.Write("Введите строку: ");
                        string[] s = Console.ReadLine().Split(' ');

                        n = p * q;
                        f = (p - 1) * (q - 1);
                        d = Calculate_d(f);
                        e_ = Calculate_e(d, f);

                        Console.WriteLine("\nn =  " + n);
                        Console.WriteLine("f =  " + f);
                        Console.WriteLine("d =  " + d);
                        Console.WriteLine("e =  " + e_);
                        Console.WriteLine("\nОткрытый ключ: " + "{" + e_ + ", " + n + "}");
                        Console.WriteLine("Закрытый ключ: " + "{" + d + ", " + n + "}");  
                        
                        List<string> input = new List<string>();

                        foreach (string item in s)
                        {
                            input.Add(item);
                        }

                        ///// ДЕШИФРОВАНИЕ /////
                        string result_2 = Decode(input, d, n); //Вызов метода Decode
                        Console.WriteLine("\nРасшифрованный текст: " + result_2);
                    }
                    else
                    {
                        Console.Write("p и q должны быть простыми числами");
                    }

                }

               
                Console.WriteLine("Зашифровать строку введите 1");
                Console.WriteLine("Расшифровать строку введите 2");
                Console.WriteLine("Ввести новые р и q введите 3");
                Console.WriteLine("Закончить введите 0");
                run = Console.ReadLine();
            }
                     
            ///// МЕТОДЫ /////
            static bool SimpleNumber(int n)  //Проверка, является ли число простым
            {
                if (n < 2)
                    return false;

                if (n == 2)
                    return true;

                for (int i = 2; i < n; i++)
                    if (n % i == 0)
                        return false;

                return true;
            }

            static int Calculate_d(int f)   //Вычисление d, оно должно быть взаимно простым с m
            {
                int d = f - 1;  //по условию, d должно быть меньше f

                for (int i = 2; i <= f; i++)
                    if ((f % i == 0) && (d % i == 0)) //если f и d имеют общие делители, то d уменьшается, 
                                                      //иначе получаем d
                    {
                        d--;
                        i = 1;
                    }

                return d;
            }

            static int Calculate_e(int d, int f)  //Вычисление e по формуле
            {
                int e = 200;

                while (true)
                {
                    if ((e * d) % f == 1)  //e должно быть взаимно простым с f, если так, то берем берем e
                        break;
                    else
                        e++;
                }

                return e;
            }

            List<object> Encode(string s, int e, int n, bool flag)  //Шифрование 
            {
                List<string> result = new List<string>();
                List<object> ob = new List<object>();
                BigInteger bi;

                for (int i = 0; i < s.Length; i++)
                {
                    int index = Array.IndexOf(alphavit, s[i]);  //получаем номер буквы в алфавите
                    if (index == -1) // проверка на допустивые символы
                    {
                        flag = false;
                        break;
                    }

                    bi = new BigInteger(index);
                    bi = BigInteger.Pow(bi, (int)e);   //возведим в степень e_ номер буквы

                    BigInteger n_ = new BigInteger((int)n);

                    bi = bi % n_;  //получаем шифр буквы

                    result.Add(bi.ToString());
                }
                if (!flag)
                {
                    ob.Add(result);
                    ob.Add(false);
                }
                else
                {
                    ob.Add(result);
                    ob.Add(true);
                }
                return ob;
            }

            string Decode(List<string> input, long d, long n)
            {
                string result = "";

                BigInteger bi;

                foreach (string item in input)
                {
                    bi = new BigInteger(Convert.ToDouble(item));
                    bi = BigInteger.Pow(bi, (int)d);   //возведим в степень d шифр буквы

                    BigInteger n_ = new BigInteger((int)n);

                    bi = bi % n_;  //получаем номер буквы в алфавите

                    int index = Convert.ToInt32(bi.ToString());

                    result += alphavit[index].ToString(); //добавляем полученную букву в строку
                }

                return result;
            }

        }
    }
}
