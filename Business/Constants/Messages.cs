using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string AddSingular = " has been added.";
        public static string UpdateSingular = " has been updated.";
        public static string DeleteSingular = " has been deleted.";
        public static string CarNameValidation = "The value entered must be at least 2 characters.";
        public static string PriceValidation = "Please enter a value greater than 0.";
        public static string NotExist = "There is no such a ";
        public static string AlreadyExist = " is already exists.";
        public static string RentSuccess = "Car rental has been successfully completed.";
        public static string ReturnSuccess = "Car delivery has been successfully completed.";
        public static string InvalidName = "Name must contain at least two characters.";
        public static string InvalidPrice = "Daily Price must be greater than 0.";
        public static string InvalidFileExtension = "Invalid file extension.";
        public static string ImageNumberLimitExceeded = "The image limit for this car is full and new images cannot be added.";
    }
}
