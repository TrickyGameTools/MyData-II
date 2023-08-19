// Lic:
// MyData II
// YAML export
// 
// 
// 
// (c) Jeroen P. Broks, 2018, 2023
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// Please note that some references to data like pictures or audio, do not automatically
// fall under this licenses. Mostly this is noted in the respective files.
// 
// Version: 23.08.19
// EndLic
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