﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using NodePad.Common;
using NodePad.ViewModel.Common;

namespace NodePad.ViewModel.Design.Common
{
    public class GroupSelectorVmD : GroupSelectorVm
    {
        public GroupSelectorVmD()
        {
            Items.ForEach(v=>AddItem(v,v.ToString()));
            Orientation = Orientation.Horizontal;
        }

        public static IEnumerable<int> Items => Enumerable.Range(0, 15);
    }
}
