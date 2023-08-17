// Lic:
// MyData II
// Main Window (code)
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
// Version: 23.08.17
// EndLic

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
// using System.Text.RegularExpressions; // Epic Fail
using TrickyUnits;
using System.Linq.Expressions;
using System.Runtime.Remoting.Channels;

namespace MyData_II {


	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		static private bool DoNotModify = false;
		static public Dictionary<bool,Visibility> Visible = new Dictionary<bool,Visibility>();
		static public Dictionary<object,string> Gadget2Field = new Dictionary<object,string>();

		public MainWindow() {
			Visible[true]=Visibility.Visible; Visible[false]=Visibility.Hidden;
			Debug.WriteLine($"Running {Args.Executable}");
			InitializeComponent();
			RefreshLBDatabases();
			#region Generated for GUIArray
			// Start: Generated Part
			GUIArray.grids[0] = DataGrid00;
			GUIArray.ltype[0] = TypeLabel00;
			GUIArray.lname[0] = NameLabel00;
			GUIArray.linfo[0] = InfoLabel00;
			GUIArray.lstrike[0] = Strike00;
			GUIArray.Reg(0, DataText00);
			GUIArray.RegI(0, DataInt00);
			GUIArray.Reg(0, DataBool00);
			GUIArray.Reg(0, DataMC00);
			GUIArray.grids[1] = DataGrid01;
			GUIArray.ltype[1] = TypeLabel01;
			GUIArray.lname[1] = NameLabel01;
			GUIArray.linfo[1] = InfoLabel01;
			GUIArray.lstrike[1] = Strike01;
			GUIArray.Reg(1, DataText01);
			GUIArray.RegI(1, DataInt01);
			GUIArray.Reg(1, DataBool01);
			GUIArray.Reg(1, DataMC01);
			GUIArray.grids[2] = DataGrid02;
			GUIArray.ltype[2] = TypeLabel02;
			GUIArray.lname[2] = NameLabel02;
			GUIArray.linfo[2] = InfoLabel02;
			GUIArray.lstrike[2] = Strike02;
			GUIArray.Reg(2, DataText02);
			GUIArray.RegI(2, DataInt02);
			GUIArray.Reg(2, DataBool02);
			GUIArray.Reg(2, DataMC02);
			GUIArray.grids[3] = DataGrid03;
			GUIArray.ltype[3] = TypeLabel03;
			GUIArray.lname[3] = NameLabel03;
			GUIArray.linfo[3] = InfoLabel03;
			GUIArray.lstrike[3] = Strike03;
			GUIArray.Reg(3, DataText03);
			GUIArray.RegI(3, DataInt03);
			GUIArray.Reg(3, DataBool03);
			GUIArray.Reg(3, DataMC03);
			GUIArray.grids[4] = DataGrid04;
			GUIArray.ltype[4] = TypeLabel04;
			GUIArray.lname[4] = NameLabel04;
			GUIArray.linfo[4] = InfoLabel04;
			GUIArray.lstrike[4] = Strike04;
			GUIArray.Reg(4, DataText04);
			GUIArray.RegI(4, DataInt04);
			GUIArray.Reg(4, DataBool04);
			GUIArray.Reg(4, DataMC04);
			GUIArray.grids[5] = DataGrid05;
			GUIArray.ltype[5] = TypeLabel05;
			GUIArray.lname[5] = NameLabel05;
			GUIArray.linfo[5] = InfoLabel05;
			GUIArray.lstrike[5] = Strike05;
			GUIArray.Reg(5, DataText05);
			GUIArray.RegI(5, DataInt05);
			GUIArray.Reg(5, DataBool05);
			GUIArray.Reg(5, DataMC05);
			GUIArray.grids[6] = DataGrid06;
			GUIArray.ltype[6] = TypeLabel06;
			GUIArray.lname[6] = NameLabel06;
			GUIArray.linfo[6] = InfoLabel06;
			GUIArray.lstrike[6] = Strike06;
			GUIArray.Reg(6, DataText06);
			GUIArray.RegI(6, DataInt06);
			GUIArray.Reg(6, DataBool06);
			GUIArray.Reg(6, DataMC06);
			GUIArray.grids[7] = DataGrid07;
			GUIArray.ltype[7] = TypeLabel07;
			GUIArray.lname[7] = NameLabel07;
			GUIArray.linfo[7] = InfoLabel07;
			GUIArray.lstrike[7] = Strike07;
			GUIArray.Reg(7, DataText07);
			GUIArray.RegI(7, DataInt07);
			GUIArray.Reg(7, DataBool07);
			GUIArray.Reg(7, DataMC07);
			GUIArray.grids[8] = DataGrid08;
			GUIArray.ltype[8] = TypeLabel08;
			GUIArray.lname[8] = NameLabel08;
			GUIArray.linfo[8] = InfoLabel08;
			GUIArray.lstrike[8] = Strike08;
			GUIArray.Reg(8, DataText08);
			GUIArray.RegI(8, DataInt08);
			GUIArray.Reg(8, DataBool08);
			GUIArray.Reg(8, DataMC08);
			GUIArray.grids[9] = DataGrid09;
			GUIArray.ltype[9] = TypeLabel09;
			GUIArray.lname[9] = NameLabel09;
			GUIArray.linfo[9] = InfoLabel09;
			GUIArray.lstrike[9] = Strike09;
			GUIArray.Reg(9, DataText09);
			GUIArray.RegI(9, DataInt09);
			GUIArray.Reg(9, DataBool09);
			GUIArray.Reg(9, DataMC09);
			GUIArray.grids[10] = DataGrid10;
			GUIArray.ltype[10] = TypeLabel10;
			GUIArray.lname[10] = NameLabel10;
			GUIArray.linfo[10] = InfoLabel10;
			GUIArray.lstrike[10] = Strike10;
			GUIArray.Reg(10, DataText10);
			GUIArray.RegI(10, DataInt10);
			GUIArray.Reg(10, DataBool10);
			GUIArray.Reg(10, DataMC10);
			GUIArray.grids[11] = DataGrid11;
			GUIArray.ltype[11] = TypeLabel11;
			GUIArray.lname[11] = NameLabel11;
			GUIArray.linfo[11] = InfoLabel11;
			GUIArray.lstrike[11] = Strike11;
			GUIArray.Reg(11, DataText11);
			GUIArray.RegI(11, DataInt11);
			GUIArray.Reg(11, DataBool11);
			GUIArray.Reg(11, DataMC11);
			GUIArray.grids[12] = DataGrid12;
			GUIArray.ltype[12] = TypeLabel12;
			GUIArray.lname[12] = NameLabel12;
			GUIArray.linfo[12] = InfoLabel12;
			GUIArray.lstrike[12] = Strike12;
			GUIArray.Reg(12, DataText12);
			GUIArray.RegI(12, DataInt12);
			GUIArray.Reg(12, DataBool12);
			GUIArray.Reg(12, DataMC12);
			GUIArray.grids[13] = DataGrid13;
			GUIArray.ltype[13] = TypeLabel13;
			GUIArray.lname[13] = NameLabel13;
			GUIArray.linfo[13] = InfoLabel13;
			GUIArray.lstrike[13] = Strike13;
			GUIArray.Reg(13, DataText13);
			GUIArray.RegI(13, DataInt13);
			GUIArray.Reg(13, DataBool13);
			GUIArray.Reg(13, DataMC13);
			GUIArray.grids[14] = DataGrid14;
			GUIArray.ltype[14] = TypeLabel14;
			GUIArray.lname[14] = NameLabel14;
			GUIArray.linfo[14] = InfoLabel14;
			GUIArray.lstrike[14] = Strike14;
			GUIArray.Reg(14, DataText14);
			GUIArray.RegI(14, DataInt14);
			GUIArray.Reg(14, DataBool14);
			GUIArray.Reg(14, DataMC14);
			GUIArray.grids[15] = DataGrid15;
			GUIArray.ltype[15] = TypeLabel15;
			GUIArray.lname[15] = NameLabel15;
			GUIArray.linfo[15] = InfoLabel15;
			GUIArray.lstrike[15] = Strike15;
			GUIArray.Reg(15, DataText15);
			GUIArray.RegI(15, DataInt15);
			GUIArray.Reg(15, DataBool15);
			GUIArray.Reg(15, DataMC15);
			// End:   Generated part
			#endregion
			PageGrid.Visibility = Visibility.Hidden;
			PageMainGrid.Visibility = Visibility.Hidden;
			RecordsGrid.Visibility = Visibility.Hidden;
		}

		void RefreshLBDatabases() {
			LB_Databases.Items.Clear();
			foreach(var k in Args.DataBases.Keys) LB_Databases.Items.Add(k);
		}

		void OnWindowClose(object sender, EventArgs e) {
			Debug.WriteLine("User requested to close");
		}

		private void Act_Filter(object sender, RoutedEventArgs e) {
			RefreshLBRecords();
		}

		internal string ChosenRecordName {
			get {
				if (LB_Records.SelectedItem == null) return "";
				return LB_Records.SelectedItem.ToString().ToUpper();
			}
		}

		internal MyDataRecord ChosenRecord {
			get {
				if (LB_Records.SelectedItem == null) return null;
				return MyData.CurrentDatabase.Record(ChosenRecordName);
			}

		}

		void RefreshLBRecords() {
			if (MyData.CurrentDatabase == null) return;
			var Expression = TB_Filter.Text.ToUpper().Trim();			
			LB_Records.Items.Clear();
			foreach (var entry in MyData.CurrentDatabase.Records.Keys) {
				// if (Expression!="*") Confirm.Annoy($" '{entry}' '{Expression}' math:{Regex.IsMatch(entry, $"^{Expression}$")}"); // debug only!
				   
				// Temp solution as RegEx is failing on me (as usual).
				if(Expression == "" || Expression == "*" || entry.Contains(Expression))
					LB_Records.Items.Add(entry);
			}
			PageGrid.Visibility = Visibility.Hidden;
		}

		void RefreshLBPages() {
			if (MyData.CurrentDatabase == null) return;
			PageList.Items.Clear();
			foreach (var k in MyData.CurrentDatabase.Pages) PageList.Items.Add(k.PageName);
		}

		void SetUpDataView() {
			Debug.WriteLine($"Set up Dataview\n- LB_Databases.SelectedItem={LB_Databases.SelectedItem}\n- LB_Records.SelectedItem = {LB_Records.SelectedItem}\n- PageList.SelectedItem={PageList.SelectedItem}\n ");

			if (LB_Databases.SelectedItem == null || LB_Records.SelectedItem == null || PageList.SelectedItem == null) {
				PageGrid.Visibility = Visibility.Hidden;
				return;
			}
			PageGrid.Visibility = Visibility.Visible;
			var DNM = DoNotModify;
			DoNotModify = true;
			var idx = PageList.SelectedIndex;
			var pg = MyData.CurrentDatabase.Pages[idx];
			Debug.WriteLine($"- idx = {idx}\n- pg = {pg}");
			Gadget2Field.Clear();
			for(uint i=0;i<=GUIArray.max ; ++i) {
				var hasrec = pg.Fields[i] != null;
				Debug.WriteLine($"- #{i}; hasrec: {hasrec}");
				GUIArray.grids[i].Visibility = Visible[hasrec];
				if (hasrec) {
					var CRec = LB_Records.SelectedItem.ToString();
					var CFld = pg.Fields[i].NameField;
					var CurValue = MyData.CurrentDatabase[CRec,CFld];
					Debug.WriteLine($"LB_Records.SelectedItem.ToString() = '{LB_Records.SelectedItem.ToString()}'; pg.Fields[{i}].NameField = '{pg.Fields[i].NameField}'; CurValue = '{CurValue}';");
					Debug.WriteLine($"- {pg.Fields[i].Type} {pg.Fields[i].NameField}");
					GUIArray.ltype[i].Content = pg.Fields[i].Type.ToString();
					GUIArray.lname[i].Content = pg.Fields[i].NameField;
					switch(pg.Fields[i].Type) {
						case MyDataTypes.String:
							GUIArray.tb[i].Visibility= Visibility.Visible;
							GUIArray.tbi[i].Visibility = Visibility.Hidden;
							GUIArray.chk[i].Visibility = Visibility.Hidden;
							GUIArray.cb[i].Visibility = Visibility.Hidden;
							GUIArray.linfo[i].Visibility = Visibility.Hidden;
							GUIArray.lstrike[i].Visibility = Visibility.Hidden;
							GUIArray.tb[i].Text = CurValue;
							Gadget2Field[GUIArray.tb[i]] = CFld;
							break;
						case MyDataTypes.Int:
							GUIArray.chk[i].Visibility = Visibility.Hidden;
							GUIArray.cb[i].Visibility = Visibility.Hidden;
							GUIArray.tb[i].Visibility = Visibility.Hidden;
							GUIArray.tbi[i].Visibility = Visibility.Visible;
							GUIArray.linfo[i].Visibility = Visibility.Hidden;
							GUIArray.lstrike[i].Visibility = Visibility.Hidden;
							GUIArray.tbi[i].Text = MyData.CurrentDatabase.Record(CRec).IntVal(CFld).ToString();
							Gadget2Field[GUIArray.tbi[i]] = CFld;
							break;
					   case MyDataTypes.Bool:
							GUIArray.chk[i].Visibility = Visibility.Visible;
							GUIArray.cb[i].Visibility = Visibility.Hidden;
							GUIArray.tb[i].Visibility = Visibility.Hidden;
							GUIArray.tbi[i].Visibility = Visibility.Hidden;
							GUIArray.linfo[i].Visibility = Visibility.Hidden;
							GUIArray.lstrike[i].Visibility = Visibility.Hidden;
							GUIArray.chk[i].IsChecked = MyData.CurrentDatabase.Record(CRec).BoolVal(CFld);
							Gadget2Field[GUIArray.chk[i]] = CFld;
							break;
						case MyDataTypes.MC:
							GUIArray.chk[i].Visibility = Visibility.Hidden;
							GUIArray.cb[i].Visibility = Visibility.Visible;
							GUIArray.tb[i].Visibility = Visibility.Hidden;
							GUIArray.tbi[i].Visibility = Visibility.Hidden;
							GUIArray.linfo[i].Visibility = Visibility.Hidden;
							GUIArray.lstrike[i].Visibility = Visibility.Hidden;
							Gadget2Field[GUIArray.cb[i]] = CFld;
							GUIArray.cb[i].Items.Clear();
							int itv = 0;
							bool found = false;
							if (MyData.CurrentDatabase.Fields[CFld].FieldItems.Count > 0) {
								for (int iti = 0; iti < MyData.CurrentDatabase.Fields[CFld].FieldItems.Count; ++iti) {
									var item = MyData.CurrentDatabase.Fields[CFld].FieldItems[iti];
									GUIArray.cb[i].Items.Add(item);
									if (item == CurValue) {
										found = true;
										itv = iti;
									}
								}
								if ((!found) && CurValue!="") {
									Error.Err($"Field \"{CFld}\" does not have an item with the string \"{CurValue}\"");
									MyData.CurrentDatabase[CRec, CFld] = MyData.CurrentDatabase.Fields[CFld].FieldItems[0];
								}
								GUIArray.cb[i].SelectedIndex = itv;
								GUIArray.cb[i].IsEnabled = true;
							} else
								GUIArray.cb[i].IsEnabled = false;
							break;
						case MyDataTypes.Info:
							GUIArray.chk[i].Visibility = Visibility.Hidden;
							GUIArray.cb[i].Visibility = Visibility.Hidden;
							GUIArray.tb[i].Visibility = Visibility.Hidden;
							GUIArray.tbi[i].Visibility = Visibility.Hidden;
							GUIArray.linfo[i].Visibility = Visibility.Visible;
							GUIArray.lstrike[i].Visibility = Visibility.Hidden;
							GUIArray.linfo[i].Content = pg.Fields[i].NameField;
							GUIArray.ltype[i].Content = "";
							GUIArray.lname[i].Content = "";
							break;
						case MyDataTypes.Strike:
							GUIArray.chk[i].Visibility = Visibility.Hidden;
							GUIArray.cb[i].Visibility = Visibility.Hidden;
							GUIArray.ltype[i].Content = "";
							GUIArray.lname[i].Content = "";
							GUIArray.tb[i].Visibility = Visibility.Hidden;
							GUIArray.tbi[i].Visibility = Visibility.Hidden;
							GUIArray.linfo[i].Visibility = Visibility.Hidden;
							GUIArray.lstrike[i].Visibility = Visibility.Visible;
							break;
					}
				}
			}

			// show the datafields and give their data
			DoNotModify = DNM;
		}

		private void LB_Databases_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (LB_Databases.SelectedItem == null) {
				PageGrid.Visibility = Visibility.Hidden;
				PageMainGrid.Visibility = Visibility.Hidden;
				RecordsGrid.Visibility = Visibility.Hidden;
				return;
			}
			RecordsGrid.Visibility = Visibility.Visible;
			MyData.CurrentDatabase = Args.DataBases[(string)LB_Databases.SelectedItem];
			PageMainGrid.Visibility=Visibility.Visible;
			RefreshLBRecords();
			RefreshLBPages();
			SetUpDataView();
		}

		private void PageList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			SetUpDataView();
		}

		private void LB_Records_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			SetUpDataView();
		}

		private void Act_Check(object sender, RoutedEventArgs e) {
			var C = (CheckBox)sender;
			C.Content = $"{C.IsChecked}";
			if (DoNotModify) return;
			if (Gadget2Field.ContainsKey(sender)) {
				ChosenRecord[Gadget2Field[sender]] = C.IsChecked.ToString();
			}
		}

		private void Act_TBChange(object sender, TextChangedEventArgs e) {
			if (DoNotModify) return;
			var T=(TextBox)sender;
			if (Gadget2Field.ContainsKey(sender)) {
				var f = Gadget2Field[sender];
				switch (MyData.CurrentDatabase.Fields[f].Type) {
					case MyDataTypes.String:
						ChosenRecord[f] = T.Text;
						break;
					case MyDataTypes.Int: {
							try {
								ChosenRecord[f] = int.Parse(T.Text).ToString(); // Seems odd, but this FORCES an integer to be in this field. Very important later with exporting!
							} catch {
								ChosenRecord[f] = "0";

							}
						}
						break;
					default:
						Error.Crash($"Internal error! Please report! Textbox handling \"{MyData.CurrentDatabase.Fields[f].Type}\" requested. Should not be posisble! ");
						break;
				}
				
			}
		}

		private void DataMC_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (DoNotModify) return;
			var MCV=(ComboBox)sender;
			ChosenRecord[Gadget2Field[sender]]=MCV.SelectedItem.ToString();
		}
	}
}