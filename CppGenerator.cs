using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FlyLang
{
    public class CppGenerator
    {
        private const string SystemFunctions =
@"
void print(string str){
    cout << str;
}
void printLine(string str){
    cout << str << ""\n"";
}
string readLine(){
    string s;
    getline(cin, s);
    return s;
}
";
        private Dictionary<string, string> methodConversions = new Dictionary<string, string>()
        {
            {"str", "std::to_string"},
        };
        private StringBuilder builder;
        private bool lineOpen = false;
        public void Append(string s) => builder.Append(s);
        public CppGenerator()
        {
            builder = new StringBuilder();
            builder.Append(@"
            #include<iostream> 
            #include<string>
            using namespace std;
            ");
            builder.Append(SystemFunctions);
            builder.Append("int main(){");
        }
        public string Generate()
        {
            builder.Append("return 0;}");
            return builder.ToString();
        }
        public void GenerateToFile(string output)
        {
            File.WriteAllText(output, Generate());
        }
        public void AddMethodCall(string name, params object[] args)
        {
            if (methodConversions.ContainsKey(name))
                name = methodConversions[name];
            if (lineOpen)
            {
                builder.Append($"{name}({string.Join(",", args)})");
            }
            else
                builder.AppendLine($"{name}({string.Join(",", args)});");

        }
        public void AddVarAssignment(string name, object value = null)
        {
            string t = string.Empty;
            string content = value != null ? value.ToString() + ";" : "";
            builder.Append($@"{t} {name} = {content}");
        }
    }
}