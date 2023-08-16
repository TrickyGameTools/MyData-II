// Lic:
// MyData II
// Pages
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
// Version: 23.08.16
// EndLic
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData_II {
	internal class MyDataPage {
		private string _Name = "";
		private uint _xnum = 0;
		private MyData _Parent = null;

		

		internal MyDataField[] Fields = new MyDataField[GUIArray.mmax];
		internal uint NumFields { get {
				for (uint i= 0;i<=GUIArray.max;i++) if (Fields[i] == null) return i;
				return GUIArray.max;

			} }

		internal string PageName {
			get {
				if (_xnum == 0) return _Name;
				return $"{_Name} ({_xnum})";
			}
		}

		internal MyDataPage(MyData Ouwe, string Name) {
			_Name = Name;
			_Parent = Ouwe;
			if (Ouwe == null) throw new Exception("Internal error! Page without parent");
			_Parent.Pages.Add(this);
		}

		private MyDataPage(MyDataPage Old) {
			_Name = Old._Name;
			_Parent = Old._Parent;
			if (Old._xnum == 0) { Old._xnum = 1; }
			_xnum = Old._xnum + 1;
			if (_Parent == null) throw new Exception("Internal error! Page without parent");
			_Parent.Pages.Add(this);
		}

		internal MyDataPage NewField(string _type,string _name,string _defaultvalue) {
			var ret = this;
			if (NumFields >= GUIArray.max) ret = new MyDataPage(this);
			var rnf = ret.NumFields;
			ret.Fields[rnf] = new MyDataField(ret, _type, _name, rnf, _defaultvalue);
			ret._Parent.Fields[_name] = ret.Fields[rnf];			
			return ret;
		}


	}
}