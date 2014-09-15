using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AR.Drone.WinApp.MyUserControl.MapComboBox
{
    public class ComboBoxTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ContentPresenter presenter = (ContentPresenter)container;
            if (presenter.TemplatedParent is ComboBox)
            {
                return (DataTemplate)presenter.FindResource("ComboCollapsed");
            }
            else
            {
                return (DataTemplate)presenter.FindResource("ComboExpanded");
            }
        }
    }
}
