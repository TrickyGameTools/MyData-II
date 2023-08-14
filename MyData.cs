// Lic:
// MyData II
// Actual database class
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
// Version: 23.08.14
// EndLic
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrickyUnits;

namespace MyData_II {
    internal class MyData {
        internal readonly string FileName;

        private MyData(string _FileName) {
            FileName = _FileName;
        }

        internal MyData Load(string _FileName) { 
            var RFN= Dirry.AD(_FileName.Replace("\\", "/"));
            try {
                if (!File.Exists(RFN)) { Error.Err($"Database file not found.\nGFN: {_FileName}\nRFN:{RFN}"); return null; }
                return new MyData(_FileName);
            } catch(Exception ex) {
                Error.Err($"Loading database failed due to .NET throwing an exception.\nGFN: {_FileName}\nRFN:{RFN}\nNEX:{ex.Message}");
                return null;
            }            
        }
    }
}