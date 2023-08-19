using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickyUnits;

namespace MyData_II {

    // GINIE is not INI either

    internal class X_GINIE:Export {
        string header => $"# GINIE File generated {DateTime.Now}";

        public override string Extension(MyData database) => "ini";

        void ClassIt(MyData MyDataBase, GINIE g, string classname = "class") {
            foreach (var n in MyDataBase.Fields.Keys) {
                var t = MyDataBase.Fields[n].LType;
                var nu = n.ToUpper();
                switch (t) {
                    case "info":
                    case "strike":
                        break; // Nothing I need here!
                    case "string":
                    case "mc":
                        g[classname, n] = "String";
                        break;
                    case "int":
                        g[classname, n] = "Int";
                        break;
                    case "bool":
                    case "boolean":
                        g[classname, n] = "Boolean";
                        break;
                    default:
                        Error.Err($"Unknown field type \"{n}\"! Field ignored and further exports will lead to errors in Neil!");
                        break;
                }
            }
        }

        void RecIt(MyData MyDataBase, GINIE g, string rec, MyDataRecord mr) {
            var v = "";
            foreach (var k in MyDataBase.Fields.Keys) {
                if (mr.ContainsKey(k)) v = mr[k];
                g[$"Rec:{rec}", k] = v;
            }
        }

        public override string XBase(MyData MyDataBase) {
            var output = GINIE.Empty();
            ClassIt(MyDataBase,output);
            foreach (var IT in MyDataBase.Records) RecIt(MyDataBase,output, IT.Key, IT.Value);
            return $"{header}\n\n{output.ToSource()}";
        }

        public override string XRecord(MyData MyDataBase,string recname = "", bool addreturn = false) {
            var output = GINIE.Empty();
            RecIt(MyDataBase, output, recname, MyDataBase.Record(recname));
            return $"{header}\n\n{output.ToSource()}";
        }

        public override string XClass(MyData MD,string cln) {
            var output = GINIE.Empty();
            ClassIt(MD,output);
            return $"{header}\n\n{output.ToSource()}";
        }
    }
}
