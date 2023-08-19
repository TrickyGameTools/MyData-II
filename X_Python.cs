// Lic:
// MyData II
// Python export
// 
// 
// 
// (c) Jeroen P. Broks, 2018
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
using TrickyUnits;

namespace MyData_II {
    internal class X_Python:Export {
        public override string Extension(MyData database) => "py";

        public override string XRecord(MyData MyDataBase, string recname = "", bool addreturn = false) {
            var header = Header(MyDataBase, "#");
            var vr = "";
            var t = ""; if (!addreturn) { t = "\t"; } else { vr = $"{header}{eol}{eol}MyRec = "; }
            var ret = t + vr + "{" + eol;
            var first = true;
            foreach (string k in MyDataBase.Record(recname).Keys) {
                var val = MyDataBase[recname,k];
                var ok = !(MyDataBase.Sys_RemoveNonExistent && (val == "" || (MyDataBase.Fields[k].LType == "bool" && val.ToUpper() != "TRUE")));
                var lin = $"\t{t}\"{k}\" : ";
                if (!ok) { lin += "null"; } else {
                    switch (MyDataBase.Fields[k].LType) {
                        case "string":
                        case "mc":
                            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(MyDataBase[recname,k]);
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
                            lin += MyDataBase[recname,k];
                            break;
                        case "bool":
                            if (MyDataBase[recname,k].ToUpper() == "TRUE") lin += "True"; else lin += "False";
                            break;
                        case "date":
                            var sd = MyDataBase[recname,k].Split('/');
                            lin += "{" + $" \"day\" : {sd[0]}, \"month\" : {sd[1]}, \"year\" : {sd[2]} " + "}";
                            break;
                        case "time":
                            var si = TimeSplit(MyDataBase[recname,k]); //MyDataBase[recname,k].Split(':');
                            lin += "{" + $" \"hour\" : {si[0]}, \"minute\" : {si[1]}, \"second\" : {si[2]} " + "}";
                            break;
                        case "color":
                            si = TimeSplit(MyDataBase[recname,k], ',');
                            lin += "{" + $" \"red\" : {si[0]}, \"green\" : {si[1]}, \"blue\" : {si[2]} " + "}";
                            break;
                        default:
                            Error.Err("I cannot handle type -> " + MyDataBase.Fields[k].SType);
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
            var header = Header(MyDataBase, "#");
            var ret = header + eol + eol + "MyData = {" + eol;
            var first = true;
            foreach (string rID in MyDataBase.Records.Keys) {
                if (!first) ret += $",{eol}"; first = false;
                ret += $"\t\"{rID}\" : {XRecord(MyDataBase,rID, false)}";
            }
            ret += "}" + eol + eol;
            return ret;

        }
    }
}