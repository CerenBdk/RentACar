using Business.Concrete;
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

        public ConsoleManager()
        {
            _carManager = new CarManager(new EfCarDal());
            _colorManager = new ColorManager(new EfColorDal());
            _brandManager = new BrandManager(new EfBrandDal());
        }

        public void Dashboard()
        {
            while (flag)
            {
                Console.WriteLine("\n*************** Main Menu ***************");
                Console.WriteLine("          1. Car Menu");
                Console.WriteLine("          2. Brand Menu");
                Console.WriteLine("          3. Color Menu");
                Console.WriteLine("          4. View Car List By Daily Price");
                Console.WriteLine("          5. Exit \n");

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
                        ViewCarListByDailyPrice();
                        break;
                    case '5':
                        Console.WriteLine("*************** Have a nice day. Good Bye :) ***************");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("\nYou entered an incorrect value! Please try again.");
                        break;
                }
            }
        }

        private void ViewCarListByDailyPrice()
        {
            bool flag2 = true;
            decimal priceMax = 0;
            Console.WriteLine("Daily Price (Minimum): ");
            decimal priceMin = Convert.ToDecimal(Console.ReadLine());

            while (flag2)
            {
                Console.WriteLine("Daily Price (Maximum): ");
                priceMax = Convert.ToDecimal(Console.ReadLine());
                if (priceMin >= priceMax)
                {
                    Console.WriteLine("Please enter a value greater than the minimum price");

                }
                else flag2 = false;
            }
            _carManager.GetList(_carManager.GetByDailyPrice(priceMin, priceMax));
        }

        #region CarMenu
        public void CarMenu()
        {
            while (flag)
            {
                Console.WriteLine("\n*************** Main Menu ***************");
                Console.WriteLine("          1. Add new Car");
                Console.WriteLine("          2. Delete a Car");
                Console.WriteLine("          3. Update a Car");
                Console.WriteLine("          4. View Cars by Brand ID");
                Console.WriteLine("          5. View Cars by Color ID");
                Console.WriteLine("          6. View the list of all Car");
                Console.WriteLine("          7. Go to Main Menu");
                Console.WriteLine("          8. Exit \n");

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
                        Dashboard();
                        break;
                    case '8':
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
            Console.WriteLine("Brand ID: ");
            car.BrandID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Color ID: ");
            car.ColorID = Convert.ToInt32(Console.ReadLine());
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
                    Console.WriteLine("Please enter a value greater than 0.");

                }
                else flag2 = false;
            }
            car.DailyPrice = price;

            var description = "";
            bool flag3 = true;
            while (flag3)
            {
                Console.WriteLine("Description: ");
                description = Console.ReadLine();
                if (description.Length < 2)
                {
                    Console.WriteLine("Please enter at least 2 characters.");

                }
                else flag3 = false;
            }

            car.Description = description;

            _carManager.Add(car);
            Console.WriteLine("{0} has been registered.\n", car.ID);
        }

        private void DeleteCar()
        {
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the car to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _carManager.GetAll();
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine("There are no cars registered with this ID.");

                }
                else flag2 = false;
            }
            _carManager.GetList(_carManager.GetAll());
            _carManager.Delete(_carManager.FindByID(choice));
            Console.WriteLine("{0} has been deleted.\n", choice);
        }

        private void UpdateCar()
        {
            _carManager.GetList(_carManager.GetAll());
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the car to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _carManager.GetAll();
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine("There are no cars registered with this ID.");

                }
                else flag2 = false;
            }
            var c = _carManager.FindByID(choice);
            Console.WriteLine("Brand ID: ");
            c.BrandID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Color ID: ");
            c.ColorID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Model Year: ");
            c.ModelYear = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Daily Price: ");
            c.DailyPrice = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Description: ");
            c.Description = Console.ReadLine();

            _carManager.Update(c);
            Console.WriteLine("{0} has been updated.\n", c.ID);
        }

        private void CarsByBrandID()
        {
            _brandManager.GetList(_brandManager.GetAll());
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the brand: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _brandManager.GetAll();
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine("There are no brand registered with this ID.");

                }
                else flag2 = false;
            }
            _carManager.GetList(_carManager.GetCarsByBrandID(choice));
        }

        private void CarsByColorID()
        {
            _colorManager.GetList(_colorManager.GetAll());
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the color: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _colorManager.GetAll();
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine("There are no color registered with this ID.");

                }
                else flag2 = false;
            }
            _carManager.GetList(_carManager.GetCarsByColorID(choice));
        }

        private void GetCarList()
        {
            _carManager.GetList(_carManager.GetAll());
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
            Console.WriteLine("{0} has been registered.\n", brand.ID);
        }

        private void DeleteBrand()
        {
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the brand to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _brandManager.GetAll();
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine("There are no brand registered with this ID.");

                }
                else flag2 = false;
            }
            _brandManager.GetList(_brandManager.GetAll());
            _brandManager.Delete(_brandManager.FindByID(choice));
            Console.WriteLine("{0} has been deleted.\n", choice);
        }

        private void UpdateBrand()
        {
            _brandManager.GetList(_brandManager.GetAll());
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the brand to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _brandManager.GetAll();
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine("There are no brand registered with this ID.");

                }
                else flag2 = false;
            }
            var brand = _brandManager.FindByID(choice);

            Console.WriteLine("Name: ");
            brand.Name = Console.ReadLine();

            _brandManager.Update(brand);
            Console.WriteLine("{0} has been updated.\n", brand.ID);
        }

        private void GetBrandList()
        {
            _brandManager.GetList(_brandManager.GetAll());
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
            Console.WriteLine("{0} has been registered.\n", color.ID);
        }

        private void DeleteColor()
        {
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the color to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _colorManager.GetAll();
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine("There are no color registered with this ID.");

                }
                else flag2 = false;
            }
            _colorManager.GetList(_colorManager.GetAll());
            _colorManager.Delete(_colorManager.FindByID(choice));
            Console.WriteLine("{0} has been deleted.\n", choice);
        }

        private void UpdateColor()
        {
            _colorManager.GetList(_colorManager.GetAll());
            bool flag2 = true;
            int choice = 0;
            while (flag2)
            {
                Console.WriteLine("Please select the color to delete: ");
                choice = Convert.ToInt32(Console.ReadLine());
                var list = _colorManager.GetAll();
                if (!list.Any(x => x.ID == choice))
                {
                    Console.WriteLine("There are no color registered with this ID.");

                }
                else flag2 = false;
            }
            var color = _colorManager.FindByID(choice);

            Console.WriteLine("Name: ");
            color.Name = Console.ReadLine();

            _colorManager.Update(color);
            Console.WriteLine("{0} has been updated.\n", color.ID);
        }

        private void GetColorList()
        {
            _colorManager.GetList(_colorManager.GetAll());
        }  
        #endregion
    }
}
