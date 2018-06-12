﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Linq;

namespace ConsoleApp2
{





    [DataContract]
    class EURUSDDB : IEquatable<EURUSDDB>
    {
        [DataMember]
        public String Date { get; set; }

        [DataMember]
        public String Time { get; set; }

        [DataMember]
        public Double Open { get; set; }

        [DataMember]
        public Double High { get; set; }

        [DataMember]
        public Double Low { get; set; }

        [DataMember]
        public Double Close { get; set; }

        [DataMember]
        public Double Volume
        {
            get; set;
        }

        public EURUSDDB()
        {

        }



        public EURUSDDB(String Date, String Time = "", Double Open = 0, Double High = 0, Double Low = 0, Double Close = 0, Double Volume = 0)
        {
            this.Date = Date;
            this.Time = Time;
            this.Open = Open;
            this.High = High;
            this.Low = Low;

            this.Close = Close;
            this.Volume = Volume;

        }


        public bool Equals(EURUSDDB other)
        {
            if (other == null) return false;
            return (this.Open.Equals(other.Open));
        }

        public override string ToString()
        {
            return string.Format("Date: {0} \nTime: {1} \nOpen: {2} \nHigh: {3} \nLow: {4} \nClose: {5} \nVolume: {6}", Date, Time, Open, High, Low, Close, Volume);
        }



    }





    class Program
    {

        static List<EURUSDDB> getData(string path)
        {
            List<EURUSDDB> list = new List<EURUSDDB>();

            foreach (string line in File.ReadAllLines(path))
            {
                string[] attr = line.Split(',');
                for (int i = 0; i < attr.Length; i++)
                {
                    if ("(null)".Equals(attr[i])) attr[i] = "-";
                    if (attr[i] == null) attr[i] = "-";

                }

                EURUSDDB item = new EURUSDDB();
                item.Date = attr[0];
                item.Time = attr[1];
                item.Open = Double.Parse(attr[2]);
                item.High = Double.Parse(attr[3]);
                item.Low = Double.Parse(attr[4]);
                item.Close = Double.Parse(attr[5]);
                item.Volume = Double.Parse(attr[6]);


                list.Add(item);
            }


            return list;
        }






        static void Main(string[] args)
        {
            string path = "C:\\Users\\seraph\\source\\repos\\ConsoleApp2\\ConsoleApp2\\bin\\Debug\\myfile88.json";

            List<EURUSDDB> list = getData("C:\\Users\\seraph\\Desktop\\EURUSD07.txt.txt");
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            File.AppendAllText(path, output);


            String JSONstring = File.ReadAllText(path);
            List<EURUSDDB> myList = JsonConvert.DeserializeObject<List<EURUSDDB>>(JSONstring);

            /*myList.ForEach(Console.WriteLine);
            Console.ReadLine();
            */

            if (myList == null)
                myList = new List<EURUSDDB>();

            string input = " ";

            String inputDate;
            String inputTime;
            Double inputOpen = 0;
            Double inputHigh = 0;
            Double inputLow = 0;
            Double inputClose = 0;
            Double inputVolume = 0;

            while (input != "q")
            {
                Console.WriteLine("Press 'a' to ADD a new Item");
                Console.WriteLine("Press 'd' to DELETE  Item");
                Console.WriteLine("Press 's' to SHOW content");
                Console.WriteLine("Press 'q' to QUIT Program");
                Console.WriteLine("Press command");
                input = Console.ReadLine();
                switch (input) //Switch on input string
                {
                    case "a":
                        Console.WriteLine("Adding a new Price Element");

                        Console.WriteLine("Enter price date:");
                        inputDate = Console.ReadLine();

                        Console.WriteLine("Enter price time:");
                        inputTime = Console.ReadLine();


                        Console.WriteLine("Enter price Open:");
                        inputOpen = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Enter price High:");
                        inputHigh = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Enter price Low:");
                        inputLow = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Enter price Close:");
                        inputClose = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Enter price volume:");
                        inputVolume = Convert.ToDouble(Console.ReadLine());

                        myList.Add(new EURUSDDB(inputDate, inputTime, inputOpen, inputHigh, inputLow, inputClose, inputVolume));

                        Console.WriteLine("Date: " + inputDate + "Time: " + inputTime + "Open: " + inputOpen + "High: " + inputHigh + "Low: " + inputLow + "Close: " + inputClose + "Volume: " + inputVolume);

                        break;

                    case "d":
                        Console.WriteLine("Deleting Price Elements");
                        Console.WriteLine("Enter Date to DELETE:");
                        inputDate = Console.ReadLine();
                        // myList.Remove(new EURUSDDB(inputDate));


                        foreach (EURUSDDB chunk in myList.ToList())
                        {

                            if (chunk.Date == inputDate)
                            {
                                myList.Remove(chunk);
                            }
                        }
                        Console.WriteLine("Deleted item with name" + inputDate);

                        break;

                    case "q":
                        Console.WriteLine("Quit program");
                        break;

                    case "s":
                        Console.WriteLine("\nShowing Contents: ");

                        myList.ForEach(Console.WriteLine);

                        break;

                    default:
                        Console.WriteLine("Incorect command, try again");
                        break;


                }
            }

            Console.WriteLine("Rewriting json file");
            string data = JsonConvert.SerializeObject(myList);
            File.WriteAllText("C:\\Users\\seraph\\source\\repos\\ConsoleApp2\\ConsoleApp2\\bin\\Debug\\myfile88.json", data);
            Console.ReadLine();


        }

    }
}
