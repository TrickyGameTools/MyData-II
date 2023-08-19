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
// Version: 23.08.19
// EndLic

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using TrickyUnits;

namespace MyData_II {

	abstract class Export {

		public const string eol = "\n";
		public Export() {
			MKL.Version("MyData II - X_Base.cs","23.08.19");
			MKL.Lic    ("MyData II - X_Base.cs","GNU General Public License 3");
		}

		readonly static Dictionary<string,Export> Register = new Dictionary<string,Export>();

		readonly static public Export XMyData = new X_MyData();
		public static void RegisterExports() {
			// MyData itself may NOT be among the Exports!
			// All key strings must be in ALL CAPS.
			Register["GINIE"] = new X_GINIE();
			Register["JSON"] = new X_JSON();
			Register["LUA"] = new X_Lua();
			Register["NEIL"] = new X_Neil();
			Register["PHP"] = new X_php();
			Register["PYTHON"] = new X_Python();
			Register["XML"] = new X_XML();
			Register["YAML"] = new X_YAML();
		}

		internal string Header(MyData MD,string p) {
			var H = $"{p} Generated with MyData II on {DateTime.Now}\n";
			if (MD.Sys_License!="") H+=$"{p} License: {MD.Sys_License}\n";
			H += "\n\n";
			return H;
		}

		/// <summary>
		/// Exports a database to all export files set in the [sys] block
		/// </summary>
		/// <param name="database">The database to export</param>
		public static void ExportToFile(MyData database) {
			foreach(var X in database.Sys.ExportRec) {
				if (X.Value!="") {
					if (!Register.ContainsKey(X.Key)) {
						Error.Err($"Request to export the database in record by record to {X.Key}. However no driver is known to do that!");
					} else if (!Register[X.Key].AllowPerRec()) {
						Error.Err($"Request to export the database in record by record to {X.Key}. However this driver does not allow that");
					} else {
						//Debug.WriteLine("Not yet implemented so I cannot export record by record!");
						var oDIR = Dirry.AD(Dirry.C(X.Value));						
						if (File.Exists(oDIR)) {
							Error.Err($"A file name {oDIR} is in the way for rec export");

						} else {
							if (!Directory.Exists(oDIR)) {
								if (Confirm.Yes($"Create {X.Value} for exporting to {X.Key}?")) Directory.CreateDirectory(oDIR);
							}
							if (Directory.Exists(oDIR)) {
								var reg = Register[X.Key];
								var ods = $"{oDIR}/";
								var _class = reg.XClass(database, qstr.StripAll(oDIR)); // classname lookup
								if (_class != "") QuickStream.SaveString($"{ods}/MyData_ClassFile_{qstr.StripAll(oDIR)}.{reg.Extension(database)}", _class);
								foreach(var R in database.Records) {
									var file = $"{ods}/{R.Key}.{reg.Extension(database)}";
									if (R.Value.Modified || !File.Exists(file)) {
										var _rec = reg.XRecord(database, R.Key, true);
										QuickStream.SaveString(file, _rec);
									}
								}

							}
						}
					}
				}
			}
			foreach(var X in database.Sys.ExportBase) {
				if (X.Value != "") {
					if (Register.ContainsKey(X.Key)) {
						var XS = Register[X.Key].XBase(database);
						QuickStream.SaveString(Dirry.AD(Dirry.C(X.Value)), XS);
					} else {
						Error.Err($"Request to export the database in full to {X.Key}. However no driver is known to do that!");
					}
				}
			}
		}

		/// <summary>
		/// Exports ALL loaded databases, if they have been configured to be exported.
		/// </summary>
		public static void ExportToFile() {
			foreach (var db in Args.DataBases.Values)
				if (db.Sys_AutoExport)
					ExportToFile(db);
		}

		abstract public string XRecord(MyData database, string recname = "", bool addreturn = false);
		abstract public string XBase(MyData database);
		virtual public string XClass(MyData database, string cln) { return ""; }
		virtual public bool AllowPerRec() { return true; }

		abstract public string Extension(MyData database);

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