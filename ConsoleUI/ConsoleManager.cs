using Business.Concrete;
using Business.Constants;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class ConsoleManager
    {
        private bool flag = true;

        CarManager _carManager;
        ColorManager _colorManager;
        BrandManager _brandManager;
        UserManager _userManager;
        CustomerManager _customerManager;
        RentalManager _rentalManager;

        public ConsoleManager()
        {
            _carManager = new CarManager(new EfCarDal());
            _colorManager = new ColorManager(new EfColorDal());
            _brandManager = new BrandManager(new EfBrandDal());
            _userManager = new UserManager(new EfUserDal());
            _customerManager = new CustomerManager(new EfCustomerDal());
            _rentalManager = new RentalManager(new EfRentalDal());
        }

        public void Dashboard()
        {
            while (flag)
            {
                Console.WriteLine("\n*************** Main Menu ***************");
                Console.WriteLine("          1. Car Menu");
                Console.WriteLine("          2. Brand Menu");
                Console.WriteLine("          3. Color Menu");
                Console.WriteLine("          4. User Menu");
                Console.WriteLine("          5. Customer Menu");
                Console.WriteLine("          6. Rental Menu");
                Console.WriteLine("          7. Exit \n");

                char key = Console.ReadLine()[0];
                switch (key)
                {
                    case '1':
                        CarMenu();
                        break;
                    case '2':
                        BrandMenu();
                        break;
                    case '3':
                        ColorMenu();
                        break;
                    case '4':
                        UserMenu();
                        break;
                    case '5':
                        CustomerMenu();
                        break;
                    case '6':
                        RentACarMenu();
                        break;
                    case '7':
                        Console.WriteLine("*************** Have a nice day. Good Bye :) ***************");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("\nYou entered an incorrect value! Please try again.");
                        break;
                }
            }
        }

       
        #region CarMenu
        public void CarMenu()
        {
            while (flag)
            {
                Console.WriteLine("\n*************** Car Menu ***************");
                Console.WriteLine("          1. Add new Car");
                Console.WriteLine("          2. Delete a Car");
                Console.WriteLine("          3. Update a Car");
                Console.WriteLine("          4. View Cars by Brand ID");
                Console.WriteLine("          5. View Cars by Color ID");
                Console.WriteLine("          6. View the list of all Car");
                Console.WriteLine("          7. View the Car Detail List");
                Console.WriteLine("          8. Go to Main Menu");
                Console.WriteLine("          9. Exit \n");

                char key = Console.ReadLine()[0];
                switch (key)
                {
                    case '1':
                        AddCar();
                        break;
                    case '2':
                        DeleteCar();
                        break;
                    case '3':
                        UpdateCar();
                        break;
                    case '4':
                        CarsByBrandID();
                        break;
                    case '5':
                        CarsByColorID();
                        break;
                    case '6':
                        GetCarList();
                        break;
                    case '7':
                        ViewCarDetailList();
                        break;
                    case '8':
                        Dashboard();
                        break;
                    case '9':
                        Console.WriteLine("*************** Have a nice day. Good Bye :) ***************");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("\nYou entered an incorrect value! Please try again.");
                        break;
                }
            }
        }

        private void AddCar()
        {
            Car car = new Car();
            _brandManager.GetList(_brandManager.GetAll().Data);
            int brandId = 0;
            bool flag4 = true;
            while (flag4)
            {
                Console.WriteLine("Please select an Brand ID from the list: ");
                brandId = Convert.ToInt32(Console.ReadLine());
                if (_brandManager.GetAll().Data.Any(x => x.ID == brandId))
                {
                    flag4 = false;
                }
                else
                {
                    Console.WriteLine(Messages.NotExist + "brand. Please select an Brand ID from the list:");
                }
            }
            car.BrandID = brandId;

            _colorManager.GetList(_colorManager.GetAll().Data);
            int colorId = 0;
            bool flag5 = true;
            while (flag5)
            {
                Console.WriteLine("Please select an Color ID from the list: ");
                colorId = Convert.ToInt32(Console.ReadLine());
                if (_colorManager.GetAll().Data.Any(x => x.ID == colorId))
                {
                    flag5 = false;
                }
                else
                {
                    Console.WriteLine(Messages.NotExist + "color. Please select an Color ID from the list:");
                }
            }
            car.ColorID = colorId;

            var name = "";
            bool flag3 = true;
            while (flag3)
            {
                Console.WriteLine("Name: ");
                name = Console.ReadLine();
                if (name.Length < 2)
                {
                    Console.WriteLine(Messages.CarNameValidation);

                }
                else flag3 = false;
            }
            car.Name = name;
            Console.WriteLine("Model Year: ");
            car.ModelYear = Convert.ToInt32(Console.ReadLine());

            decimal price = 0;
            bool flag2 = true;
            while (flag2)
            {
                Console.WriteLine("Daily Price: ");
                price = Convert.ToDecimal(Console.ReadLine());
                if (price <= 0)
                {
                    Console.WriteLine(Messages.PriceValidation);

                }
                else flag2 = false;
            }
            car.DailyPrice = price;
            Console.WriteLine("Description: ");
            car.Description = Console.ReadLine();

            _carManager.Add(car);
            Console.WriteLine("Car" + Messages.AddSingular);
        }

        private void DeleteCar()
        {
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the car to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _carManager.GetAll().Data;
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine(Messages.NotExist + "car");

                }
                else flag2 = false;
            }
            _carManager.GetList(_carManager.GetAll().Data);
            _carManager.Delete(_carManager.FindByID(choice).Data);
            Console.WriteLine("Car" + Messages.DeleteSingular);
        }

        private void UpdateCar()
        {
            _carManager.GetList(_carManager.GetAll().Data);
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the car to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _carManager.GetAll().Data;
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine(Messages.NotExist + "car");

                }
                else flag2 = false;
            }
            var c = _carManager.FindByID(choice).Data;
            Console.WriteLine("Brand ID: ");
            c.BrandID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Color ID: ");
            c.ColorID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Name: ");
            c.Name = Console.ReadLine();
            Console.WriteLine("Model Year: ");
            c.ModelYear = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Daily Price: ");
            c.DailyPrice = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Description: ");
            c.Description = Console.ReadLine();

            _carManager.Update(c);
            Console.WriteLine("Car" + Messages.UpdateSingular);
        }

        private void CarsByBrandID()
        {
            _brandManager.GetList(_brandManager.GetAll().Data);
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the brand: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _brandManager.GetAll().Data;
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine(Messages.NotExist + "brand");

                }
                else flag2 = false;
            }
            _carManager.GetList(_carManager.GetCarsByBrandID(choice).Data);
        }

        private void CarsByColorID()
        {
            _colorManager.GetList(_colorManager.GetAll().Data);
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the color: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _colorManager.GetAll().Data;
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine(Messages.NotExist + "color");

                }
                else flag2 = false;
            }
            _carManager.GetList(_carManager.GetCarsByColorID(choice).Data);
        }

        private void GetCarList()
        {
            _carManager.GetList(_carManager.GetAll().Data);
        }


        private void ViewCarDetailList()
        {
            int counter = 1;
            foreach (var car in _carManager.GetCarDetails().Data)
            {
                Console.WriteLine("{0}- Car Name: {1}\n    Brand Name: {2}\n    Color Name: {3}", counter, car.CarName, car.BrandName, car.ColorName);
                counter++;
            }
        }

        #endregion

        #region BrandMenu

        private void BrandMenu()
        {
            while (flag)
            {
                Console.WriteLine("\n*************** Brand Menu ***************");
                Console.WriteLine("          1. Add new Brand");
                Console.WriteLine("          2. Delete a Brand");
                Console.WriteLine("          3. Update a Brand");
                Console.WriteLine("          4. View the list of all Brand");
                Console.WriteLine("          5. Go to Main Menu");
                Console.WriteLine("          6. Exit \n");

                char key = Console.ReadLine()[0];
                switch (key)
                {
                    case '1':
                        AddBrand();
                        break;
                    case '2':
                        DeleteBrand();
                        break;
                    case '3':
                        UpdateBrand();
                        break;
                    case '4':
                        GetBrandList();
                        break;
                    case '5':
                        Dashboard();
                        break;
                    case '6':
                        Console.WriteLine("*************** Have a nice day. Good Bye :) ***************");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("\nYou entered an incorrect value! Please try again.");
                        break;
                }
            }
        }

        private void AddBrand()
        {
            Brand brand = new Brand();
            Console.WriteLine("Name: ");
            brand.Name = Console.ReadLine();

            _brandManager.Add(brand);
            Console.WriteLine("Brand" + Messages.AddSingular);
        }

        private void DeleteBrand()
        {
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the brand to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _brandManager.GetAll().Data;
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine(Messages.NotExist + "brand");

                }
                else flag2 = false;
            }
            _brandManager.GetList(_brandManager.GetAll().Data);
            Console.WriteLine("Brand" + Messages.DeleteSingular);
        }

        private void UpdateBrand()
        {
            _brandManager.GetList(_brandManager.GetAll().Data);
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the brand to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _brandManager.GetAll().Data;
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine(Messages.NotExist + "brand");

                }
                else flag2 = false;
            }
            var brand = _brandManager.FindByID(choice).Data;

            Console.WriteLine("Name: ");
            brand.Name = Console.ReadLine();

            _brandManager.Update(brand);
            Console.WriteLine("Brand" + Messages.UpdateSingular);
        }

        private void GetBrandList()
        {
            _brandManager.GetList(_brandManager.GetAll().Data);
        } 
        #endregion

        #region ColorMenu
        private void ColorMenu()
        {
            while (flag)
            {
                Console.WriteLine("\n*************** Color Menu ***************");
                Console.WriteLine("          1. Add new Color");
                Console.WriteLine("          2. Delete a Color");
                Console.WriteLine("          3. Update a Color");
                Console.WriteLine("          4. View the list of all Color");
                Console.WriteLine("          5. Go to Main Menu");
                Console.WriteLine("          6. Exit \n");

                char key = Console.ReadLine()[0];
                switch (key)
                {
                    case '1':
                        AddColor();
                        break;
                    case '2':
                        DeleteColor();
                        break;
                    case '3':
                        UpdateColor();
                        break;
                    case '4':
                        GetColorList();
                        break;
                    case '5':
                        Dashboard();
                        break;
                    case '6':
                        Console.WriteLine("*************** Have a nice day. Good Bye :) ***************");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("\nYou entered an incorrect value! Please try again.");
                        break;
                }
            }
        }

        private void AddColor()
        {
            Color color = new Color();
            Console.WriteLine("Name: ");
            color.Name = Console.ReadLine();

            _colorManager.Add(color);
            Console.WriteLine("Color" + Messages.AddSingular);
        }

        private void DeleteColor()
        {
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the color to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _colorManager.GetAll().Data;
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine(Messages.NotExist + "color");

                }
                else flag2 = false;
            }
            _colorManager.GetList(_colorManager.GetAll().Data);
            _colorManager.Delete(_colorManager.FindByID(choice).Data);
            Console.WriteLine("Color" + Messages.DeleteSingular);
        }

        private void UpdateColor()
        {
            _colorManager.GetList(_colorManager.GetAll().Data);
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the color to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _colorManager.GetAll().Data;
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine(Messages.NotExist + "color");

                }
                else flag2 = false;
            }
            var color = _colorManager.FindByID(choice).Data;

            Console.WriteLine("Name: ");
            color.Name = Console.ReadLine();

            _colorManager.Update(color);
            Console.WriteLine("Color" + Messages.UpdateSingular);
        }

        private void GetColorList()
        {
            _colorManager.GetList(_colorManager.GetAll().Data);
        }  
        #endregion


        #region UserMenu
        private void UserMenu()
        {
            while (flag)
            {
                Console.WriteLine("\n*************** User Menu ***************");
                Console.WriteLine("          1. Add new User");
                Console.WriteLine("          2. Delete a User");
                Console.WriteLine("          3. Update a User");
                Console.WriteLine("          4. View the list of all User");
                Console.WriteLine("          5. Go to Main Menu");
                Console.WriteLine("          6. Exit \n");

                char key = Console.ReadLine()[0];
                switch (key)
                {
                    case '1':
                        AddUser();
                        break;
                    case '2':
                        DeleteUser();
                        break;
                    case '3':
                        UpdateUser();
                        break;
                    case '4':
                        GetUserList();
                        break;
                    case '5':
                        Dashboard();
                        break;
                    case '6':
                        Console.WriteLine("*************** Have a nice day. Good Bye :) ***************");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("\nYou entered an incorrect value! Please try again.");
                        break;
                }
            }
        }

        private void AddUser()
        {
            User user = new User();
            Console.WriteLine("First Name: ");
            user.FirstName = Console.ReadLine();
            Console.WriteLine("Last Name: ");
            user.LastName = Console.ReadLine();

            bool flag2 = true;
            string email = " ";
            while (flag2)
            {
                Console.WriteLine("Email: ");
                email = Console.ReadLine();
                var list = _userManager.GetAll().Data;
                if (list.Any(x => x.Email == email))
                {
                    Console.WriteLine("This email address" + Messages.AlreadyExist);

                }
                else flag2 = false;
            }
            user.Email = email;

            Console.WriteLine("Password: ");
            user.Password = Console.ReadLine();

            _userManager.Add(user);
            Console.WriteLine("User" + Messages.AddSingular);
        }

        private void DeleteUser()
        {
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the user to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _userManager.GetAll().Data;
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine(Messages.NotExist + "user");

                }
                else flag2 = false;
            }
            _userManager.GetList(_userManager.GetAll().Data);
            _userManager.Delete(_userManager.FindByID(choice).Data);
            Console.WriteLine("User" + Messages.DeleteSingular);
        }

        private void UpdateUser()
        {
            _userManager.GetList(_userManager.GetAll().Data);
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the user to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _userManager.GetAll().Data;
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine(Messages.NotExist + "user");

                }
                else flag2 = false;
            }
            var user = _userManager.FindByID(choice).Data;

            Console.WriteLine("First Name: ");
            user.FirstName = Console.ReadLine();
            Console.WriteLine("Last Name: ");
            user.LastName = Console.ReadLine();

            bool flag3 = true;
            string email = " ";
            while (flag3)
            {
                Console.WriteLine("Email: ");
                email = Console.ReadLine();
                var list = _userManager.GetAll().Data;
                if (!list.Any(x => x.Email == email))
                {
                    Console.WriteLine("This email address" + Messages.AlreadyExist);

                }
                else flag3 = false;
            }
            user.Email = email;

            Console.WriteLine("Password: ");
            user.Password = Console.ReadLine();

            _userManager.Update(user);
            Console.WriteLine("Color" + Messages.UpdateSingular);
        }

        private void GetUserList()
        {
            _userManager.GetList(_userManager.GetAll().Data);
        }
        
        #endregion
        private void CustomerMenu()
        {
            while (flag)
            {
                Console.WriteLine("\n*************** Customer Menu ***************");
                Console.WriteLine("          1. Add new Customer");
                Console.WriteLine("          2. Delete a Customer");
                Console.WriteLine("          3. Update a Customer");
                Console.WriteLine("          4. View the list of all Customer");
                Console.WriteLine("          5. Go to Main Menu");
                Console.WriteLine("          6. Exit \n");

                char key = Console.ReadLine()[0];
                switch (key)
                {
                    case '1':
                        AddCustomer();
                        break;
                    case '2':
                        DeleteCustomer();
                        break;
                    case '3':
                        UpdateCustomer();
                        break;
                    case '4':
                        GetCustomerList();
                        break;
                    case '5':
                        Dashboard();
                        break;
                    case '6':
                        Console.WriteLine("*************** Have a nice day. Good Bye :) ***************");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("\nYou entered an incorrect value! Please try again.");
                        break;
                }
            }
        }
        private void AddCustomer()
        {
            _userManager.GetList(_userManager.GetAll().Data);
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the user to add as customer: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _userManager.GetAll().Data;
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine(Messages.NotExist + "user");
                }
                else flag2 = false;
            }
            Customer c = new Customer();
            c.UserID = choice;
            Console.WriteLine("Company Name: ");
            c.CompanyName = Console.ReadLine();
            _customerManager.Add(c);
            Console.WriteLine("Customer" + Messages.AddSingular);
        }

        private void DeleteCustomer()
        {
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the customer to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _customerManager.GetAll().Data;
                if (!list.Any(x => x.UserID == choice))
                {
                    Console.WriteLine(Messages.NotExist + "customer");

                }
                else flag2 = false;
            }
            _customerManager.GetList(_customerManager.GetAll().Data);
            _customerManager.Delete(_customerManager.FindByID(choice).Data);
            Console.WriteLine("Customer" + Messages.DeleteSingular);
        }

        private void UpdateCustomer()
        {
            _customerManager.GetList(_customerManager.GetAll().Data);
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the customer to update: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _customerManager.GetAll().Data;
                if (!list.Any(x => x.UserID == choice))
                {
                    Console.WriteLine(Messages.NotExist + "customer");

                }
                else flag2 = false;
            }
            var user = _userManager.FindByID(choice).Data;
            Console.WriteLine("First Name: ");
            user.FirstName = Console.ReadLine();
            Console.WriteLine("Last Name: ");
            user.LastName = Console.ReadLine();
            bool flag3 = true;
            string email = " ";
            while (flag3)
            {
                Console.WriteLine("Email: ");
                email = Console.ReadLine();
                var list = _userManager.GetAll().Data;
                if (!list.Any(x => x.Email == email))
                {
                    Console.WriteLine("This email address" + Messages.AlreadyExist);

                }
                else flag3 = false;
            }
            user.Email = email;

            Console.WriteLine("Password: ");
            user.Password = Console.ReadLine();

            _userManager.Update(user);
            var customer = _customerManager.FindByID(choice).Data;
            Console.WriteLine("Company Name: ");
            customer.CompanyName = Console.ReadLine();
            _customerManager.Update(customer);
            Console.WriteLine("Customer" + Messages.UpdateSingular);
        }

        private void GetCustomerList()
        {
            int counter = 1;
            foreach (var customer in _customerManager.GetCustomerDetails().Data)
            {
                Console.WriteLine("{0}- First Name: {1}\n    Last Name: {2}\n    Email: {3}\n    Company Name: {4}\n", counter, customer.FirstName, customer.LastName, customer.Email, customer.CompanyName);
                counter++;
            }
        }

        #region RentACarMenu
        private void RentACarMenu()
        {
            while (flag)
            {
                Console.WriteLine("\n*************** Rental Menu ***************");
                Console.WriteLine("          1. Rent a Car");
                Console.WriteLine("          2. Return a Car");
                Console.WriteLine("          3. View the Rental Detail List");
                Console.WriteLine("          4. List of cars available for rent");
                Console.WriteLine("          5. Go to Main Menu");
                Console.WriteLine("          6. Exit \n");

                char key = Console.ReadLine()[0];
                switch (key)
                {
                    case '1':
                        AddRental();
                        break;
                    case '2':
                        UpdateRental();
                        break;
                    case '3':
                        GetRentalDetail();
                        break;
                    case '4':
                        CarListForRent();
                        break;
                    case '5':
                        Dashboard();
                        break;
                    case '6':
                        Console.WriteLine("*************** Have a nice day. Good Bye :) ***************");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("\nYou entered an incorrect value! Please try again.");
                        break;
                }
            }
        }

        private void AddRental()
        {
            bool flag2 = true;
            string email = " ";
            while (flag2)
            {
                Console.WriteLine("Please enter your registered email address: ");
                email = Console.ReadLine();
                var list = _userManager.GetAll().Data;
                if (!list.Any(x => x.Email == email))
                {
                    Console.WriteLine(Messages.NotExist + "email address.");
                }
                else flag2 = false;
            }

            bool flag3 = true;
            string password = " ";
            while (flag3)
            {
                Console.WriteLine("Please enter your password: ");
                password = Console.ReadLine();
                var selectUser = _userManager.GetAll().Data.Where(x => x.Email == email);
                if (!selectUser.Any(x => x.Password == password))
                {
                    Console.WriteLine(Messages.NotExist + "user.");
                }
                else flag3 = false;
            }
            Rental rent = new Rental();
            _carManager.GetList(_carManager.GetAll().Data.Where(x => x.IsRented == false).ToList());
            Console.WriteLine("Please select the CarID you want to rent: ");
            rent.CarID = Convert.ToInt32(Console.ReadLine());
            var car = _carManager.FindByID(rent.CarID).Data;
            car.IsRented = true;
            _carManager.Update(car);
            rent.CustomerID = _userManager.GetAll().Data.Where(x => x.Email == email && x.Password == password).Select(y => y.ID).FirstOrDefault();
            Console.WriteLine("Please enter the rent date: ");
            rent.RentDate = DateTime.Parse(Console.ReadLine());

            _rentalManager.Add(rent);
            Console.WriteLine(Messages.RentSuccess);
        }

        private void UpdateRental()
        {
            _rentalManager.GetList(_rentalManager.GetAll().Data);

            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the ID of your rental transaction record: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _rentalManager.GetAll().Data;
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine(Messages.NotExist + "rental transaction");

                }
                else flag2 = false;
            }
            var rent = _rentalManager.FindByID(choice).Data;
            bool flag3 = true;
            string email = " ";
            var customer = new User();
            while (flag3)
            {
                Console.WriteLine("Please enter your registered email address: ");
                email = Console.ReadLine();
                customer = _userManager.FindByID(rent.CustomerID).Data;
                if (customer.Email != email)
                {
                    Console.WriteLine(Messages.NotExist + "email address.");
                }
                else flag3 = false;
            }
            bool flag4 = true;
            string password = " ";
            while (flag4)
            {
                Console.WriteLine("Please enter your password: ");
                password = Console.ReadLine();
                if (customer.Password != password)
                {
                    Console.WriteLine(Messages.NotExist + "user.");
                }
                else flag4 = false;
            }
            var car = _carManager.FindByID(rent.CarID).Data;
            car.IsRented = false;
            _carManager.Update(car);
            Console.WriteLine("Please enter the rent date: ");
            rent.ReturnDate = DateTime.Parse(Console.ReadLine());
            _rentalManager.Update(rent);
            Console.WriteLine(Messages.ReturnSuccess);
        }

        private void CarListForRent()
        {
            _carManager.GetList(_carManager.GetAll().Data.Where(x => x.IsRented == false).ToList());
            
        }

        private void GetRentalDetail()
        {
            int counter = 1;
            foreach (var rent in _rentalManager.GetRentalDetails().Data)
            {
                Console.WriteLine("{0}- Car Name: {1}\n    Customer Name: {2}\n    Rent Date: {3}\n    Return Date: {4}\n", rent.RentalID, rent.CarName, rent.CustomerName, rent.RentDate, rent.ReturnDate);
                counter++;
            }
        }

        #endregion
    }
}
