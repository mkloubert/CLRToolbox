// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Timing
{
    /// <summary>
    /// Saves and handles clock data.
    /// </summary>
    public struct Time : IEquatable<Time>, IComparable<Time>, IEquatable<TimeSpan>, IComparable<TimeSpan>, IEquatable<long>, IComparable<long>, IComparable
    {
        #region Data Members (16)

        private readonly long _TICKS;
        /// <summary>
        /// Saves the maximum tick value.
        /// </summary>
        public const long MaxTicks = 863999999999;
        /// <summary>
        /// The maximum value.
        /// </summary>
        public static readonly Time MaxValue = new Time(863999999999);
        /// <summary>
        /// Saves the minimum tick value.
        /// </summary>
        public const long MinTicks = 0;
        /// <summary>
        /// The minimum value.
        /// </summary>
        public static readonly Time MinValue = new Time(0);
        /// <summary>
        /// Represents a zero value.
        /// </summary>
        public static readonly Time Zero = new Time(0);
        /// <summary>
        /// Gets the hour parts of that <see cref="Time" /> value.
        /// </summary>
        public int Hours
        {
            get { return (int)(this._TICKS / 36000000000L % 24L); }
        }

        /// <summary>
        /// Gets the millisecond parts of that <see cref="Time" /> value.
        /// </summary>
        public int Milliseconds
        {
            get { return (int)(this._TICKS / 10000L % 1000L); }
        }

        /// <summary>
        /// Gets the minute parts of that <see cref="Time" /> value.
        /// </summary>
        public int Minutes
        {
            get { return (int)(this._TICKS / 600000000L % 60L); }
        }

        /// <summary>
        /// Returns the current time.
        /// </summary>
        public static Time Now
        {
            get
            {
                var now = DateTime.Now;
                return new Time(now.Ticks - now.Date.Ticks);
            }
        }

        /// <summary>
        /// Gets the second parts of that <see cref="Time" /> value.
        /// </summary>
        public int Seconds
        {
            get { return (int)(this._TICKS / 10000000L % 60L); }
        }

        /// <summary>
        /// Gets the value in ticks.
        /// </summary>
        public long Ticks
        {
            get { return this._TICKS; }
        }

        /// <summary>
        /// Gets the total number of hours that represents that time value.
        /// </summary>
        public double TotalHours
        {
            get { return (double)this._TICKS * 2.7777777777777777E-11; }
        }

        /// <summary>
        /// Gets the total number of milliseconds that represents that time value.
        /// </summary>
        public double TotalMilliseconds
        {
            get
            {
                double num = (double)this._TICKS * 0.0001;

                if (num > 922337203685477.0)
                {
                    return 922337203685477.0;
                }

                if (num < -922337203685477.0)
                {
                    return -922337203685477.0;
                }

                return num;
            }
        }

        /// <summary>
        /// Gets the total number of minutes that represents that time value.
        /// </summary>
        public double TotalMinutes
        {
            get { return (double)this._TICKS * 1.6666666666666667E-09; }
        }

        /// <summary>
        /// Gets the total number of seconds that represents that time value.
        /// </summary>
        public double TotalSeconds
        {
            get { return (double)this._TICKS * 1E-07; }
        }

        #endregion Data Members

        #region Methods (47)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Time.Subtract(long)" />
        public static Time operator -(Time clock, long ticks)
        {
            return clock.Subtract(ticks);
        }

        /// <summary>
        /// The opposite of <see cref="Time.Equals(Time)" />.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>Are equal (<see langword="false" />) or not (<see langword="true" />).</returns>
        public static bool operator !=(Time left, Time right)
        {
            return (left == right) == false;
        }

        /// <summary>
        /// Checks if a time value and a long value that represents a tick value are NOT equal.
        /// </summary>
        /// <param name="clock">The time value.</param>
        /// <param name="ticks">The tick value.</param>
        /// <returns>Are not equal; otherwise <see langword="false" />.</returns>
        public static bool operator !=(Time clock, long ticks)
        {
            return (clock == ticks) == false;
        }

        /// <summary>
        /// Checks if a time value and a long value that represents a tick value are NOT equal.
        /// </summary>
        /// <param name="ticks">The tick value.</param>
        /// <param name="clock">The time value.</param>
        /// <returns>Are not equal; otherwise <see langword="false" />.</returns>
        public static bool operator !=(long ticks, Time clock)
        {
            return clock != ticks;
        }

        /// <summary>
        /// Checks if a time value and a timespan value are NOT equal.
        /// </summary>
        /// <param name="clock">The time value.</param>
        /// <param name="ts">The timespan value.</param>
        /// <returns>Are not equal; otherwise <see langword="false" />.</returns>
        public static bool operator !=(Time clock, TimeSpan ts)
        {
            return (clock == ts) == false;
        }

        /// <summary>
        /// Checks if a time value and a timespan value are NOT equal.
        /// </summary>
        /// <param name="ts">The timespan value.</param>
        /// <param name="clock">The time value.</param>
        /// <returns>Are not equal; otherwise <see langword="false" />.</returns>
        public static bool operator !=(TimeSpan ts, Time clock)
        {
            return clock != ts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Time.Add(long)" />
        public static Time operator +(Time clock, long ticks)
        {
            return clock.Add(ticks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Time.Equals(Time)" />
        public static bool operator ==(Time left, Time right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Checks if a time value and a long value that represents a tick value are equal.
        /// </summary>
        /// <param name="clock">The time value.</param>
        /// <param name="ticks">The tick value.</param>
        /// <returns>Are equal or not.</returns>
        public static bool operator ==(Time clock, long ticks)
        {
            return clock.Equals(ticks);
        }

        /// <summary>
        /// Checks if a time value and a long value that represents a tick value are equal.
        /// </summary>
        /// <param name="ticks">The tick value.</param>
        /// <param name="clock">The time value.</param>
        /// <returns>Are equal or not.</returns>
        public static bool operator ==(long ticks, Time clock)
        {
            return clock == ticks;
        }

        /// <summary>
        /// Checks if a time value and a timespan value are equal.
        /// </summary>
        /// <param name="clock">The time value.</param>
        /// <param name="ts">The timespan value.</param>
        /// <returns>Are equal or not.</returns>
        public static bool operator ==(Time clock, TimeSpan ts)
        {
            return clock.Equals(ts);
        }

        /// <summary>
        /// Checks if a time value and a timespan value are equal.
        /// </summary>
        /// <param name="ts">The timespan value.</param>
        /// <param name="clock">The time value.</param>
        /// <returns>Are equal or not.</returns>
        public static bool operator ==(TimeSpan ts, Time clock)
        {
            return clock == ts;
        }

        /// <summary>
        /// Adds ticks to this value.
        /// </summary>
        /// <param name="ticks">The number of ticks to add.</param>
        /// <returns>The instance with the new value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="ticks" /> is invalid.
        /// </exception>
        public Time Add(long ticks)
        {
            return new Time(this._TICKS + ticks);
        }

        private static void CheckTickValue(long ticks)
        {
            if (ticks < MinTicks || ticks > MaxTicks)
            {
#if !WINDOWS_PHONE
                throw new global::System.ArgumentOutOfRangeException("ticks",
                                                                     ticks,
                                                                     string.Format("The tick value has to be a value between {0} and {1}!",
                                                                                   MinTicks,
                                                                                   MaxTicks));
#else
                throw new global::System.ArgumentOutOfRangeException("ticks",
                                                                     string.Format("The tick value has to be a value between {0} and {1}!",
                                                                                   MinTicks,
                                                                                   MaxTicks));
#endif
            }
        }

        /// <summary>
        /// Compares to <see cref="Time" /> values.
        /// </summary>
        /// <param name="x">The left value.</param>
        /// <param name="y">The right value.</param>
        /// <returns>
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description><paramref name="x" /> is smaller than <paramref name="y" /></description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description><paramref name="x" /> is equals to <paramref name="y" /></description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description><paramref name="x" /> is greater than <paramref name="y" /></description>
        /// </item>
        /// </list>
        /// </returns>
        public static int Compare(Time x, Time y)
        {
            return x.CompareTo(y);
        }

        /// <inheriteddoc />
        public int CompareTo(object other)
        {
            if (other == null)
            {
                return 1;
            }

            var otherClock = (Time)other;

            var ticks = otherClock._TICKS;
            if (this._TICKS > ticks)
            {
                return 1;
            }
            else if (this._TICKS < ticks)
            {
                return -1;
            }

            return 0;
        }

        /// <inheriteddoc />
        public int CompareTo(Time other)
        {
            return this._TICKS
                       .CompareTo(other._TICKS);
        }

        /// <inheriteddoc />
        public int CompareTo(TimeSpan other)
        {
            return this.CompareTo(other.Ticks);
        }

        /// <inheriteddoc />
        public int CompareTo(long other)
        {
            return this._TICKS.CompareTo(other);
        }

        /// <summary>
        /// Creates a new instance from part value.
        /// </summary>
        /// <param name="hours">The hour part.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The calculated tick value for the new time value would be invalid.
        /// </exception>
        public static Time Create(int hours)
        {
            return Create(hours, 0);
        }

        /// <summary>
        /// Creates a new instance from part value.
        /// </summary>
        /// <param name="hours">The hour part.</param>
        /// <param name="minutes">The minute part.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The calculated tick value for the new time value would be invalid.
        /// </exception>
        public static Time Create(int hours,
                                  int minutes)
        {
            return Create(hours, minutes, 0);
        }

        /// <summary>
        /// Creates a new instance from part value.
        /// </summary>
        /// <param name="hours">The hour part.</param>
        /// <param name="minutes">The minute part.</param>
        /// <param name="seconds">The second part.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The calculated tick value for the new time value would be invalid.
        /// </exception>
        public static Time Create(int hours,
                                  int minutes,
                                  int seconds)
        {
            return Create(hours, minutes, seconds, 0);
        }

        /// <summary>
        /// Creates a new instance from part value.
        /// </summary>
        /// <param name="hours">The hour part.</param>
        /// <param name="minutes">The minute part.</param>
        /// <param name="seconds">The second part.</param>
        /// <param name="milliseconds">The millisecond part.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The calculated tick value for the new time value would be invalid.
        /// </exception>
        public static Time Create(int hours,
                                  int minutes,
                                  int seconds,
                                  int milliseconds)
        {
            return Create(hours, minutes, seconds, milliseconds, 0);
        }

        /// <summary>
        /// Creates a new instance from part value.
        /// </summary>
        /// <param name="hours">The hour part.</param>
        /// <param name="minutes">The minute part.</param>
        /// <param name="seconds">The second part.</param>
        /// <param name="milliseconds">The millisecond part.</param>
        /// <param name="additionalTicks">The additional ticks to add.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The calculated tick value for the new time value would be invalid.
        /// </exception>
        public static Time Create(int hours,
                                  int minutes,
                                  int seconds,
                                  int milliseconds,
                                  long additionalTicks)
        {
            return new Time((((hours * 3600L) +
                              (minutes * 60L) +
                               seconds) * 1000L + milliseconds) * 10000L    // ticks per millisecond
                                                                + additionalTicks);
        }

        /// <summary>
        /// Returns a time value as <see cref="DateTime" /> with the date part from <see cref="DateTime.Now" />.
        /// </summary>
        /// <param name="clock">The clock value.</param>
        /// <returns>The date/time value with date part from <see cref="DateTime.Now" />.</returns>
        public static implicit operator DateTime(Time clock)
        {
            return DateTime.Today
                           .AddTicks(clock._TICKS);
        }

        /// <inheriteddoc />
        public bool Equals(Time other)
        {
            return this.Equals(other._TICKS);
        }

        /// <inheriteddoc />
        public override bool Equals(object other)
        {
            if (other is Time)
            {
                return this.Equals((Time)other);
            }

            if (other is TimeSpan)
            {
                return this.Equals((TimeSpan)other);
            }

            if (other is IConvertible)
            {
                return this.Equals(GlobalConverter.Current.ChangeType<long>(other));
            }

            return base.Equals(other);
        }

        /// <inheriteddoc />
        public bool Equals(TimeSpan other)
        {
            return this.Equals(other.Ticks);
        }

        /// <inheriteddoc />
        public bool Equals(long other)
        {
            return this._TICKS == other;
        }

        /// <summary>
        /// Creates a new instance from a hour value.
        /// </summary>
        /// <param name="hours">The hour value.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="hours" /> is invalid.
        /// </exception>
        public static Time FromHours(double hours)
        {
            return new Time(TimeSpan.FromHours(hours));
        }

        /// <summary>
        /// Creates a new instance from a millisecond value.
        /// </summary>
        /// <param name="msec">The millisecond value.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="msec" /> is invalid.
        /// </exception>
        public static Time FromMilliseconds(double msec)
        {
            return new Time(TimeSpan.FromMilliseconds(msec));
        }

        /// <summary>
        /// Creates a new instance from a minute value.
        /// </summary>
        /// <param name="minutes">The minutes value.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="minutes" /> is invalid.
        /// </exception>
        public static Time FromMinutes(double minutes)
        {
            return new Time(TimeSpan.FromMinutes(minutes));
        }

        /// <summary>
        /// Creates a new instance from a seconds value.
        /// </summary>
        /// <param name="seconds">The seconds value.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="seconds" /> is invalid.
        /// </exception>
        public static Time FromSeconds(double seconds)
        {
            return new Time(TimeSpan.FromSeconds(seconds));
        }

        /// <summary>
        /// Creates a new instance from a tick value.
        /// </summary>
        /// <param name="ticks">The ticks that represent the new <see cref="Time" /> value.</param>
        /// <returns>The created instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="ticks" /> is invalid.
        /// </exception>
        public static Time FromTicks(long ticks)
        {
            return new Time(ticks);
        }

        /// <inheriteddoc />
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Converts a <see cref="Time" /> value to its tick representation.
        /// </summary>
        /// <param name="clock">The time value.</param>
        /// <returns>The time value in ticks.</returns>
        public static implicit operator long(Time clock)
        {
            return clock._TICKS;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="TimeSpan.Parse(string)" />
        public static Time Parse(IEnumerable<char> str)
        {
            return new Time(TimeSpan.Parse(StringHelper.AsString(str)));
        }

        /// <summary>
        /// Converts a <see cref="Time" /> value to its string representation.
        /// </summary>
        /// <param name="clock">The time value.</param>
        /// <returns>The time value as string.</returns>
        public static implicit operator string(Time clock)
        {
            return clock.ToString();
        }

        /// <summary>
        /// Subtracts ticks from this value.
        /// </summary>
        /// <param name="ticks">The number of ticks to subtract.</param>
        /// <returns>The instance with the new value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="ticks" /> is invalid.
        /// </exception>
        public Time Subtract(long ticks)
        {
            return this.Add(-ticks);
        }

        /// <summary>
        /// Converts a tick value to its <see cref="Time" /> representation.
        /// </summary>
        /// <param name="ticks">The tick value.</param>
        /// <returns>The tick value as <see cref="Time" /> value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="ticks" /> is invalid.
        /// </exception>
        public static explicit operator Time(long ticks)
        {
            return new Time(ticks);
        }

        /// <summary>
        /// Converts a <see cref="TimeSpan" /> value to <see cref="Time" />.
        /// </summary>
        /// <param name="ts">The input value.</param>
        /// <returns>The target value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="ts" /> is invalid.
        /// </exception>
        public static explicit operator Time(TimeSpan ts)
        {
            return new Time(ts);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> struct.
        /// </summary>
        /// <param name="ticks">The clock value in ticks.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="ticks" /> is invalid.
        /// </exception>
        public Time(long ticks)
        {
            CheckTickValue(ticks);

            this._TICKS = ticks;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> struct.
        /// </summary>
        /// <param name="ts">The time span value from where to read the tick value from.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="ts" /> is invalid.
        /// </exception>
        public Time(TimeSpan ts)
            : this(ts.Ticks)
        {

        }

        /// <summary>
        /// Converts a <see cref="Time" /> value to <see cref="TimeSpan" />.
        /// </summary>
        /// <param name="clock">The input value.</param>
        /// <returns>The target value.</returns>
        public static implicit operator TimeSpan(Time clock)
        {
            return TimeSpan.FromTicks(clock._TICKS);
        }

        /// <inheriteddoc />
        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}",
                                 this.Hours.ToString().PadLeft(2, '0'),
                                 this.Minutes.ToString().PadLeft(2, '0'),
                                 this.Seconds.ToString().PadLeft(2, '0'));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="TimeSpan.TryParse(string, out TimeSpan)" />
        public static bool TryParse(IEnumerable<char> str, out Time result)
        {
            return TryParseInner(delegate(string input, out TimeSpan tsResult)
                {
                    return TimeSpan.TryParse(input,
                                             out tsResult);
                }, str
                 , out result);
        }

        private static bool TryParseInner(TryParseInnerHandler handler, IEnumerable<char> str, out Time result)
        {
            result = default(Time);

            TimeSpan ts;
            if (handler(StringHelper.AsString(str), out ts))
            {
                result = (Time)ts;
                return true;
            }

            return false;
        }

        #endregion Methods
#if !NET2 && !NET20 && !NET35
        /// <summary>
        /// 
        /// </summary>
        /// <see cref="global::System.TimeSpan.Parse(string, global::System.IFormatProvider)" />
        public static Time Parse(global::System.Collections.Generic.IEnumerable<char> str,
                                 global::System.IFormatProvider formatProvider)
        {
            return new Time(global::System.TimeSpan.Parse(global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(str),
                                                          formatProvider));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <see cref="global::System.TimeSpan.ParseExact(string, string, global::System.IFormatProvider)" />
        public static Time ParseExact(global::System.Collections.Generic.IEnumerable<char> str,
                                      global::System.Collections.Generic.IEnumerable<char> format,
                                      global::System.IFormatProvider formatProvider)
        {
            return (Time)global::System.TimeSpan.ParseExact(global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(str),
                                                            global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(format),
                                                            formatProvider);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <see cref="global::System.TimeSpan.ParseExact(string, string[], global::System.IFormatProvider)" />
        public static Time ParseExact(global::System.Collections.Generic.IEnumerable<char> str,
                                      global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.IEnumerable<char>> formats,
                                      global::System.IFormatProvider formatProvider)
        {
            var strFormats = global::MarcelJoachimKloubert.CLRToolbox.Helpers.CollectionHelper.Select(formats, delegate(global::System.Collections.Generic.IEnumerable<char> fChars)
                {
                    return global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(fChars);
                });

            return (Time)global::System.TimeSpan.ParseExact(global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(str),
                                                            formats == null ? null : global::MarcelJoachimKloubert.CLRToolbox.Helpers.CollectionHelper.ToArray(strFormats),
                                                            formatProvider);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <see cref="global::System.TimeSpan.ToString(string)" />
        public string ToString(global::System.Collections.Generic.IEnumerable<char> format)
        {
            return TimeSpan.FromTicks(this._TICKS)
                           .ToString(StringHelper.AsString(format));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <see cref="global::System.TimeSpan.ToString(string, global::System.IFormatProvider)" />
        public string ToString(global::System.Collections.Generic.IEnumerable<char> format,
                               global::System.IFormatProvider formatProvider)
        {
            return global::System.TimeSpan.FromTicks(this._TICKS)
                                          .ToString(global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(format),
                                                    formatProvider);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <see cref="global::System.TimeSpan.TryParse(string, global::System.IFormatProvider, out global::System.TimeSpan)" />
        public static bool TryParse(global::System.Collections.Generic.IEnumerable<char> str,
                                    global::System.IFormatProvider formatProvider,
                                    out Time result)
        {
            return TryParseInner(delegate(string input, out global::System.TimeSpan tsResult)
                {
                    return global::System.TimeSpan.TryParse(input, formatProvider,
                                                            out tsResult);
                }, str
                 , out result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <see cref="global::System.TimeSpan.TryParseExact(string, string, global::System.IFormatProvider, out global::System.TimeSpan)" />
        public static bool TryParseExact(global::System.Collections.Generic.IEnumerable<char> str,
                                         global::System.Collections.Generic.IEnumerable<char> format,
                                         global::System.IFormatProvider formatProvider,
                                         out Time result)
        {
            return TryParseInner(delegate(string input, out global::System.TimeSpan tsResult)
                {
                    return global::System.TimeSpan.TryParseExact(input, global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(format), formatProvider,
                                                                 out tsResult);
                }, str
                 , out result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <see cref="TimeSpan.TryParseExact(string, string[], global::System.IFormatProvider, out TimeSpan)" />
        public static bool TryParseExact(global::System.Collections.Generic.IEnumerable<char> str,
                                         global::System.Collections.Generic.IEnumerable<IEnumerable<char>> formats,
                                         global::System.IFormatProvider formatProvider,
                                         out Time result)
        {
            return TryParseInner(delegate(string input, out global::System.TimeSpan tsResult)
                {
                    var strFormats = global::MarcelJoachimKloubert.CLRToolbox.Helpers.CollectionHelper.Select(formats, delegate(IEnumerable<char> fChars)
                        {
                            return global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(fChars);
                        });

                    return global::System.TimeSpan.TryParseExact(input,
                                                                 formats == null ? null : global::MarcelJoachimKloubert.CLRToolbox.Helpers.CollectionHelper.ToArray(strFormats),
                                                                 formatProvider,
                                                                 out tsResult);
                }, str
                 , out result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <see cref="global::System.TimeSpan.ParseExact(string, string, global::System.IFormatProvider, global::System.Globalization.TimeSpanStyles)" />
        public static Time ParseExact(global::System.Collections.Generic.IEnumerable<char> str,
                                      global::System.Collections.Generic.IEnumerable<char> format,
                                      global::System.IFormatProvider formatProvider,
                                      global::System.Globalization.TimeSpanStyles styles)
        {
            return (Time)global::System.TimeSpan.ParseExact(global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(str),
                                                            global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(format),
                                                            formatProvider,
                                                            styles);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <see cref="global::System.TimeSpan.ParseExact(string, string[], global::System.IFormatProvider, global::System.Globalization.TimeSpanStyles)" />
        public static Time ParseExact(global::System.Collections.Generic.IEnumerable<char> str,
                                      global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.IEnumerable<char>> formats,
                                      global::System.IFormatProvider formatProvider,
                                      global::System.Globalization.TimeSpanStyles styles)
        {
            var strFormats = global::MarcelJoachimKloubert.CLRToolbox.Helpers.CollectionHelper.Select(formats, delegate(global::System.Collections.Generic.IEnumerable<char> fChars)
                {
                    return global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(fChars);
                });

            return (Time)global::System.TimeSpan.ParseExact(global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(str),
                                                            formats == null ? null : global::MarcelJoachimKloubert.CLRToolbox.Helpers.CollectionHelper.ToArray(strFormats),
                                                            formatProvider,
                                                            styles);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <see cref="global::System.TimeSpan.TryParseExact(string, string, global::System.IFormatProvider, global::System.Globalization.TimeSpanStyles, out global::System.TimeSpan)" />
        public static bool TryParseExact(global::System.Collections.Generic.IEnumerable<char> str,
                                         global::System.Collections.Generic.IEnumerable<char> format,
                                         global::System.IFormatProvider formatProvider,
                                         global::System.Globalization.TimeSpanStyles styles,
                                         out Time result)
        {
            return TryParseInner(delegate(string input, out global::System.TimeSpan tsResult)
                {
                    return global::System.TimeSpan.TryParseExact(input, global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(format), formatProvider, styles,
                                                                 out tsResult);
                }, str
                 , out result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <see cref="global::System.TimeSpan.TryParseExact(string, string[], global::System.IFormatProvider, global::System.Globalization.TimeSpanStyles, out global::System.TimeSpan)" />
        public static bool TryParseExact(global::System.Collections.Generic.IEnumerable<char> str,
                                         global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.IEnumerable<char>> formats,
                                         global::System.IFormatProvider formatProvider,
                                         global::System.Globalization.TimeSpanStyles styles,
                                         out Time result)
        {
            return TryParseInner(delegate(string input, out global::System.TimeSpan tsResult)
                {
                    var strFormats = global::MarcelJoachimKloubert.CLRToolbox.Helpers.CollectionHelper.Select(formats, delegate(global::System.Collections.Generic.IEnumerable<char> fChars)
                        {
                            return global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(fChars);
                        });

                    return global::System.TimeSpan.TryParseExact(input,
                                                                formats == null ? null : global::MarcelJoachimKloubert.CLRToolbox.Helpers.CollectionHelper.ToArray(strFormats),
                                                                formatProvider,
                                                                styles,
                                                                out tsResult);
                }, str
                 , out result);
        }
#endif
        /// <summary>
        /// Checks if a left value is greater than a right value.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>Is greater or not.</returns>
        public static bool operator >(Time left, Time right)
        {
            return left.CompareTo(right) > 0;
        }
        /// <summary>
        /// Checks if a left value is greater or equal than a right value.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>Is greater or equal or not.</returns>
        public static bool operator >=(Time left, Time right)
        {
            return left.CompareTo(right) >= 0;
        }
        /// <summary>
        /// Checks if a left value is less than a right value.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>Is less or not.</returns>
        public static bool operator <(Time left, Time right)
        {
            return right > left;
        }
        /// <summary>
        /// Checks if a left value is less or equal than a right value.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>Is less or equal or not.</returns>
        public static bool operator <=(Time left, Time right)
        {
            return right >= left;
        }
        /// <summary>
        /// Checks if a <see cref="TimeSpan" /> value is greater than a <see cref="Time" /> value.
        /// </summary>
        /// <param name="ts">The timespan value.</param>
        /// <param name="clock">The time value.</param>
        /// <returns>Is greater or not.</returns>
        public static bool operator >(TimeSpan ts, Time clock)
        {
            return ts.Ticks > clock._TICKS;
        }
        /// <summary>
        /// Checks if a <see cref="TimeSpan" /> value is greater or equal than a <see cref="Time" /> value.
        /// </summary>
        /// <param name="ts">The timespan value.</param>
        /// <param name="clock">The time value.</param>
        /// <returns>Is greater/equal or not.</returns>
        public static bool operator >=(TimeSpan ts, Time clock)
        {
            return ts.Ticks >= clock._TICKS;
        }
        /// <summary>
        /// Checks if a <see cref="TimeSpan" /> value is less than a <see cref="Time" /> value.
        /// </summary>
        /// <param name="ts">The timespan value.</param>
        /// <param name="clock">The time value.</param>
        /// <returns>Is less or not.</returns>
        public static bool operator <(TimeSpan ts, Time clock)
        {
            return clock > ts;
        }
        /// <summary>
        /// Checks if a <see cref="TimeSpan" /> value is less or equal than a <see cref="Time" /> value.
        /// </summary>
        /// <param name="ts">The timespan value.</param>
        /// <param name="clock">The time value.</param>
        /// <returns>Is less/equal or not.</returns>
        public static bool operator <=(TimeSpan ts, Time clock)
        {
            return clock >= ts;
        }
        /// <summary>
        /// Checks if a <see cref="Time" /> value is greater than a <see cref="TimeSpan" /> value.
        /// </summary>
        /// <param name="ts">The timespan value.</param>
        /// <param name="clock">The time value.</param>
        /// <returns>Is greater or not.</returns>
        public static bool operator >(Time clock, TimeSpan ts)
        {
            return ts < clock;
        }
        /// <summary>
        /// Checks if a <see cref="Time" /> value is greater or equal than a <see cref="TimeSpan" /> value.
        /// </summary>
        /// <param name="ts">The timespan value.</param>
        /// <param name="clock">The time value.</param>
        /// <returns>Is greater/equal or not.</returns>
        public static bool operator >=(Time clock, TimeSpan ts)
        {
            return ts <= clock;
        }
        /// <summary>
        /// Checks if a <see cref="Time" /> value is less than a <see cref="TimeSpan" /> value.
        /// </summary>
        /// <param name="ts">The timespan value.</param>
        /// <param name="clock">The time value.</param>
        /// <returns>Is less or not.</returns>
        public static bool operator <(Time clock, TimeSpan ts)
        {
            return ts > clock;
        }
        /// <summary>
        /// Checks if a <see cref="Time" /> value is less or equal than a <see cref="TimeSpan" /> value.
        /// </summary>
        /// <param name="ts">The timespan value.</param>
        /// <param name="clock">The time value.</param>
        /// <returns>Is less/equal or not.</returns>
        public static bool operator <=(Time clock, TimeSpan ts)
        {
            return ts >= clock;
        }
        private delegate bool TryParseInnerHandler(string str, out TimeSpan result);
    }
}
