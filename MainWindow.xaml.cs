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
// Version: 23.08.15
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

namespace MyData_II {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            Debug.WriteLine($"Running {Args.Executable}");
            InitializeComponent();
            RefreshLBDatabases();
            // Start: Generated Part
            GUIArray.ltype[0] = TypeLabel00;
            GUIArray.linfo[0] = InfoLabel00;
            GUIArray.lstrike[0] = Strike00;
            GUIArray.Reg(0, DataText00);
            GUIArray.ltype[1] = TypeLabel01;
            GUIArray.linfo[1] = InfoLabel01;
            GUIArray.lstrike[1] = Strike01;
            GUIArray.Reg(1, DataText01);
            GUIArray.ltype[2] = TypeLabel02;
            GUIArray.linfo[2] = InfoLabel02;
            GUIArray.lstrike[2] = Strike02;
            GUIArray.Reg(2, DataText02);
            GUIArray.ltype[3] = TypeLabel03;
            GUIArray.linfo[3] = InfoLabel03;
            GUIArray.lstrike[3] = Strike03;
            GUIArray.Reg(3, DataText03);
            GUIArray.ltype[4] = TypeLabel04;
            GUIArray.linfo[4] = InfoLabel04;
            GUIArray.lstrike[4] = Strike04;
            GUIArray.Reg(4, DataText04);
            GUIArray.ltype[5] = TypeLabel05;
            GUIArray.linfo[5] = InfoLabel05;
            GUIArray.lstrike[5] = Strike05;
            GUIArray.Reg(5, DataText05);
            GUIArray.ltype[6] = TypeLabel06;
            GUIArray.linfo[6] = InfoLabel06;
            GUIArray.lstrike[6] = Strike06;
            GUIArray.Reg(6, DataText06);
            GUIArray.ltype[7] = TypeLabel07;
            GUIArray.linfo[7] = InfoLabel07;
            GUIArray.lstrike[7] = Strike07;
            GUIArray.Reg(7, DataText07);
            GUIArray.ltype[8] = TypeLabel08;
            GUIArray.linfo[8] = InfoLabel08;
            GUIArray.lstrike[8] = Strike08;
            GUIArray.Reg(8, DataText08);
            GUIArray.ltype[9] = TypeLabel09;
            GUIArray.linfo[9] = InfoLabel09;
            GUIArray.lstrike[9] = Strike09;
            GUIArray.Reg(9, DataText09);
            GUIArray.ltype[10] = TypeLabel10;
            GUIArray.linfo[10] = InfoLabel10;
            GUIArray.lstrike[10] = Strike10;
            GUIArray.Reg(10, DataText10);
            GUIArray.ltype[11] = TypeLabel11;
            GUIArray.linfo[11] = InfoLabel11;
            GUIArray.lstrike[11] = Strike11;
            GUIArray.Reg(11, DataText11);
            GUIArray.ltype[12] = TypeLabel12;
            GUIArray.linfo[12] = InfoLabel12;
            GUIArray.lstrike[12] = Strike12;
            GUIArray.Reg(12, DataText12);
            GUIArray.ltype[13] = TypeLabel13;
            GUIArray.linfo[13] = InfoLabel13;
            GUIArray.lstrike[13] = Strike13;
            GUIArray.Reg(13, DataText13);
            GUIArray.ltype[14] = TypeLabel14;
            GUIArray.linfo[14] = InfoLabel14;
            GUIArray.lstrike[14] = Strike14;
            GUIArray.Reg(14, DataText14);
            GUIArray.ltype[15] = TypeLabel15;
            GUIArray.linfo[15] = InfoLabel15;
            GUIArray.lstrike[15] = Strike15;
            GUIArray.Reg(15, DataText15);

            // End:   Generated part
        }

        void RefreshLBDatabases() {
            LB_Databases.Items.Clear();
            foreach(var k in Args.DataBases.Keys) LB_Databases.Items.Add(k);
        }

        void OnWindowClose(object sender, EventArgs e) {
            Debug.WriteLine("User requested to close");
        }

        private void Act_Filter(object sender, RoutedEventArgs e) {

        }
    }
}