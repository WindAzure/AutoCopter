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
        public DataTemplate SelectedTemplate
        {
            set;
            get;
        }

        public DataTemplate DropDownTemplate
        {
            set;
            get;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ComboBoxItem comboBoxItem = container.GetVisualParent<ComboBoxItem>();
            if (comboBoxItem == null)
            {
                return SelectedTemplate;
            }
            return DropDownTemplate;
           /* ContentPresenter presenter = (ContentPresenter)container;
            if (presenter.TemplatedParent is ComboBox)
            {
                return (DataTemplate)presenter.FindResource("ComboCollapsed");
            }
            else
            {
                return (DataTemplate)presenter.FindResource("ComboExpanded");
            }*/
        }
    }
}
