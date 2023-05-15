

using Spectre.Console;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.Arm;


namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.MarkupLine("[bold blue]Product Management App[/]");



            while (true)
            {
                AnsiConsole.MarkupLine("[bold red]Enter your Choice[/]");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. View Product");
                Console.WriteLine("3. View All Products");
                Console.WriteLine("4. Update the Product");
                Console.WriteLine("5. Delete the product");
                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddProduct();
                            break;
                        case 2:
                            ViewProduct();
                            break;
                        case 3:
                            ViewAllProducts();
                            break;
                        case 4:
                            UpdateProduct();
                            break;
                        case 5:
                            DeletetheProduct();
                            break;

                        default:
                            Console.WriteLine("Wrong Choice Entered!");
                            break;
                    }
                }

            }
        }
        public static void AddProduct()
        {
            SqlConnection con = new SqlConnection("Server=IN-8B3K9S3; database=productmanagement; Integrated Security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand($"insert into product values(@pname, @pdescr, @pquantity, @price)", con);
            Console.Write("Enter Product name: ");
            string pname = Console.ReadLine();
            Console.Write("Enter Product Description: ");
            string pdescr = Console.ReadLine();
            Console.Write("Enter Product Quantity: ");
            int pquantity = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Amount: ");
            int price = Convert.ToInt32(Console.ReadLine());
            cmd.Parameters.AddWithValue("@pname", pname);
            cmd.Parameters.AddWithValue("@pdescr", pdescr);
            cmd.Parameters.AddWithValue("@pquantity", pquantity);
            cmd.Parameters.AddWithValue("@price", price);
            int rowsaffected = cmd.ExecuteNonQuery();
            AnsiConsole.MarkupLine("[bold green]Product added successfully![/]");
            con.Close();

        }
        public static void ViewProduct()
        {
            SqlConnection con1 = new SqlConnection("Server=IN-8B3K9S3; database=productmanagement; Integrated Security=true");
            con1.Open();
            AnsiConsole.MarkupLine("[bold yellow]Enter the id to view the product[/]");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlCommand cmd1 = new SqlCommand($"select * from product where id = {id}", con1);
            SqlDataReader rd = cmd1.ExecuteReader();
            while (rd.Read())
            {
                for (int i = 0; i < rd.FieldCount; i++)
                {
                    Console.WriteLine($"{rd[i]}      |");
                }
                Console.WriteLine();
            }
            con1.Close();

        }
        public static void ViewAllProducts()
        {
            SqlConnection con2 = new SqlConnection("Server=IN-8B3K9S3; database=productmanagement; Integrated Security=true");
            con2.Open();
            SqlCommand cmd2 = new SqlCommand($"select * from product", con2);
            SqlDataReader rd = cmd2.ExecuteReader();
            while (rd.Read())
            {
                for (int i = 0; i < rd.FieldCount; i++)
                {
                    Console.WriteLine($"{rd[i]}      |");
                }
                Console.WriteLine();
            }
        }
        public static void UpdateProduct()
        {
            string connectionString = "Server=IN-8B3K9S3; database=productmanagement; Integrated Security=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                AnsiConsole.MarkupLine("[bold yellow]Enter the id to update the product[/]");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter the new Product Name:");
                string pname = Console.ReadLine();

                Console.WriteLine("Enter the new Product description:");
                string pdescr = Console.ReadLine();

                Console.Write("Enter Product Quantity: ");
                int pquantity = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter product Amount: ");
                int price = Convert.ToInt32(Console.ReadLine());
                string updateQuery = "UPDATE Product SET pname = @pname, pdescr = @pdescr,pquantity=@pquantity,price=@price WHERE id = @id";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@pname", pname);
                    command.Parameters.AddWithValue("@pdescr", pdescr);
                    command.Parameters.AddWithValue("@pquantity", pquantity);
                    command.Parameters.AddWithValue("@price", price);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        AnsiConsole.MarkupLine("[bold yellow]Product updated successfully[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold yellow]Product doesnot found[/]");

                    }
                }
                connection.Close();
            }
        }
        public static void DeletetheProduct()
        {

            string connectionString = "Server=IN-8B3K9S3; database=productmanagement; Integrated Security=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                AnsiConsole.MarkupLine("[bold yellow]Enter the id to delete the product[/]");
                int id = Convert.ToInt32(Console.ReadLine());
                string deleteQuery = "DELETE FROM Product WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        AnsiConsole.MarkupLine("[bold yellow]Product deleted successfully[/]");

                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold yellow]Product doesnot found[/]");

                    }
                }
                connection.Close();

            }
        }
    }
}