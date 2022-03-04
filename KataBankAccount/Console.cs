using System;
using System.Collections.Generic;
using System.Text;

namespace KataBankAccount
{
    public interface IConsole
    {
        void WriteLine(string chaine);
    }

    public class Console : IConsole
    {
        public void WriteLine(string chaine) => System.Console.WriteLine(chaine);
    }
}
