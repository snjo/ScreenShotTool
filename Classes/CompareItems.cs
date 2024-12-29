using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShotTool.Classes
{
    internal class CompareByIndex : IComparer
    {
        // from https://stackoverflow.com/questions/7913455/inserting-an-item-into-a-c-sharp-winforms-listview
        private readonly ListView _listView;

        public CompareByIndex(ListView listView)
        {
            this._listView = listView;
        }
        public int Compare(object x, object y)
        {
            int i = this._listView.Items.IndexOf((ListViewItem)x);
            int j = this._listView.Items.IndexOf((ListViewItem)y);
            return i - j;
        }
    }
}
