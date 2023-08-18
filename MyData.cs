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
// Version: 23.08.18
// EndLic

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrickyUnits;

namespace MyData_II {
	enum MyDataTypes { None,Info, Strike, String, Int, Bool, MC};

	enum MyDataParseState { None, Sys, Page, Template, PrefixToTemplate, Record };
	
	internal class MyData {
		internal readonly string FileName;
		internal static readonly Dictionary<string,MyDataTypes> MyDataTypesMap = new Dictionary<string,MyDataTypes>();

		internal readonly List<MyDataPage> Pages = new List<MyDataPage>();
		internal readonly Dictionary<string,MyDataField> Fields = new Dictionary<string,MyDataField>();
		internal readonly SortedDictionary<string, MyDataRecord> Records = new SortedDictionary<string, MyDataRecord>();

		internal static MyData CurrentDatabase = null;

		#region Sys
		internal string Sys_OutPutGINIEBase { get; private set; } = "";
		internal string Sys_OutPutGINIERec { get; private set; } = "";

		internal string Sys_License { get; private set; } = "";
		internal bool Sys_AutoExport { get; private set; } = false;
		#endregion

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

		internal static bool ValidRecName(string k) {
			bool ret = true;
			for(int a = 0; a < k.Length; ++a) {
				ret = ret && (
					k[a] == '_' ||
					(k[a]>='A' && k[a]<='Z') ||
					(k[a]>='a' && k[a]<='z') ||
					(k[a]>='0' && k[a]<='9')
					);
			}
			return ret;
		}

		internal MyDataRecord Record (string rec) {
			rec = rec.ToUpper();
			if (!Records.ContainsKey(rec)) {
				Error.Err($"Record {rec} not found.");
				return null;
			}
			return Records[rec];
		}

		internal bool RecordExists(string rec) {
			rec = rec.ToUpper();
			return Records.ContainsKey(rec);
		}

		internal string this[string rec, string fld] {
			get {
				var r = Record(rec);
				if (r == null) { return "ERROR!"; }
				return r[fld];
			}

			set {
				var r = Record(rec);
				if (r == null) { return; }
				r[fld] = value;
			}
		}

		private MyData(string _FileName) {
			FileName = _FileName;
			var values = Enum.GetValues(typeof(MyDataTypes));
			if (!MyDataTypesMap.ContainsKey("INT")) {
				foreach (var value in values) {
					var ev = (MyDataTypes)value;
					var tag = $"{ev}";
					MyDataTypesMap[tag.ToUpper()] = ev;
					Debug.WriteLine($"typedef {ev} => ({tag});");
				}
			}
			var L = QuickStream.LoadLines(FileName);
			Parse(L,FileName);
		}

		private void Parse(string[] Line,string filename="???") {
			var Stage=MyDataParseState.None;
			MyDataPage CurrentPage = null;
			MyDataRecord CurrentRecord = null;
			string CurrentTemplate = "";
			uint strikes = 0;
			for(int lnum=0; lnum<Line.Length; lnum++) {
				var ErrTag = $"{filename}:{lnum + 1}\n";
				var cline = Line[lnum].Trim();
				if (cline.Length>0 && cline[0] != '#') { // ignore white lines and comments
					if (cline[0] == '[' && cline[cline.Length - 1] == ']') {
						CurrentRecord = null;
						var l = cline.Substring(1); l = l.Substring(0, l.Length - 1).Trim();
						if (l.ToUpper() == "SYS" || l.ToUpper() == "SYSTEM") {
							Stage = MyDataParseState.Sys;
						} else if (qstr.Prefixed(l.ToUpper(), "PAGE:")) {
							var namepage = l.Substring(5);
							CurrentPage = new MyDataPage(this, namepage);
							Stage = MyDataParseState.Page;
						} else if (l.ToUpper() == "DEFAULT") {
							// [Default] is deprecated. [Template:Default] should be used in stead
							CurrentTemplate = "DEFAULT";
							Stage = MyDataParseState.Template;
						} else if (qstr.Prefixed(l.ToUpper(), "TEMPLATE:")) {
							var p = l.IndexOf(':');
							CurrentTemplate = l.Substring(p + 1).Trim();
							Stage = MyDataParseState.Page;
						} else if (qstr.Prefixed(l.ToUpper(), "RECORDS")) {
							Stage = MyDataParseState.Record;
						} else {
							throw new Exception($"\n\n\n{ErrTag}New stage definition unknown\n{cline}");
						}
					} else {
						switch (Stage) {
							case MyDataParseState.None: throw new Exception($"Parse error!\n{ErrTag}\nThe stage has not yet been set!");
							case MyDataParseState.Sys: {
									var p = cline.IndexOf('=');
									if (p<0) {
										Error.Err($"{ErrTag}Syntax error in sys definition");
									} else {
										var key = cline.Substring(0, p).Trim().ToUpper();
										var value=cline.Substring(p+1).Trim();	
										switch(key) {
											case "AUTOOUTPUT": // Deprecated!
											case "AUTOEXPORT": // AutoExport is the new value
												Sys_AutoExport = value.ToUpper() == "TRUE" || value.ToUpper() == "YES";
												break;
											case "LICENSE":
											case "LIC":
												Sys_License = value;
												break;
											case "OUTPUTGINIEREC":
												Sys_OutPutGINIERec = value;
												break;
											case "OUTPUTGINIEBASE":
												Sys_OutPutGINIEBase = value;
												break;
											default:
												Error.Err($"{ErrTag}Unknown system variable: {key}");
												break;
										}
									}

								}
								break;
							case MyDataParseState.Page: {
									if (qstr.Prefixed(cline.ToUpper(), "STRIKE")) {
										CurrentPage = CurrentPage.NewField("STRIKE", $"*_STRIKE{strikes++}_*", "");
									} else {
										var p = cline.IndexOf(' ');
										if (p < 0) {
											Error.Err($"{ErrTag}Syntax error in field declaration");
										}
										var t = cline.Substring(0, p).ToUpper();
										var rest = cline.Substring(p + 1).Trim();
										if (t[0] == '@') {
											if (MyDataField.NewestField == null)
												Error.Err($"{ErrTag}Item addition request without field");
											else if (MyDataField.NewestField.Type != MyDataTypes.MC)
												Error.Err($"{ErrTag}Item addition only possible at MC (Multiple Choice fields) and not with {MyDataField.NewestField.Type} fields");
											else
												MyDataField.NewestField.FieldItems.Add(rest);
										} else {
											var f = rest;
											var d = "";
											p = rest.IndexOf("=");
											if (p >= 0) {
												f = rest.Substring(0, p);
												d = rest.Substring(p + 1);
											}
											if (!MyDataTypesMap.ContainsKey(t))
												Error.Err($"{ErrTag}Unknown type: {t}");
											else
												CurrentPage = CurrentPage.NewField(t, f, d);
										}
									}
								}
								break;
							case MyDataParseState.Template: {
									var p = cline.IndexOf('=');
									if (p < 1)
										Error.Err("Template defintion syntax error");
									else {
										var k = cline.Substring(0, p).Trim();
										var v = cline.Substring(p + 1).Trim();
										if (Fields.ContainsKey(k)) {
											Fields[k].DefaultValue(CurrentTemplate, v);
										}
									}
								}
								break;
							case MyDataParseState.Record: {
									if (qstr.Prefixed(cline.ToUpper(), "REC:")) {
										// Enhancement: I might work it out with the auto prefix to template when the template feature is fully working
										CurrentRecord = new MyDataRecord(this);
										Records[cline.Substring(4).ToUpper().Trim()] = CurrentRecord;
									} else {
										var p = cline.IndexOf('=');
										if (p < 1)
											Error.Err("Value defintion syntax error");
										var f=cline.Substring(0, p).Trim();
										var v=cline.Substring(p + 1).Trim();
										CurrentRecord[f] = v;
										CurrentRecord.Modified = false;
									}
								} break;
							default: throw new Exception($"Internal error!\n{ErrTag}\nUnknown Stage ({Stage})");
						}
					}
				}
			}
		}

		static internal MyData Load(string _FileName) { 
			var RFN= Dirry.AD(_FileName.Replace("\\", "/"));
			try {
				if (!File.Exists(RFN)) { Error.Err($"Database file not found.\nGFN: {_FileName}\nRFN:{RFN}"); return null; }
				return new MyData(RFN);
			} catch(Exception ex) {
				Error.Err($"Loading database failed due to .NET throwing an exception.\nGFN: {_FileName}\nRFN:{RFN}\nNEX:{ex.Message}");
				return null;
			}            
		}
	}
}