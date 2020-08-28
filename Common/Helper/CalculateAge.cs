using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class CalculateAge
    {
        /// <summary>  
        /// For calculating only age  
        /// </summary>  
        /// <param name="dateOfBirth">Date of birth</param>  
        /// <returns> age e.g. 26</returns>  
        public static int GetAge(DateTime dateOfBirth)
        {

            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            {
                age = age - 1;
            }

            return age;
        }
    }
}
