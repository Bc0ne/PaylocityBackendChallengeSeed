namespace Api.Helpers
{
    public class AgeHelper
    {
        public static int CalculateAge(DateTime birthday)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;

            if (today < birthday.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}
