using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ameba.Common.Controls
{
    public class BitCheckBox : CheckBox
    {
        #region Cвойства зависимостей
        public static readonly DependencyProperty ValueUInt32Property = DependencyProperty.Register("ValueUInt32", typeof(UInt32), typeof(BitCheckBox),
            new FrameworkPropertyMetadata((UInt32)(0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, propertyChangedCallback, null, true, UpdateSourceTrigger.PropertyChanged));

        static void propertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs baseValue)
        {
            BitCheckBox obj = (BitCheckBox)d;

            obj.IsChecked = ((UInt32)baseValue.NewValue & (1 << obj.ValueBit)) != 0;
        }
        #endregion

        #region Публичные свойства
        public UInt32 ValueUInt32
        {
            get
            {
                return (UInt32)GetValue(ValueUInt32Property);
            }
            set
            {
                SetValue(ValueUInt32Property, value);
            }
        }

        public byte ValueBit { get; set; }
        #endregion

        #region Конструктор
        public BitCheckBox()
        {
            Checked += BitCheckBox_Checked;
            Unchecked += BitCheckBox_Unchecked;
        }

        private void BitCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ValueUInt32 &= (UInt32)(~(1 << ValueBit));
        }

        private void BitCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ValueUInt32 |= (UInt32)(1 << ValueBit);
        }
        #endregion
    }
}
