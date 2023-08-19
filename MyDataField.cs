// Lic:
// MyData II
// Field Management
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
// Version: 23.08.19
// EndLic
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyData_II {
    internal class MyDataField {
        internal readonly MyDataTypes Type = MyDataTypes.None;
        internal readonly string NameField;
        internal readonly uint Index = 0;
        internal readonly MyDataPage Parent;

        internal string SType => $"{Type}";
        internal string LType => SType.ToLower();
        internal string UType => SType.ToUpper();

        static internal MyDataField NewestField { get; private set; } = null;

        internal readonly List<string> FieldItems = new List<string>();

        private Dictionary<string,string> DefaultValues = new Dictionary<string,string>();
        internal string DefaultValue(string template) {
            template= template.Trim().ToUpper();
            if (DefaultValues.ContainsKey(template)) return DefaultValues[template];
            switch (Type) {
                case MyDataTypes.Int:
                    return "0";
                case MyDataTypes.Bool:
                    return "False";
                default:
                    return "";
            }
        }
        internal void DefaultValue(string template,string value) {
            template = template.Trim().ToUpper();
            DefaultValues[template] = value;
        }

        internal ComboBox FCombo => GUIArray.cb[Index];

        internal TextBox FText => GUIArray.tb[Index];

        internal CheckBox FCheck => GUIArray.chk[Index];

        internal MyDataField(MyDataPage Ouwe, MyDataTypes type, string name, uint idx,string defaultvalue="") {
            Parent = Ouwe;
            Type = type;
            NameField = name;
            Index = idx;
            DefaultValue("default", defaultvalue);
            NewestField = this;
        }

        internal MyDataField(MyDataPage Ouwe, string type, string name,uint idx,string defaultvalue="") {
            Parent = Ouwe;
            Type = MyData.MyDataTypesMap[type.ToUpper()];
            NameField = name;
            Index = idx;
            DefaultValue("default", defaultvalue);
            NewestField = this;
        }
    }
}