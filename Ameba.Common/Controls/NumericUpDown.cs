using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Globalization;
using System.ComponentModel;

namespace Ameba.Common.Controls
{
    [DefaultProperty("Value"), DefaultEvent("ValueChanged")]
    public class NumericUpDown : Control
    {
        #region Fields

        private const decimal DefaultMinimum = 0M;

        private const decimal DefaultMaximum = 100M;

        private const decimal DefaultIncrement = 1M;

        private const int DefaultDecimalPlaces = 0;

        private const NumberStyles NumberStyle = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;

        public static readonly DependencyProperty ValueProperty;

        public static readonly DependencyProperty MinimumProperty;

        public static readonly DependencyProperty MaximumProperty;

        public static readonly DependencyProperty IncrementProperty;

        public static readonly DependencyProperty DecimalPlacesProperty;

        public static readonly DependencyProperty InterceptArrowKeysProperty;

        public static readonly DependencyProperty IsReadOnlyProperty;

        public static readonly DependencyProperty NumberFormatInfoProperty;

        public static readonly RoutedEvent ValueChangedEvent;

        public static RoutedCommand IncreaseCommand;

        public static RoutedCommand DecreaseCommand;

        private decimal _inputValue;

        private string _lastInput;

        private TextBox _textBox;

        #endregion

        #region Properties

        public decimal Value
        {
            get
            {
                return (decimal)GetValue(ValueProperty);
            }

            set
            {
                SetValue(ValueProperty, value);
            }
        }

        public decimal Minimum
        {
            get
            {
                return (decimal)GetValue(MinimumProperty);
            }

            set
            {
                SetValue(MinimumProperty, value);
            }
        }

        public decimal Maximum
        {
            get
            {
                return (decimal)GetValue(MaximumProperty);
            }

            set
            {
                SetValue(MaximumProperty, value);
            }
        }

        public decimal Increment
        {
            get
            {
                return (decimal)GetValue(IncrementProperty);
            }

            set
            {
                SetValue(IncrementProperty, value);
            }
        }

        public int DecimalPlaces
        {
            get
            {
                return (int)GetValue(DecimalPlacesProperty);
            }

            set
            {
                SetValue(DecimalPlacesProperty, value);
            }
        }

        public bool InterceptArrowKeys
        {
            get
            {
                return (bool)GetValue(InterceptArrowKeysProperty);
            }

            set
            {
                SetValue(InterceptArrowKeysProperty, value);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return (bool)GetValue(IsReadOnlyProperty);
            }

            set
            {
                SetValue(IsReadOnlyProperty, value);
            }
        }

        public NumberFormatInfo NumberFormatInfo
        {
            get
            {
                return (NumberFormatInfo)GetValue(NumberFormatInfoProperty);
            }

            set
            {
                SetValue(NumberFormatInfoProperty, value);
            }
        }

        public string ContentText
        {
            get
            {
                if (_textBox != null)
                {
                    return _textBox.Text;
                }

                return null;
            }
        }

        #endregion

        #region Events

        public event RoutedPropertyChangedEventHandler<decimal> ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }

            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        #endregion

        #region Constructors
        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));

            InitializeCommands();

            ValueProperty = DependencyProperty.Register("Value", typeof(decimal), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(DefaultMinimum, OnValueChanged, CoerceValue));

            MinimumProperty = DependencyProperty.Register("Minimum", typeof(decimal), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(DefaultMinimum, OnMinimumChanged, CoerceMinimum));

            MaximumProperty = DependencyProperty.Register("Maximum", typeof(decimal), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(DefaultMaximum, OnMaximumChanged, CoerceMaximum));

            IncrementProperty = DependencyProperty.Register("Increment", typeof(decimal), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(DefaultIncrement, OnIncrementChanged, CoerceIncrement), ValidateIncrement);

            DecimalPlacesProperty = DependencyProperty.Register("DecimalPlaces", typeof(int), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(DefaultDecimalPlaces, OnDecimalPlacesChanged), ValidateDecimalPlaces);

            InterceptArrowKeysProperty = DependencyProperty.Register("InterceptArrowKeys", typeof(bool), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(true));

            IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(false, OnIsReadOnlyChanged));

            NumberFormatInfoProperty = DependencyProperty.Register("NumberFormatInfo", typeof(NumberFormatInfo), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(NumberFormatInfo.CurrentInfo.Clone(), OnNumberFormatInfoChanged));


            ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<decimal>), typeof(NumericUpDown));

            EventManager.RegisterClassHandler(typeof(NumericUpDown),
                Mouse.MouseDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown), true);
        }

        public NumericUpDown()
            : base()
        {
            _lastInput = String.Empty;
        }

        #endregion

        #region Methods

        #region Statics

        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            NumericUpDown control = (NumericUpDown)obj;

            decimal oldValue = (decimal)args.OldValue;
            decimal newValue = (decimal)args.NewValue;

            NumericUpDownAutomationPeer peer = UIElementAutomationPeer.FromElement(control) as NumericUpDownAutomationPeer;
            if (peer != null)
            {
                peer.RaiseValueChangedEvent(oldValue, newValue);
            }

            RoutedPropertyChangedEventArgs<decimal> e = new RoutedPropertyChangedEventArgs<decimal>(
                oldValue, newValue, ValueChangedEvent);

            control.OnValueChanged(e);
            control.UpdateText();
        }

        private static void OnMinimumChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            element.CoerceValue(MaximumProperty);
            element.CoerceValue(ValueProperty);
        }

        private static void OnMaximumChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            element.CoerceValue(ValueProperty);
        }

        private static void OnIncrementChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
        }

        private static void OnDecimalPlacesChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            NumericUpDown control = (NumericUpDown)element;

            control.CoerceValue(IncrementProperty);
            control.CoerceValue(MinimumProperty);
            control.CoerceValue(MaximumProperty);
            control.CoerceValue(ValueProperty);

            control.UpdateText();
        }

        private static void OnIsReadOnlyChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            NumericUpDown control = element as NumericUpDown;
            bool readOnly = (bool)args.NewValue;

            if (readOnly != control._textBox.IsReadOnly)
            {
                control._textBox.IsReadOnly = readOnly;
            }
        }

        private static void OnNumberFormatInfoChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            NumericUpDown control = element as NumericUpDown;
            control.UpdateText();
        }

        private static object CoerceValue(DependencyObject element, object value)
        {
            decimal newValue = (decimal)value;
            NumericUpDown control = (NumericUpDown)element;

            newValue = Math.Max(control.Minimum, Math.Min(control.Maximum, newValue));
            newValue = Decimal.Round(newValue, control.DecimalPlaces);

            return newValue;
        }

        private static object CoerceMinimum(DependencyObject element, object value)
        {
            decimal newMinimum = (decimal)value;
            NumericUpDown control = (NumericUpDown)element;
            return Decimal.Round(newMinimum, control.DecimalPlaces);
        }

        private static object CoerceMaximum(DependencyObject element, object value)
        {
            NumericUpDown control = (NumericUpDown)element;
            decimal newMaximum = (decimal)value;
            return Decimal.Round(Math.Max(newMaximum, control.Minimum), control.DecimalPlaces);
        }

        private static object CoerceIncrement(DependencyObject element, object value)
        {
            decimal newIncrement = (decimal)value;
            NumericUpDown control = (NumericUpDown)element;

            decimal coercedNewIncrement = Decimal.Round(newIncrement, control.DecimalPlaces);

            if (coercedNewIncrement == Decimal.Zero)
            {
                coercedNewIncrement = SmallestForDecimalPlaces(control.DecimalPlaces);
            }

            return coercedNewIncrement;
        }

        private static bool ValidateIncrement(object value)
        {
            decimal change = (decimal)value;
            return change > 0;
        }

        private static bool ValidateDecimalPlaces(object value)
        {
            int decimalPlaces = (int)value;
            return decimalPlaces >= 0;
        }

        private static void InitializeCommands()
        {
            IncreaseCommand = new RoutedCommand("IncreaseCommand", typeof(NumericUpDown));
            CommandManager.RegisterClassCommandBinding(typeof(NumericUpDown), new CommandBinding(IncreaseCommand, OnIncreaseCommand));
            CommandManager.RegisterClassInputBinding(typeof(NumericUpDown), new InputBinding(IncreaseCommand, new KeyGesture(Key.Up)));

            DecreaseCommand = new RoutedCommand("DecreaseCommand", typeof(NumericUpDown));
            CommandManager.RegisterClassCommandBinding(typeof(NumericUpDown), new CommandBinding(DecreaseCommand, OnDecreaseCommand));
            CommandManager.RegisterClassInputBinding(typeof(NumericUpDown), new InputBinding(DecreaseCommand, new KeyGesture(Key.Down)));
        }

        private static void OnIncreaseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            NumericUpDown control = sender as NumericUpDown;

            if (control != null)
            {
                control.OnIncrease();
            }
        }

        private static void OnDecreaseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            NumericUpDown control = sender as NumericUpDown;

            if (control != null)
            {
                control.OnDecrease();
            }
        }

        private static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NumericUpDown control = (NumericUpDown)sender;

            if (!control.IsKeyboardFocusWithin)
            {
                e.Handled = control.Focus() || e.Handled;
            }
        }

        private static decimal SmallestForDecimalPlaces(int decimalPlaces)
        {
            if (decimalPlaces < 0)
                throw new ArgumentOutOfRangeException("decimalPlaces");

            decimal d = 1;

            for (int i = 0; i < decimalPlaces; i++)
            {
                d /= 10;
            }

            return d;
        }

        #endregion

        #region Dynamics

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_textBox != null)
            {
                _textBox.TextChanged -= new TextChangedEventHandler(OnTextBoxTextChanged);
                _textBox.PreviewKeyDown -= new KeyEventHandler(OnTextBoxPreviewKeyDown);
            }

            _textBox = (TextBox)base.GetTemplateChild("textbox");

            if (_textBox != null)
            {
                _textBox.TextChanged += new TextChangedEventHandler(OnTextBoxTextChanged);
                _textBox.PreviewKeyDown += new KeyEventHandler(OnTextBoxPreviewKeyDown);
                _textBox.IsReadOnly = false;
            }

            UpdateText();
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new NumericUpDownAutomationPeer(this);
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                OnGotFocus();
            }
            else
            {
                OnLostFocus();
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            if (IsKeyboardFocusWithin)
            {
                if (e.Delta > 0)
                {
                    OnIncrease();
                }
                else
                {
                    OnDecrease();
                }
            }
        }

        protected virtual void OnValueChanged(RoutedPropertyChangedEventArgs<decimal> args)
        {
            RaiseEvent(args);
        }

        protected virtual void OnIncrease()
        {
            UpdateValue();

            if (Value + Increment <= Maximum)
            {
                Value += Increment;
            }
        }

        protected virtual void OnDecrease()
        {
            UpdateValue();

            if (Value - Increment >= Minimum)
            {
                Value -= Increment;
            }
        }

        private void OnGotFocus()
        {
            if (_textBox != null)
            {
                _textBox.Focus();
            }

            UpdateText();
        }

        private void OnLostFocus()
        {
            UpdateValue();
            UpdateText();
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsReadOnly)
            {
                string text = _textBox.Text;

                if (String.IsNullOrEmpty(text) || text == NumberFormatInfo.NegativeSign)
                {
                    return;
                }

                decimal parsedValue = 0M;
                if (decimal.TryParse(text, NumberStyle, NumberFormatInfo, out parsedValue))
                {
                    if ((DecimalPlaces == 0) && (text.Contains(NumberFormatInfo.NumberDecimalSeparator)))
                    {
                        ReturnPreviousInput();
                        return;
                    }

                    _lastInput = text;
                    _inputValue = parsedValue;
                    return;
                }

                ReturnPreviousInput();
            }
            else
            {
                _lastInput = _textBox.Text;
                _inputValue = Value;
            }
        }

        private void OnTextBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    if (InterceptArrowKeys)
                        OnIncrease();
                    break;

                case Key.Down:
                    if (InterceptArrowKeys)
                        OnDecrease();
                    break;

                case Key.Return:
                    UpdateValue();
                    UpdateText();
                    break;

                default:
                    return;
            }

            e.Handled = true;
        }

        internal void UpdateText()
        {
            NumberFormatInfo formatInfo = (NumberFormatInfo)NumberFormatInfo.Clone();
            formatInfo.NumberGroupSeparator = String.Empty;

            string formattedValue = Value.ToString("F" + DecimalPlaces, formatInfo);

            if (_textBox != null)
            {
                _lastInput = formattedValue;
                _textBox.Text = formattedValue;
            }
        }

        internal void UpdateValue()
        {
            if (_inputValue != Value)
            {
                Value = (decimal)CoerceValue(this, _inputValue);
            }
        }

        private void ReturnPreviousInput()
        {
            int selectionLenght = _textBox.SelectionLength;
            int selectionStart = _textBox.SelectionStart;

            _textBox.Text = _lastInput;
            _textBox.SelectionStart = (selectionStart == 0) ? 0 : (selectionStart - 1);
            _textBox.SelectionLength = selectionLenght;
        }

        #endregion

        #endregion
    }

    public class NumericUpDownAutomationPeer : FrameworkElementAutomationPeer, IRangeValueProvider
    {
        #region Properties

        double IRangeValueProvider.Value
        {
            get
            {
                return (double)GetOwner().Value;
            }
        }

        double IRangeValueProvider.Minimum
        {
            get
            {
                return (double)GetOwner().Minimum;
            }
        }

        double IRangeValueProvider.Maximum
        {
            get
            {
                return (double)GetOwner().Maximum;
            }
        }

        double IRangeValueProvider.SmallChange
        {
            get
            {
                return (double)GetOwner().Increment;
            }
        }

        double IRangeValueProvider.LargeChange
        {
            get
            {
                return (double)GetOwner().Increment;
            }
        }

        bool IRangeValueProvider.IsReadOnly
        {
            get
            {
                return GetOwner().IsReadOnly;
            }
        }

        #endregion

        #region Constructors

        public NumericUpDownAutomationPeer(NumericUpDown owner)
            : base(owner)
        {
        }

        #endregion

        #region Methods

        public override object GetPattern(PatternInterface patternInterface)
        {
            if (patternInterface == PatternInterface.RangeValue)
            {
                return this;
            }
            return base.GetPattern(patternInterface);
        }

        protected override string GetClassNameCore()
        {
            return "NumericUpDown";
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Spinner;
        }

        internal void RaiseValueChangedEvent(decimal oldValue, decimal newValue)
        {
            base.RaisePropertyChangedEvent(RangeValuePatternIdentifiers.ValueProperty,
                (double)oldValue, (double)newValue);
        }

        private NumericUpDown GetOwner()
        {
            return (NumericUpDown)base.Owner;
        }

        void IRangeValueProvider.SetValue(double value)
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }

            decimal val = (decimal)value;
            NumericUpDown control = GetOwner();

            if (val < control.Minimum || val > control.Maximum)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            control.Value = val;
        }

        #endregion
    }
}
