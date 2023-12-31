// Lic:
// MyData II
// Lua Exporter
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
using TrickyUnits;

namespace MyData_II {
    class X_Lua : Export {
        public X_Lua() {
            MKL.Version("MyData II - X_Lua.cs","23.08.19");
            MKL.Lic    ("MyData II - X_Lua.cs","GNU General Public License 3");
        }
        string header(MyData MyDataBase) {
                var today = DateTime.Today;
                return $"-- File generated by MyData on {today.DayOfWeek.ToString()} {today.Day}/{today.Month}/{today.Year}{eol}-- License: {MyDataBase.Sys_License}{eol}{eol}";
        }

        public override string XRecord(MyData MyDataBase,string recname = "", bool addreturn = false) {
            var ret = "";
            if (addreturn) { ret = header(MyDataBase) + "local ret={" + eol; }
            foreach (string k in MyDataBase.Record(recname).Keys) {
                if (!addreturn) ret += "\t";
                ret += $"\t[\"{k}\"] = ";
                var v = MyDataBase[recname,k];
                if (!(MyDataBase.Sys_RemoveNonExistent && (v == "" || (MyDataBase.Fields[k].LType == "bool" && v.ToUpper() != "TRUE")))) {
                    switch (MyDataBase.Fields[k].LType) {
                        case "string":
                        case "mc":
                            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(MyDataBase[recname,k]);
                            ret += "\"";
                            foreach (byte b in bytes) {
                                if ((b > 31 && b < 128) && b != '"') { ret += qstr.Chr(b); } else {
                                    ret += "\\" + qstr.Right($"00{b}", 3);
                                }
                            }
                            ret += "\"";
                            break;
                        case "int":
                        case "double":
                            if (MyDataBase[recname,k].Trim() == "")
                                ret += "0";
                            else
                                ret += MyDataBase[recname,k];
                            break;
                        case "bool":
                            if (MyDataBase[recname,k].ToUpper() == "TRUE") ret += "true"; else ret += "false";
                            break;
                        case "date":
                            var ds = MyDataBase[recname,k].Split('/');
                            ret += "{" + $" day={ds[0]}, month={ds[1]}, year ={ds[2]} " + "}";
                            break;
                        case "time":
                            ds = MyDataBase[recname,k].Split(':');
                            ret += "{" + $" hour={ds[0]}, minute={ds[1]}, second ={ds[2]} " + "}";
                            break;
                        case "color":
                            ds = MyDataBase[recname,k].Split(',');
                            ret += "{" + $" red={ds[0]}, green={ds[1]}, blue ={ds[2]} " + "}";
                            break;
                        default:
                            Error.Err($"I do not know how to deal with type {MyDataBase.Fields[k].Type}");
                            break;

                    }
                    ret += "," + eol;
                }
            }
            if (addreturn) {
                ret += "}\n\nreturn ret\n\n";
            }
            return ret;
        }

        public override string XBase(MyData MyDataBase) {
            var ret = $"{header(MyDataBase)}{eol}local ret = " + "{" + eol;
            foreach (string recID in MyDataBase.Records.Keys) {
                ret += $"\t[\"{recID}\"] = " + "{" + $"{eol}{XRecord(MyDataBase,recID)}{eol}\t" + "}," + eol;
            }
            ret += $"{eol}" + "}" + $"{eol}{eol}return ret{eol}{eol}";
            return ret;
        }

        public override string Extension(MyData database) => "lua";
    }
}