// Lic:
// MyData II
// JSON exporter
// 
// 
// 
// (c) Jeroen P. Broks, 2023
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
using TrickyUnits;

namespace MyData_II {
    internal class X_JSON : Export {

        public X_JSON() {
            MKL.Version("MyData II - X_JSON.cs","23.08.19");
            MKL.Lic    ("MyData II - X_JSON.cs","GNU General Public License 3");
        }

        public override string XRecord(MyData MyDataBase, string recname = "", bool addreturn = false) {
            var t = ""; if (!addreturn) t = "\t";
            var ret = t + "{" + eol;
            var first = true;
            foreach (string k in MyDataBase.Record(recname).Keys) {
                var val = MyDataBase[recname, k];
                var ok = !(MyDataBase.Sys_RemoveNonExistent && (val == "" || (MyDataBase.Fields[k].Type.ToString().ToLower() == "bool" && val.ToUpper() != "TRUE")));
                var lin = $"\t{t}\"{k}\" : ";
                if (!ok) { lin += "null"; } else {
                    switch (MyDataBase.Fields[k].Type.ToString().ToLower()) { // Lazy! 
                        case "string":
                        case "mc":
                            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(MyDataBase[recname, k]);
                            lin += "\"";
                            foreach (byte b in bytes) {
                                if ((b > 31 && b < 128) && b != '"') { lin += qstr.Chr(b); } else {
                                    lin += "\\" + qstr.Right("00" + Convert.ToString(b, 8), 3);
                                }
                            }
                            lin += "\"";
                            break;
                        case "int":
                        case "double":
                            lin += MyDataBase[recname, k];
                            break;
                        case "bool":
                            if (MyDataBase[recname, k].ToUpper() == "TRUE") lin += "true"; else lin += "false";
                            break;
                        case "date":
                            var sd = MyDataBase[recname, k].Split('/');
                            lin += "{" + $" \"day\" : {sd[0]}, \"month\" : {sd[1]}, \"year\" : {sd[2]} " + "}";
                            break;
                        case "time":
                            var si = TimeSplit(MyDataBase[recname, k]);
                            lin += "{" + $" \"hour\" : {si[0]}, \"minute\" : {si[1]}, \"second\" : {si[2]} " + "}";
                            break;
                        case "color":
                            si = TimeSplit(MyDataBase[recname, k], ',');
                            lin += "{" + $" \"red\" : {si[0]}, \"green\" : {si[1]}, \"blue\" : {si[2]} " + "}";
                            break;
                        default:
                            Error.Err($"I cannot handle type -> {MyDataBase.Fields[k].Type}");
                            break;
                    }
                }
                if (ok || (!MyDataBase.Sys.ContainsKey("NONULL") || MyDataBase.Sys["NONULL"].ToUpper() != "TRUE")) {
                    if (!first) ret += $",{eol}"; first = false;
                    ret += lin;
                }

            }
            ret += "}" + eol;
            return ret;
        }

        public override string XBase(MyData MyDataBase) {
            var ret = "{" + eol;
            var first = true;
            foreach (string rID in MyDataBase.Records.Keys) {
                if (!first) ret += $",{eol}"; first = false;
                ret += $"\t\"{rID}\" : {XRecord(MyDataBase, rID, false)}";
            }
            ret += "}" + eol + eol;
            return ret;

        }

        public override string Extension(MyData database) => ".json";
    }

}