using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Bros.Models
{
    public class ProductFactory
    {

        static Random rand = new Random();

        public static Product Product
        {
            get
            {
            Product _Product = new Product(){
                    Name = RandomWord(),
                    Price = RandomDouble(),
                    Image = defaultPic(),
                    DateCreated = DateTime.Now,
                    Description = randomSentence(5)
                };
                return _Product;
            }
        }

        static private string RandomEnum<T>()
        {

            T[] items = (T[])Enum.GetValues(typeof(T));

            return items[rand.Next(0, items.Length)].ToString();

        }

        static byte[] defaultPic()
        {
            byte[] arr;
            System.Drawing.Image img = System.Drawing.Image.FromFile("~/Content/Images/defaultPic.jpg");
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();

            }
            return arr;
        }

        static string RandomZip()
        {
            string zip = "";
            for (int i = 0; i < 5; i++)
            {
                int x = rand.Next(0, 10);
                zip += (x + "");
            }

            return zip;
        }

        static string randomSentence(int num)
        {
            string toReturn = "";

            for (int i = 0; i < num; i++ )
            {
                for (int j = 0; j < (rand.Next(30) + 1); j++ )
                {
                    toReturn += RandomWord();
                }
                toReturn += ". ";
            }

            return toReturn;

        }

        static string RandomWord()
        {
            string word = "";
            int length = rand.Next(4, 15);

            for (int i = 0; i < length; i++)
            {
                word += RandomLetter(i);
            }


            return word;
        }

        static double RandomDouble()
        {
            return (double)rand.Next(100)+1;
        }

        static char RandomLetter(int index)
        {
            char letter = ' ';
            if (index == 0)
            {
                letter = (char)rand.Next(65, 91);
            }
            else
            {
                letter = (char)rand.Next(97, 123);
            }

            return letter;
        }
    }
}