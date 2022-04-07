using System;
using System.Text;

namespace CustomConsole
{
    public delegate void FunctionPasser(object[] objects, string info);

    public class Function
    {
        public Function(string name, StringConverter[] paramConverters, FunctionPasser callback)
        {
            Name = name;

            if (paramConverters == null)
            {
                ParamConverters = Array.Empty<StringConverter>();
            }
            else
            {
                ParamConverters = paramConverters;
            }
            
            Callback = callback;
        }

        public string Name { get; }

        public byte ParamCount => (byte)ParamConverters.Length;
        public StringConverter[] ParamConverters { get; }

        public FunctionPasser Callback { get; set; }

        public object[] GetParams(string funcParams, out int endIndex)
        {
            bool inQuotes = false;
            endIndex = -1;

            object[] objects = new object[ParamCount];
            StringBuilder param = new StringBuilder();

            funcParams = funcParams.Trim();
            if (funcParams[0] != '(')
            {
                VirtualConsole.Log("Invalid syntax");
                return null;
            }

            int paramCounter = 0;

            for (int i = 1; i < funcParams.Length; i++)
            {
                char c = funcParams[i];

                if (c == '\n' || c == '\r')
                {
                    VirtualConsole.Log("Invlaid new line character");
                    return null;
                }

                if (c == ')' && !inQuotes)
                {
                    endIndex = i + 1;
                    break;
                }
                if (c == ',' && !inQuotes)
                {
                    if (paramCounter >= ParamCount)
                    {
                        VirtualConsole.Log($"Function requires {ParamCount} parameters");
                        return null;
                    }

                    object obj = ParamConverters[paramCounter](param.ToString().Trim());

                    // Invalid parameter
                    if (obj == null) { return null; }

                    objects[paramCounter] = obj;
                    paramCounter++;

                    param.Clear();
                    continue;
                }

                if (c == '\"' && !inQuotes) { inQuotes = true; }
                else if (c == '\"' && inQuotes) { inQuotes = false; }

                param.Append(c);
                continue;
            }

            // last parameter
            if (ParamCount != 0)
            {
                if (param.Length == 0)
                {
                    VirtualConsole.Log($"Invalid syntax");
                    return null;
                }

                if (paramCounter >= ParamCount)
                {
                    VirtualConsole.Log($"Function requires {ParamCount} parameters");
                    return null;
                }

                object obj = ParamConverters[paramCounter](param.ToString().Trim());

                // Invalid parameter
                if (obj == null) { return null; }

                objects[paramCounter] = obj;
                paramCounter++;
            }

            if (ParamCount != paramCounter)
            {
                VirtualConsole.Log($"Function requires {ParamCount} parameters");
                return null;
            }

            return objects;
        }
    }
}
