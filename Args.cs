// Lic:
// MyData II
// CLI argument handler
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
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrickyUnits;

namespace MyData_II {
	static internal class Args {
		internal static readonly string[] pure = Environment.GetCommandLineArgs();
		internal static string Executable => pure[0];

		private static readonly SortedDictionary<string, bool> boolswitches;
		private static readonly SortedDictionary<string,string> stringswitches;
		private static readonly SortedDictionary<string, int> intswitches;

		internal static int swInt(string sw) { sw = sw.ToUpper();  if (intswitches.ContainsKey(sw)) return intswitches[sw]; else return 0; }
		internal static string swStr(string sw) { sw = sw.ToUpper(); if (stringswitches.ContainsKey(sw)) return stringswitches[sw]; else return ""; }
		internal static bool swBool(string sw) { sw = sw.ToUpper(); if (boolswitches.ContainsKey(sw)) return boolswitches[sw]; else return false; }

		internal static readonly string[] DataBaseFiles = null;
		internal static readonly SortedDictionary<string, MyData> DataBases = new SortedDictionary<string, MyData>();

		static Args() {
			MKL.Version("MyData II - Args.cs","23.08.14");
			MKL.Lic    ("MyData II - Args.cs","GNU General Public License 3");
			Dirry.InitAltDrives();
			boolswitches = new SortedDictionary<string, bool>();
			stringswitches = new SortedDictionary<string, string>();
			intswitches = new SortedDictionary<string, int>();
			var _dbf = new List<string>();
			int i = 0;
			while (++i < pure.Length) {
				if (pure[i][0] == '-') {
					var sw = pure[i].Substring(1).ToUpper();
					switch (sw) {

						// Pop version information
						case "V":
						case "VERSION":
							Confirm.Annoy($"MyData {MKL.Newest}\n\n{MKL.All()}");
							break;

						// Integer switches
						case "INTEGERTEST": {
								if (++i >= pure.Length) Error.Crash($"Switch -{sw} expected an integer value, but got nothing");
								try {
									intswitches[sw] = int.Parse(pure[i]);
								} catch (Exception e) {
									Error.Crash($"Trying to parse the value {pure[i]} for switch -{sw} led to the exception {e.Message}");
								}
							}
							break;

						// String switches
						case "STRINGTEST": {
								if (++i >= pure.Length) Error.Crash($"Switch -{sw} expected a string value, but got nothing");
								stringswitches[sw] = pure[i];
							}
							break;

						// Boolean switches
						case "BOOLTEST":
							boolswitches[sw] = true;
							break;

						default:
							Error.Crash($"Unknown switch {sw}");
							break;
					}
					if (swStr("StringTest") != "") Confirm.Annoy($"String Test: {swStr("StringTest")}");
					if (swInt("IntegerTest") != 0) Confirm.Annoy($"Integer Test: {swInt("IntegerTest")}");
					if (swBool("BoolTest")) Confirm.Annoy("A boolean test was requested");
				} else {
					_dbf.Add(pure[i]);
				}
			}
			if (_dbf.Count <= 0) {
				Confirm.Annoy("No database has been chosen. Please choose a database file");
				var f = FFS.RequestFile();
				if (f == "") Environment.Exit(0);
				_dbf.Add(f);
			}
			_dbf.Sort();
			DataBaseFiles = _dbf.ToArray();
			var AnySuccess = false;
			foreach (var f in DataBaseFiles) {
				uint c = 0;
				var t = qstr.StripDir(f);
				while (DataBases.ContainsKey(t)) t = $"{qstr.StripDir(t)} ({++c})";
				DataBases[t] = MyData.Load(f);
				AnySuccess = AnySuccess || DataBases[t] != null;
			}
			if (!AnySuccess) { Error.Crash("None of the requested databases have been loaded succesfully"); Environment.Exit(3); }
		}
	}
}