// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using System;

namespace MarcelJoachimKloubert.CLRToolbox.Values
{
    /// <summary>
    /// Handle a progress value that cannot be less than 0 and not greater than 100 percentage.
    /// </summary>
    public sealed class ProgressValue : NotificationObjectBase
    {
        #region Fields (4)

        private readonly double _MAXIMUM;
        private readonly double _MINIMUM;
        private double _step = 1;
        private double _value;

        #endregion Fields

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of <see cref="ProgressValue" />.
        /// </summary>
        /// <param name="min">The value for the <see cref="ProgressValue.Minimum" /> property.</param>
        /// <param name="max">The value for the <see cref="ProgressValue.Maximum" /> property.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="min" /> is greater than <paramref name="max" />.
        /// </exception>
        public ProgressValue(double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException("min");
            }

            this._MINIMUM = min;
            this._MAXIMUM = max;

            this._value = min;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ProgressValue" /> by using 0 as minimum value.
        /// </summary>
        /// <param name="max">The value for the <see cref="ProgressValue.Maximum" /> property.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="max" /> is less than 0.
        /// </exception>
        public ProgressValue(double max)
            : this(0, max)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="ProgressValue" /> by using 0 as minimum
        /// and 100 as maimum value.
        /// </summary>
        public ProgressValue()
            : this(100)
        {

        }

        #endregion Constructors

        #region Properties (5)

        /// <summary>
        /// Gets the maximum value.
        /// </summary>
        public double Maximum
        {
            get { return this._MAXIMUM; }
        }

        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        public double Minimum
        {
            get { return this._MINIMUM; }
        }

        /// <summary>
        /// Gets the percentage value.
        /// </summary>
        public double Percentage
        {
            get
            {
                double range = this.Maximum - this.Minimum;
                if (range == 0)
                {
                    return 0;
                }

                return (this._value - this.Minimum) / range * 100.0f;
            }
        }

        /// <summary>
        /// Gets or sets the value that should be used by <see cref="ProgressValue.Increase()" /> method.
        /// </summary>
        public double Step
        {
            get { return this._step; }

            set
            {
                lock (this._SYNC)
                {
                    if (value != this._step)
                    {
                        this.OnPropertyChanging("Step");
                        this._step = value;
                        this.OnPropertyChanged("Step");
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <remarks>
        /// If new value is out of <see cref="ProgressValue.Minimum" /> or <see cref="ProgressValue.Maximum" />
        /// it is automatically corrected.
        /// </remarks>
        public double Value
        {
            get { return this._value; }

            set
            {
                lock (this._SYNC)
                {
                    if (value != this._value)
                    {
                        if (value < this.Minimum)
                        {
                            value = this.Minimum;
                        }

                        if (value > this.Maximum)
                        {
                            value = this.Maximum;
                        }

                        this.OnPropertyChanging("Value");
                        this._value = value;
                        this.OnPropertyChanged("Value");
                    }
                }
            }
        }

        #endregion Properties

        #region Methods (4)

        // Public Methods (4) 
        
        /// <summary>
        /// Resets <see cref="ProgressValue.Value" /> by the value of <see cref="ProgressValue.Maximum" />.
        /// </summary>
        public void Completed()
        {
            this.Value = this.Maximum;
        }

        /// <summary>
        /// Increases <see cref="ProgressValue.Value" /> by the value of <see cref="ProgressValue.Step" />.
        /// </summary>
        public void Increase()
        {
            this.Increase(this.Step);
        }

        /// <summary>
        /// Increases <see cref="ProgressValue.Value" />.
        /// </summary>
        /// <param name="step">The value to add.</param>
        public void Increase(double step)
        {
            this.Value += step;
        }

        /// <summary>
        /// Resets <see cref="ProgressValue.Value" /> by the value of <see cref="ProgressValue.Minimum" />.
        /// </summary>
        public void Reset()
        {
            this.Value = this.Minimum;
        }

        #endregion Methods
    }
}
