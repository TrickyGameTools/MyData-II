// Lic:
// MyData II
// Record Manager
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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData_II {
	internal class MyDataRecord {
		readonly private MyData Parent = null;
		readonly private Dictionary<string, string> Data = new Dictionary<string, string>();
		internal bool Modified = false;

		internal string this[string key] {
			get {
				if (!Parent.Fields.ContainsKey(key)) {
					Error.Err($"There is no field named '{key}' in this database. Yet data from that field is requested!");
					return "ERROR";
				}
				if (this.Data.ContainsKey(key)) {
					Console.Beep(); Debug.WriteLine($"Field {key} not found!");
					Data[key] = Parent.Fields[key].DefaultValue("DEFAULT");
				}
				return Data[key];
			}
			set {
				if (!Parent.Fields.ContainsKey(key)) 
					Error.Err($"There is no field named '{key}' in this database. Yet a request to assign data to that field was done. This request will be ignored!");
				Data[key] = value;
				Modified = true;
			}
		}

		internal MyDataRecord(MyData Parent,string template="Default") {
			this.Parent = Parent;
			foreach(var k in Parent.Fields) {
				Data[k.Key] = k.Value.DefaultValue(template);
			}
		}
	}
}