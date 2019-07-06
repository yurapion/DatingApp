using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    // Our own Created Class to make error message readable for client side 
    public static class Extensions
    {
        //Create an extension method to add headers to our error response
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        //Create an Extension Method to calculate Age
        public static int CalculateAge (this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }
            return age;
        }
    }
}