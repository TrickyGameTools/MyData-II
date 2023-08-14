// Lic:
// MyData II
// Error handler
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrickyUnits;

namespace MyData_II {

    static internal class Error {
        static private void TCrash(string message,string _error="Error!",bool fatal=false) {
            Confirm.Annoy(message, _error, System.Windows.Forms.MessageBoxIcon.Error);
            if (fatal) Environment.Exit(1);
        }

        static internal void Crash(string message) => TCrash(message, "Fatal Error", true);
        static internal void Crash(Exception ex) => Crash($".NET threw an exception!\n\n{ex.Message}");

        static internal void Err(string message) => TCrash(message);
        static internal void Err(Exception ex) => TCrash($".NET threw a non-fatal exception\n\n{ex.Message}");
    }
}