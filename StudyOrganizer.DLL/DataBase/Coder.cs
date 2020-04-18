using System.Text;

namespace StudyOrganizer.DLL.DataBase
{
    public static class Coder
    {
        public static string CodeString(string toCode)
        {
            StringBuilder builder = new StringBuilder();
            char x;
            foreach (var c in toCode.ToCharArray())
            {
                x = (char) (c + 69);                
                builder.Append(x);
            }

            return builder.ToString();
        }
    }
}