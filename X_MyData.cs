// Lic:
// MyData II
// Export (save) to the standard MyData format
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
// Version: 23.08.19
// EndLic
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickyUnits;

namespace MyData_II {
	internal class X_MyData:Export {
		public X_MyData() {
			MKL.Version("MyData II - X_MyData.cs","23.08.19");
			MKL.Lic    ("MyData II - X_MyData.cs","GNU General Public License 3");
		}

		public override string XRecord(MyData database, string recname = "", bool addreturn = false) {
			Error.Crash("XRecord show NOT be called from MyData export! Somebody been messing around with the source coude?");
			return ""; // Never called, but C# can't tell and will throw an error if this line is missing.
		}

		public override string XClass(MyData database, string cln) {
			Error.Crash("XClass show NOT be called from MyData export! Somebody been messing around with the source coude?");
			return ""; // Never called, but C# can't tell and will throw an error if this line is missing.
		}

		public override string XBase(MyData database) {
			Debug.WriteLine($"Loading {database.FileName}");
			var raw = QuickStream.LoadLines(database.FileName);
			var DoInc = true;
			var ret = new StringBuilder();
			foreach(var line in raw) {
				var tline=line.Trim().ToUpper();
				if (tline.Length>1 && tline[0] == '[' && tline[tline.Length - 1] == ']') DoInc = tline != "[RECORDS]";
				if (DoInc) ret.Append($"{line}\n");
			}
			ret.Append($"\n[Records]\n# This part was generated on {DateTime.Now} by MyData II\n");
			foreach(var rec in database.Records) {
				ret.Append($"\nREC: {rec.Key}\n");
				var V = rec.Value;
				foreach(var fkey in rec.Value.Keys) {
					var F = database.Fields[fkey];
					switch (F.Type) {
						case MyDataTypes.Info:
						case MyDataTypes.Strike:
							break; // Don't save these!
						default:
							ret.Append($"\t{fkey} = {rec.Value[fkey]}\n");
							break;
					}
				}
			}
			Debug.WriteLine($"{ret.Length}");
			return ret.ToString();
		}
		override public bool AllowPerRec() => false;

		override public string Extension(MyData database) => "MyData";

	}
}