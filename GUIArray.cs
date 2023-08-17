// Lic:
// MyData II
// GUI Array
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
// Version: 23.08.17
// EndLic
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyData_II {
    internal class GUIArray {
        public const int max = 15;
        static public int mmax => max + 1;

        internal static readonly Grid[] grids = new Grid[mmax];
        internal static readonly TextBox[] tb = new TextBox[mmax];
        internal static readonly TextBox[] tbi = new TextBox[mmax];
        internal static readonly ComboBox[] cb = new ComboBox[mmax];
        internal static readonly CheckBox[] chk = new CheckBox[mmax];

        static public readonly Label[] ltype = new Label[mmax];
        static public readonly Label[] lname = new Label[mmax];
        static public readonly Label[] linfo = new Label[mmax];
        static public readonly Label[] lstrike = new Label[mmax];

        static public readonly Dictionary<TextBox,int> itb = new Dictionary<TextBox,int>();
        static public readonly Dictionary<TextBox, int> itbi = new Dictionary<TextBox, int>();
        static public readonly Dictionary<ComboBox,int> icb = new Dictionary<ComboBox,int>();
        static public readonly Dictionary<CheckBox,int> ichk = new Dictionary<CheckBox,int>();

        public static void Reg(int idx,TextBox _tb) { tb[idx] = _tb; itb[_tb] = idx; }
        public static void RegI(int idx, TextBox _tb) { tbi[idx] = _tb; itbi[_tb] = idx; }
        public static void Reg(int idx,ComboBox _tb) { cb[idx] = _tb; icb[_tb] = idx; }
        public static void Reg(int idx,CheckBox _tb) { chk[idx] = _tb; ichk[_tb] = idx; }

        
    }
}