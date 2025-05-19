using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ispit.Model;

namespace Ispit.Konzola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            List<Ucenik> ucenici = new List<Ucenik>();
            string[] unosi = { };
            ushort maxDuljina;
            string rezultat;
            string okvir;
            Console.WriteLine("Početak rada...\nADD = dodaj ucenika\nPRINT = ispis svih ucenika\nREMOVE = ukloni ucenika\nCLEAR = ukloni sve ucenike\nQUIT = izlaz iz programa");
            do {
                Console.Write("\nUnesite komandu: ");
                string unos = Console.ReadLine();
                unos = unos.Trim().ToLower();
                if (unos == "quit")
                {
                    break;
                }
                switch (unos)
                {
                    case "add":
                        while (true)
                        {
                            Console.Write("\nUnesite podatke za ucenika ('ime, prezime, YYYY-MM-DD, a.bc' ili QUIT za odustajanje): ");
                            unos = Console.ReadLine();
                            if (unos.Trim().ToLower() == "quit")
                            {
                                break;
                            }
                            unosi = unos.Split(',');
                            if (unosi.Length != 4)
                            {
                                Console.WriteLine("Neispravan broj parametara!");
                                continue;
                            }
                            for (ushort i = 0; i < unosi.Length; i++)
                            {
                                unosi[i] = unosi[i].Trim();
                            }
                            if (Ucenik.ProvjeraUnosa(unosi[0], unosi[1], unosi[2], unosi[3]))
                            {
                                break;
                            }
                        }
                        if(unos.ToLower().Trim() == "quit")
                        {
                            break;
                        }
                        ucenici.Add(new Ucenik(unosi[0], unosi[1], DateTime.Parse(unosi[2]), Double.Parse(unosi[3], NumberStyles.Any, CultureInfo.InvariantCulture)));
                        Console.WriteLine("Dodan ucenik!");
                        if (ucenici.Count == 3)
                        {
                            maxDuljina = 0;
                            rezultat = "";
                            for( ushort i = 0; i < ucenici.Count; i++)
                            {
                                string meduRez = i + 1 + ".\t" + ucenici[i].ToString()+"\n";
                                if(meduRez.Length > maxDuljina)
                                {
                                    maxDuljina = (ushort)meduRez.Length;
                                }
                                rezultat += meduRez;
                            }
                            okvir = ""; 
                            for(ushort i = 0;i < maxDuljina; i++)
                            {
                                okvir += "-";
                            }
                            Console.WriteLine(okvir + "\n" + rezultat + okvir);
                        }
                        break;
                    case "print":
                        if(ucenici.Count == 0)
                        {
                            Console.WriteLine("EMPTY!");
                            break;
                        }
                        maxDuljina = 0;
                        rezultat = "";
                        for (ushort i = 0; i < ucenici.Count; i++)
                        {
                            string meduRez = i + 1 + ".\t" + ucenici[i].ToString() + "\n";
                            if (meduRez.Length > maxDuljina)
                            {
                                maxDuljina = (ushort)meduRez.Length;
                            }
                            rezultat += meduRez;
                        }
                        okvir = "";
                        for (ushort i = 0; i < maxDuljina; i++)
                        {
                            okvir += "-";
                        }
                        Console.WriteLine(okvir + "\n" + rezultat + okvir);
                        break;
                    case "remove":
                        if (ucenici.Count == 0)
                        {
                            Console.WriteLine("Nema ucenika za ukoniti!");
                            break;
                        }
                        while(true){
                            Console.Write("\nUnesite indeks ucenika kojeg zelite ukloniti (ili QUIT za odustajanje): ");
                            unos = Console.ReadLine();
                            if (unos.Trim().ToLower() == "quit")
                            {
                                break;
                            }
                            int indeks;
                            if (int.TryParse(unos, out indeks))
                            {
                                if (indeks < 1 || indeks > ucenici.Count)
                                {
                                    Console.WriteLine("Indeks izvan dosega!");
                                }
                                else
                                {
                                    ucenici.RemoveAt(indeks-1);
                                    Console.WriteLine($"Ucenik {indeks} uspješno uklonjen!");
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Indeks se nije uspio parsirati!");
                            }
                        }
                        break;
                    case "clear":
                        ucenici.Clear();
                        Console.WriteLine("Popis učenika uspješno obrisan!");
                        break;
                }
            } while (true);
            Console.WriteLine("\nKraj rada...");
        }
    }
}
