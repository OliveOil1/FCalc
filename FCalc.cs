using System;
using PluginBase;
using System.Runtime.InteropServices;
using System.Data;

namespace FCalc
{
    public class FCalc : IPlugin
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        public string name => "FCalc";

        public string description => "An advanced calculator built into FLauncher";

        string[] prefixes = { "calc/", "="};

        public bool CommandEntered(string text_entered, string parameter)
        {
            bool inputHandled = false;
            DataTable dataTable = new DataTable();

            try
            {
                var answer = dataTable.Compute(text_entered, "");
                MessageBox((IntPtr)0, answer.ToString(), "FCalc", 0);
                inputHandled = true;
            }
            catch
            {
                foreach (string prefix in prefixes)
                {
                    if (text_entered.ToLower().StartsWith(prefix))
                    {
                        var expression = text_entered.Remove(0, prefix.Length);
                        try
                        {
                            var answer = dataTable.Compute(expression, "");
                            MessageBox((IntPtr)0, answer.ToString(), "FCalc", 0);
                        }
                        catch (Exception ex)
                        {
                            MessageBox((IntPtr)0, ex.Message, "FCalc Exception Encountered", 0);
                        }
                        inputHandled = true;
                        break;
                    }
                }
            }

            return inputHandled;
        }

        public int Init()
        {
            return 0;
        }
    }
}
