using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData_II {
    internal class X_YAML : Export {

        public override string Extension(MyData database) => "yaml";

        override public string XRecord(MyData MyDataBase, string recname = "", bool addreturn = false) {
            var ret = "";
            if (addreturn) ret += Header(MyDataBase, "#");
            foreach (string k in MyDataBase.Records[recname].Keys) {
                var val = MyDataBase[recname, k];
                if (!(MyDataBase.Sys_RemoveNonExistent && (val == "" || (MyDataBase.Fields[k].LType == "bool" && val.ToUpper() != "TRUE")))) {
                    if (!addreturn) ret += "\t";
                    switch (MyDataBase.Fields[k].LType) {
                        case "date":
                            var ds = val.Split('/');
                            ret += $"{k} : {eol}";
                            if (!addreturn) ret += "\t"; ret += $"\tday : {ds[0]}{eol}";
                            if (!addreturn) ret += "\t"; ret += $"\tmonth : {ds[1]}{eol}";
                            if (!addreturn) ret += "\t"; ret += $"\tyear : {ds[2]}{eol}";
                            break;
                        case "time":
                            ds = val.Split(':');
                            ret += $"{k} : {eol}";
                            if (!addreturn) ret += "\t"; ret += $"\thour : {ds[0]}{eol}";
                            if (!addreturn) ret += "\t"; ret += $"\tminute : {ds[1]}{eol}";
                            if (!addreturn) ret += "\t"; ret += $"\tsecond : {ds[2]}{eol}";
                            break;
                        case "color":
                            ds = val.Split(',');
                            ret += $"{k} : {eol}";
                            if (!addreturn) ret += "\t"; ret += $"\tred : {ds[0]}{eol}";
                            if (!addreturn) ret += "\t"; ret += $"\tgreen : {ds[1]}{eol}";
                            if (!addreturn) ret += "\t"; ret += $"\tblue : {ds[2]}{eol}";
                            break;
                        default:
                            ret += $"{k} : {val}{eol}";
                            break;
                    }
                }
            }
            return ret;
        }

        override public string XBase(MyData MyDataBase) {
            var ret = Header(MyDataBase, "#");
            foreach (string rID in MyDataBase.Records.Keys) {
                ret += $"{rID} :{eol}{XRecord(MyDataBase, rID, false)}";
            }
            return ret;
        }
    }
}

