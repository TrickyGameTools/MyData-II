// Lic:
// MyData II
// Abstract Export
// 
// 
// 
// (c) Jeroen P. Broks, 2018, 2021, 2023
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
// Version: 23.08.18
// EndLic
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickyUnits;

namespace MyData_II {

    abstract class Export {
        public Export() {
            MKL.Version("MyData II - X_Base.cs","23.08.18");
            MKL.Lic    ("MyData II - X_Base.cs","GNU General Public License 3");
        }

        abstract public string XRecord(MyData database, string recname = "", bool addreturn = false);
        abstract public string XBase(MyData database);
        virtual public string XClass(MyData database, string cln) { return ""; }


        /*
        public string eol {
            get {
                if (!MyDataBase.sys.ContainsKey("EOL")) return "\n";
                switch (MyDataBase.sys["EOL"].ToUpper()) {
                    case "DOS":
                    case "WINDOWS":
                        // return "\r\n"; // Fuck Windows
                        return "\n";
                    case "UNIX":
                    case "LINUX":
                    case "MAC":
                    case "MACOSX":
                        return "\n";
                    default:
                        TrickyUnits.GTK.QuickGTK.Error($"I do not know EOL type {MyDataBase.sys[""]}, so reverting back to Unix format."EOL);
                        MyDataBase.sys["EOL"] = "UNIX"; // Won't alter the database files, but it will prevent the message to pop up over and over and over and over and over etc.
                        return "\n";
                }
            }
        }
        */
        public static int[] TimeSplit(string time, char sep = ':') {
            var s = time.Split(sep);
            int[] ret = { 0, 0, 0 };
            try {
                for (int i = 0; i < ret.Length; i++) ret[i] = Int32.Parse(s[i]);
            } catch {
                ret[0] = 0; // Just a warning suppressor :P
            }
            return ret;
        }
    }

}