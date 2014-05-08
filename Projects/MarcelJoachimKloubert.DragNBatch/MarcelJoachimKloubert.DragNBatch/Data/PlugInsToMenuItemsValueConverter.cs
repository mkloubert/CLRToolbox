// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Windows.Data;
using MarcelJoachimKloubert.DragNBatch.PlugIns;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MarcelJoachimKloubert.DragNBatch.Data
{
    /// <summary>
    /// Converts a list of <see cref="IPlugIn" /> to <see cref="MenuItem" /> and back.
    /// </summary>
    public sealed class PlugInsToMenuItemsValueConverter : ValueConverterBase<IEnumerable<IPlugIn>, IEnumerable<MenuItem>>
    {
        #region Methods (3)

        // Protected Methods (2) 

        /// <inheriteddoc />
        protected override IEnumerable<MenuItem> Convert(IEnumerable<IPlugIn> plugIns, string parameter, CultureInfo culture)
        {
            if (plugIns == null)
            {
                return null;
            }

            var result = new List<MenuItem>();

            plugIns.OfType<IPlugIn>()
                   .Select(p => new
                           {
                               DisplayText = p.GetDisplayName(culture),
                               PlugIn = p,
                           })
                   .OrderBy(x => x.DisplayText, StringComparer.Create(culture, true))
                   .ForEach(ctx =>
                            {
                                var x = ctx.Item;

                                MenuItem newItem = new MenuItem();
                                newItem.Header = x.DisplayText;
                                newItem.IsCheckable = false;
                                newItem.Tag = x.PlugIn;
                                newItem.Click += CreateClickHandler(newItem, ctx.State.MenuItems);

                                ctx.State
                                   .MenuItems
                                   .Add(newItem);
                            }, actionState: new
                            {
                                Culture = culture,
                                MenuItems = result,
                            });

            return result;
        }

        /// <inheriteddoc />
        protected override IEnumerable<IPlugIn> ConvertBack(IEnumerable<MenuItem> menuItems, string parameter, CultureInfo culture)
        {
            if (menuItems == null)
            {
                return null;
            }

            return menuItems.OfType<MenuItem>()
                            .Select(mi => mi.Tag)
                            .OfType<IPlugIn>();
        }

        // Private Methods (1) 

        private static RoutedEventHandler CreateClickHandler(MenuItem item, IEnumerable<MenuItem> allItems)
        {
            return (sender, e) =>
                {
                    allItems.ForEach(ctx =>
                        {
                            var i = ctx.Item;
                            i.IsChecked = item.Equals(i) ? true : false;

                            if (i.IsChecked)
                            {
                                App.Current
                                   .MainWindow
                                   .ViewModel
                                   .SelectedPlugIn = i.Tag as IPlugIn;
                            }
                        });
                };
        }

        #endregion Methods
    }
}