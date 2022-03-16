using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TugasValidation
{
    public class User
    {
        private static List<UserData> Data { get; set; } = new List<UserData>();
        UserData temp = new UserData();
        public void Show()
        {
            Console.Clear();
            Console.WriteLine("--== Show User ==--");
            foreach (var item in Data)
            {
                Console.WriteLine("=======================");
                Console.WriteLine($"Name : {item.FirstName} {item.LastName}");
                Console.WriteLine($"Username : {item.UserName}");
                Console.WriteLine($"Password : {item.Password}");
                Console.WriteLine("=======================");
            }
            var pilih = Console.ReadKey();
            switch (pilih)
            {
                default:
                    Program.Main();
                    break;
            }
        }
        public void Create()
        {
            var hasSymbol = new Regex(@"[@#$%^&+=]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowwerChar = new Regex(@"[a-z]+");
            Console.Clear();
            Console.WriteLine("--== Create User ==--");
            Console.Write("First Name : ");
            temp.FirstName = Console.ReadLine();
            Console.Write("Last Name : ");
            temp.LastName  = Console.ReadLine();
            temp.UserName = temp.FirstName + temp.LastName;
            foreach (var item in Data)
            {
                if (item.UserName.Equals(temp.UserName))
                {
                    Random rand = new Random();
                    temp.UserName = temp.UserName + rand.Next(1, 99);
                }
            }
            Boolean count = false;
            do
            {
                Console.Write("Password : ");
                String ps = Console.ReadLine();
                if (hasSymbol.IsMatch(ps) && hasUpperChar.IsMatch(ps) && hasLowwerChar.IsMatch(ps))
                {
                    temp.Password = BCrypt.Net.BCrypt.HashPassword(ps);
                    Data.Add(temp);
                    Console.WriteLine("Create New Account Success");
                    var pilih = Console.ReadKey();
                    switch (pilih)
                    {
                        default:
                        Program.Main();
                        break;
                    }
                }
                else 
                {
                    Console.WriteLine("(password must include LowerCase, UpperCase, and Symbol");
                    count = true;
                }

            } 
            while (count);  
        }
        public void Login() {
            Console.Clear();
            Console.WriteLine("--== Login User ==--");
            Console.Write("Username : ");
            string userName = Console.ReadLine();
            Console.Write("Password : ");
            string password = Console.ReadLine();

            int count = 0;
            foreach (var item in Data)
            {
                bool validPassword = BCrypt.Net.BCrypt.Verify(password, item.Password);
                if (item.UserName.Equals(userName) && validPassword)
                {
                    Console.WriteLine("login berhasil");
                    count = 1;
                }
            }
            if (count != 1)
            {
                Console.WriteLine("Login tidak berhasil");
            }
            var pilih = Console.ReadKey();
            switch (pilih)
            {
                default:
                    Program.Main();
                    break;
            }
        }
        public void Search() {
            Console.Clear();
            Console.WriteLine("--== Search User ==--");
            Console.Write("Masukkan keyword : ");
            String keyword = Console.ReadLine();
            int count = 0;
            foreach (var item in Data)
            {
                if (item.FirstName.Equals(keyword) || item.LastName.Equals(keyword) || item.UserName.Equals(keyword))
                {
                    Console.WriteLine("=======================");
                    Console.WriteLine($"Name : {item.FirstName} {item.LastName}");
                    Console.WriteLine($"Username : {item.UserName}");
                    Console.WriteLine($"Password : {item.Password}");
                    Console.WriteLine("=======================");
                    count = 1;
                }
            }
            if (count != 1)
            {
                Console.WriteLine("Data tidak ditemukan!");
            }
            var pilih = Console.ReadKey();
            switch (pilih)
            {
                default:
                    Program.Main();
                    break;
            }
        }

        public void Update()
        {
            Console.Clear();
            Console.WriteLine("--== Update User ==--");
            Console.Write("Masukkan Username : ");
            string userName = Console.ReadLine();
            int count = 0;
            for (int i = 0; i < Data.Count; i++)
            {
                if (Data[i].UserName.Equals(userName))
                {
                    Data.RemoveAt(i);
                    Console.Write("First Name : ");
                    temp.FirstName = Console.ReadLine();
                    Console.Write("Last Name : ");
                    temp.LastName = Console.ReadLine();
                    temp.UserName = temp.FirstName + temp.LastName;
                    foreach (var item in Data)
                    {
                        if (item.UserName.Equals(temp.UserName))
                        {
                            Random rand = new Random();
                            temp.UserName = temp.UserName + rand.Next(1, 99);
                        }
                    }
                    Console.Write("Password : ");
                    String pass = Console.ReadLine();
                    temp.Password = BCrypt.Net.BCrypt.HashPassword(pass);
                    Data.Insert(i, temp);
                    count = 1;
                }
            }
            if (count != 1)
            {
                Console.WriteLine("Tidak ada data yang terupdate");
            }
            else
            {
                Console.WriteLine("Data sudah terupdate");
            }
            var pilih = Console.ReadKey();
            switch (pilih)
            {
                default:
                    Program.Main();
                    break;
            }
        }

        public void Delete()
        {
            Console.Clear();
            Console.WriteLine("--== Delete User ==--");
            Console.Write("Masukkan username : ");
            string userName = Console.ReadLine();
            int count = 0;
            for (int i = 0; i < Data.Count; i++)
            {
                if (Data[i].UserName.Equals(userName))
                {
                    Data.RemoveAt(i);
                    count = 1;
                }
            }
            if (count == 1)
            {
                Console.WriteLine("Data Sudah Terhapus");
            }
            else 
            {
                Console.WriteLine("Tidak ada data yang terhapus");
            }
            var pilih = Console.ReadKey();
            switch (pilih)
            {
                default:
                    Program.Main();
                    break;
            }
        }
    }
}
